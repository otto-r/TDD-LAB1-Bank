using System;

namespace BankAccountLab
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Account account = new Account(double.MaxValue/2, -1);

            Console.WriteLine(account.Balance);
            account.Deposit(double.MaxValue/2);
            account.Deposit(double.MaxValue/20);
            Console.WriteLine(account.Balance);

            

        }
    }
}
