using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading___Concurrency_in_C_
{
    class BankAccount
    {
        public int Balance { get; private set; }
        public void Deposit(int amount)
        {
            int temp = Balance;
            temp += amount;
            Thread.Sleep(100);
            Balance = temp;
        }
        public void Withdraw(int amount)
        {
            int temp = Balance;
            temp -= amount;
            Thread.Sleep(100);
            Balance = temp;
        }
    }
    class BankAccountWithLock
    {
        private readonly object balanceLock = new object();
        public int Balance { get; private set; }
        public void Deposit(int amount)
        {
            lock (balanceLock)
            {
                int temp = Balance;
                temp += amount;
                Thread.Sleep(100);
                Balance = temp;
            }
        }
        public void Withdraw(int amount)
        {
            lock (balanceLock)
            {
                int temp = Balance;
                temp -= amount;
                Thread.Sleep(100);
                Balance = temp;
            }
        }
    }
    class banking
    {
        public static void Run()
        {
            BankAccount account = new BankAccount();
            Thread t1 = new Thread(() => PerformTransactions(account));
            Thread t2 = new Thread(() => PerformTransactions(account));
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            Console.WriteLine($"Final balance (without lock): {account.Balance}");
            BankAccountWithLock accountWithLock = new BankAccountWithLock();
            Thread t3 = new Thread(() => PerformTransactionsWithLock(accountWithLock));
            Thread t4 = new Thread(() => PerformTransactionsWithLock(accountWithLock));
            t3.Start();
            t4.Start();
            t3.Join();
            t4.Join();
        }
        static void PerformTransactions(BankAccount Acc)
        {
            for (int i = 0; i < 5; i++)
            {
                Acc.Deposit(100);
                Acc.Withdraw(50);
            }
        }
        static void PerformTransactionsWithLock(BankAccountWithLock Acc)
        {
            for (int i = 0; i < 5; i++)
            {
                Acc.Deposit(100);
                Acc.Withdraw(50);
            }
        }
    }
    
}
