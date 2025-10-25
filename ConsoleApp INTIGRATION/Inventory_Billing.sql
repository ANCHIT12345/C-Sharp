CREATE DATABASE Inventory_Billing;
USE Inventory_Billing;

CREATE TABLE products(
ProductID INT PRIMARY KEY IDENTITY(1,1),
Name VARCHAR(50),
Price DECIMAL(10,2),
StockQty INT
);

CREATE TABLE customer_details(
CustomerID INT PRIMARY KEY IDENTITY(1,1),
Name VARCHAR(50),
PhoneNo INT
);



