using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Inventory___Billing_System__Retail_Store_.Services
{
    interface IBillable
    {
        void Checkout(int customerID);
    }
    abstract class ProductBase
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQty { get; set; }
        public string Category { get; set; }

        public abstract decimal GetDiscount(int qty);

        public override string ToString()
        {
            return $"ID: {ProductID}, Name: {Name}, Price: {Price:C}, Stock: {StockQty}, Category: {Category}";
        }
    }

    class Grocery : ProductBase
    {
        public override decimal GetDiscount(int qty)
        {
            return qty >= 10 ? 0.05m : 0m;
        }
    }

    class Electronics : ProductBase
    {
        public override decimal GetDiscount(int qty)
        {
            return qty >= 2 ? 0.10m : 0m;
        }
    }
    static class ProductFactory
    {
        public static ProductBase CreateFromRow(SqlDataReader reader)
        {
            int id = Convert.ToInt32(reader["ProductID"]);
            string name = Convert.ToString(reader["Name"]);
            decimal price = Convert.ToDecimal(reader["Price"]);
            int stock = Convert.ToInt32(reader["StockQty"]);
            string category = reader["Category"] == DBNull.Value ? "Grocery" : Convert.ToString(reader["Category"]);

            ProductBase product;
            if (category.Equals("Electronics", StringComparison.OrdinalIgnoreCase))
                product = new Electronics();
            else
                product = new Grocery();

            product.ProductID = id;
            product.Name = name;
            product.Price = price;
            product.StockQty = stock;
            product.Category = category;
            return product;
        }
    }
    internal class BillingService : IBillable
    {
        private static readonly string ConnectionString ="Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=Inventory_Billing;Trusted_Connection=True;";
        private Dictionary<int, int> cart = new Dictionary<int, int>();
        private Dictionary<int, ProductBase> productCache = new Dictionary<int, ProductBase>();
        public void AddToCart(int productId, int quantity)
        {
            if (quantity <= 0)
            {
                Console.WriteLine("Quantity must be > 0.");
                return;
            }
            var product = LoadProductById(productId);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            if (product.StockQty < quantity)
            {
                Console.WriteLine($"Not enough stock available. Current stock: {product.StockQty}");
                return;
            }

            if (cart.ContainsKey(productId))
                cart[productId] += quantity;
            else
                cart[productId] = quantity;
            productCache[productId] = product;

            Console.WriteLine($"Added {quantity} of '{product.Name}' (ID: {productId}) to cart.");
        }

        public void RemoveFromCart(int productId, int quantity)
        {
            if (!cart.ContainsKey(productId))
            {
                Console.WriteLine("Product not in cart.");
                return;
            }

            if (quantity <= 0)
            {
                Console.WriteLine("Quantity must be > 0.");
                return;
            }

            cart[productId] -= quantity;
            if (cart[productId] <= 0)
            {
                cart.Remove(productId);
                productCache.Remove(productId);
                Console.WriteLine($"Removed product {productId} from cart.");
            }
            else
            {
                Console.WriteLine($"Reduced product {productId} quantity by {quantity}. New qty: {cart[productId]}");
            }
        }

        public void ViewCart()
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            Console.WriteLine("Cart contents:");
            decimal runningTotal = 0m;
            foreach (var kv in cart)
            {
                int pid = kv.Key;
                int qty = kv.Value;
                ProductBase product;
                if (!productCache.TryGetValue(pid, out product))
                {
                    product = LoadProductById(pid);
                    if (product != null) productCache[pid] = product;
                }

                if (product == null)
                {
                    Console.WriteLine($"ProductID {pid} - (details unavailable) x {qty}");
                    continue;
                }

                decimal discount = product.GetDiscount(qty);
                decimal total = product.Price * qty * (1 - discount);
                runningTotal += total;

                Console.WriteLine($"{product.Name} (ID:{pid}) - Qty: {qty}, Unit: {product.Price:C}, Discount: {discount:P}, Line Total: {total:C}");
            }

            Console.WriteLine($"Cart Total (est): {runningTotal:C}");
        }

        public void ClearCart()
        {
            cart.Clear();
            productCache.Clear();
            Console.WriteLine("Cart cleared.");
        }
        public void Checkout(int customerID)
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("Cart empty - nothing to checkout.");
                return;
            }
            List<int> productIds = new List<int>(cart.Keys);
            var invoiceItems = new List<object>();
            decimal grandTotal = 0m;
            DateTime now = DateTime.Now;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlTransaction tx = con.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (var pid in productIds)
                        {
                            int desiredQty = cart[pid];

                            using (SqlCommand cmd = new SqlCommand(
                                "SELECT ProductID, Name, Price, StockQty, Category FROM Products WITH (UPDLOCK, ROWLOCK) WHERE ProductID = @pid", con, tx))
                            {
                                cmd.Parameters.AddWithValue("@pid", pid);

                                using (SqlDataReader rdr = cmd.ExecuteReader())
                                {
                                    if (!rdr.Read())
                                        throw new Exception($"ProductID {pid} not found.");

                                    ProductBase product = ProductFactory.CreateFromRow(rdr);

                                    if (product.StockQty < desiredQty)
                                        throw new Exception($"Insufficient stock for '{product.Name}' (ID:{pid}). Available: {product.StockQty}, Requested: {desiredQty}");
                                    productCache[pid] = product;
                                }
                            }
                        }
                        foreach (var pid in productIds)
                        {
                            int qty = cart[pid];
                            using (SqlCommand updateCmd = new SqlCommand(
                                "UPDATE Products SET StockQty = StockQty - @qty WHERE ProductID = @pid", con, tx))
                            {
                                updateCmd.Parameters.AddWithValue("@qty", qty);
                                updateCmd.Parameters.AddWithValue("@pid", pid);
                                int rows = updateCmd.ExecuteNonQuery();
                                if (rows == 0)
                                    throw new Exception($"Failed to update stock for ProductID {pid}.");
                            }
                        }
                        tx.Commit();
                        Console.WriteLine("Stock updated and transaction committed.");

                    }
                    catch (Exception ex)
                    {
                        try { tx.Rollback(); } catch { /* ignore rollback errors */ }
                        Console.WriteLine("Checkout failed: " + ex.Message);
                        return;
                    }
                }
            }
            foreach (var kv in cart)
            {
                int pid = kv.Key;
                int qty = kv.Value;
                var product = productCache[pid];
                decimal discountRate = product.GetDiscount(qty);
                decimal lineTotal = product.Price * qty * (1 - discountRate);
                grandTotal += lineTotal;

                invoiceItems.Add(new
                {
                    ProductID = pid,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    Quantity = qty,
                    Discount = discountRate,
                    LineTotal = lineTotal
                });
            }
            var invoice = new
            {
                InvoiceID = Guid.NewGuid().ToString(),
                CustomerID = customerID,
                Date = now,
                Items = invoiceItems,
                GrandTotal = grandTotal
            };
            string invoiceJsonFile = $"Invoice_Customer_{customerID}_{now:yyyyMMddHHmmss}.json";
            string invoiceJson = JsonSerializer.Serialize(invoice, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(invoiceJsonFile, invoiceJson);
            Console.WriteLine($"Invoice JSON saved: {invoiceJsonFile}");
            StringBuilder txt = new StringBuilder();
            txt.AppendLine($"Invoice ID: {invoice.InvoiceID}");
            txt.AppendLine($"Customer ID: {customerID}");
            txt.AppendLine($"Date: {now}");
            txt.AppendLine("Items:");
            foreach (var itemObj in invoiceItems)
            {
                string itemJson = JsonSerializer.Serialize(itemObj);
                using (JsonDocument doc = JsonDocument.Parse(itemJson))
                {
                    var root = doc.RootElement;
                    txt.AppendLine($" - {root.GetProperty("ProductName").GetString()} (ID:{root.GetProperty("ProductID").GetInt32()}) x {root.GetProperty("Quantity").GetInt32()} @ {root.GetProperty("UnitPrice").GetDecimal():C} " +
                                   $"Discount: {root.GetProperty("Discount").GetDecimal():P} => {root.GetProperty("LineTotal").GetDecimal():C}");
                }
            }
            txt.AppendLine($"Grand Total: {grandTotal:C}");

            string invoiceTxtFile = $"Invoice_Customer_{customerID}_{now:yyyyMMddHHmmss}.txt";
            File.WriteAllText(invoiceTxtFile, txt.ToString());
            Console.WriteLine($"Invoice text saved: {invoiceTxtFile}");
            AppendToDailySalesLog(new
            {
                InvoiceID = invoice.InvoiceID,
                CustomerID = customerID,
                Date = now,
                Items = invoiceItems,
                GrandTotal = grandTotal
            });
            ClearCart();
            Console.WriteLine("Checkout completed successfully.");
        }
        private ProductBase LoadProductById(int productId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT ProductID, Name, Price, StockQty, Category FROM Products WHERE ProductID = @pid", con))
                {
                    cmd.Parameters.AddWithValue("@pid", productId);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (!rdr.Read()) return null;
                        return ProductFactory.CreateFromRow(rdr);
                    }
                }
            }
        }
        private void AppendToDailySalesLog(object saleEntry)
        {
            string logFile = $"DailySales_{DateTime.Now:yyyyMMdd}.json";
            List<object> sales = new List<object>();

            if (File.Exists(logFile))
            {
                try
                {
                    string existing = File.ReadAllText(logFile);
                    if (!string.IsNullOrWhiteSpace(existing))
                    {
                        sales = JsonSerializer.Deserialize<List<object>>(existing) ?? new List<object>();
                    }
                }
                catch
                {
                    // ignore malformed log and overwrite
                    sales = new List<object>();
                }
            }

            sales.Add(saleEntry);
            string updated = JsonSerializer.Serialize(sales, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(logFile, updated);
            Console.WriteLine($"Appended sale to daily log: {logFile}");
        }
    }
}
