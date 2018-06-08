using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccountLab
{
    public class Account
    {
        public double Balance { get; private set; }
        public double InterestRate { get; set; }

        public Account(double initBalance)
        {
            Balance = initBalance;
        }

        public Account(double initBalance, double r)
        {
            if (double.IsNaN(r))
                throw new Exception("Interest rate cannot be NaN.");
            if (double.IsPositiveInfinity(r) || double.IsNegativeInfinity(r))
                throw new Exception("Interest rate cannot be set to infinity.");
            Balance = initBalance;
            InterestRate = r;
        }

        //Methods
        public void Deposit(double amount)
        {
            if (double.IsNaN(amount))
                throw new Exception("Deposit must be a value.");
            if (double.IsInfinity(amount) || double.IsNegativeInfinity(amount))
                throw new Exception("Cannot deposit infinity or negative infinity");
            if (amount <= 0)
                throw new Exception("Cannot deposit zero or negative.");
            Balance += amount;
        }

        public void Withdraw(double amount)
        {
            if (double.IsNaN(amount))
                throw new Exception("Must withdraw a value.");
            if (double.IsInfinity(amount) || double.IsNegativeInfinity(amount))
                throw new Exception("Cannot withdraw infinity or negative infinity");
            if (amount > Balance)
                throw new Exception("Withdrawal cannot exceed account balance.");
            if (amount <= 0)
                throw new Exception("Cannot withdraw zero or negative.");
            Balance -= amount;
        }

        public bool Transfer(Account recipientAccount, double amountBeingTransferd)
        {
            if (recipientAccount == this)
                throw new Exception("Recipient account cannot be equal to sender account.");
            if (recipientAccount == null)
                throw new Exception("There is no recipient account.");
            if (double.IsInfinity(amountBeingTransferd) || double.IsNegativeInfinity(amountBeingTransferd))
                throw new Exception("Cannot transfer infinity of one sort or the other.");
            if (amountBeingTransferd <= 0 || double.IsNaN(amountBeingTransferd))
                throw new Exception("Cannot transfer zero, negative or NaN amount");
            if (Balance < amountBeingTransferd)
                throw new Exception($"Not sufficient funds in account. \nBalance: {Balance} \nAmount attempted to transfer: {amountBeingTransferd}");
            Balance -= amountBeingTransferd;
            recipientAccount.Balance += amountBeingTransferd;
            return true;
        }

        public double CalculateInterest()
        {
            double interestAccrued = Balance * InterestRate;
            Balance = (InterestRate + 1) * Balance;
            return interestAccrued;
        }
    }
}
