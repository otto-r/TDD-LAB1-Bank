using System;
using Xunit;
using BankAccountLab;

namespace BankTest
{
    public class BankTests
    {
        #region Contructor
        [Theory]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        public void ContructorTest(double r)
        {
            Assert.Throws<Exception>(() => new Account(100, r));
        }
        #endregion

        #region Deposit
        //TODO: Limit to 14 significant digits
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.NaN)]
        //[InlineData(double.Epsilon)]
        public void DepositTest(double amount)
        {
            Account account = TestAccount.CreateAccount(100);
            Assert.Throws<Exception>(() => account.Deposit(amount));
        }

        [Fact]
        public void DepositSuccessful()
        {
            double amount = 10;
            double initialBalance = 100;
            Account account = TestAccount.CreateAccount(initialBalance);
            account.Deposit(amount);
            Assert.Equal(account.Balance, (initialBalance + amount));
        }
        #endregion

        #region Withdraw
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(101)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.NaN)]
        public void WithdrawTest(double amount)
        {
            Account account = TestAccount.CreateAccount(100);
            Assert.Throws<Exception>(() => account.Withdraw(amount));
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        public void WithdrawSuccesful(double amount)
        {
            double initialBalance = 100;
            Account account = TestAccount.CreateAccount(initialBalance);
            account.Withdraw(amount);
            Assert.Equal(account.Balance, (initialBalance - amount));
        }
        #endregion

        #region Transfer
        [Theory]
        [InlineData(0, 100, 10)]
        [InlineData(100, 100, -1)]
        [InlineData(100, 100, 0)]
        [InlineData(100, 100, double.PositiveInfinity)]
        [InlineData(100, 100, double.NegativeInfinity)]
        [InlineData(100, 100, double.NaN)]
        public void TransferTest(double initialBalanceSenderAccount, double initialBalanceRecipientAccount, double transferAmount)
        {
            Account accountSender = new Account(initialBalanceSenderAccount);
            Account accountRecipient = new Account(initialBalanceRecipientAccount);
            Assert.Throws<Exception>(() => accountSender.Transfer(accountRecipient, transferAmount));
        }

        [Fact]
        public void TransferToSameAccount()
        {
            Account accountSender = new Account(100);
            Assert.Throws<Exception>(() => accountSender.Transfer(accountSender, 10));
        }

        [Fact]
        public void TransferToNullAccount()
        {
            Account accountSender = new Account(100);
            Assert.Throws<Exception>(() => accountSender.Transfer(null, 10));
        }

        [Fact]
        public void TransferCheckReturnValue()
        {
            Account accountSender = new Account(100);
            Account accountRecipient = new Account(100);
            Assert.True(accountSender.Transfer(accountRecipient, 10));
        }

        [Fact]
        public void TransferSumMatchTest()
        {
            double initialBalance = 100;
            double transferAmount = 10;
            Account accountSender = new Account(initialBalance);
            Account accountRecipient = new Account(initialBalance);
            accountSender.Transfer(accountRecipient, transferAmount);
            Assert.Equal((accountRecipient.Balance + accountSender.Balance), initialBalance * 2);
        }

        [Fact]
        public void TransferSenderDebitTest()
        {
            double initialBalance = 100;
            double transferAmount = 10;
            double expectedBalanceSender = 90;
            Account accountSender = new Account(initialBalance);
            Account accountRecipient = new Account(initialBalance);
            accountSender.Transfer(accountRecipient, transferAmount);
            Assert.Equal(accountSender.Balance, expectedBalanceSender);
        }

        [Fact]
        public void TransferSenderCreditTest()
        {
            double initialBalance = 100;
            double transferAmount = 10;
            double expectedBalanceRecipient = 110;
            Account accountSender = new Account(initialBalance);
            Account accountRecipient = new Account(initialBalance);
            accountSender.Transfer(accountRecipient, transferAmount);
            Assert.Equal(accountRecipient.Balance, expectedBalanceRecipient);
        }
        #endregion

        #region CalculateInterest
        [Theory]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        public void InterestTest(double interestRate)
        {
            Assert.Throws<Exception>(() =>
            {
                Account account = new Account(100, interestRate);
                account.CalculateInterest();
            });
        }

        [Theory]
        [InlineData(100, 0.015)]
        [InlineData(100, 0.00)]
        [InlineData(100, -0.02)]
        [InlineData(100, -1.0)]
        [InlineData(0, 0.015)]
        [InlineData(0, 0.00)]
        [InlineData(0, -0.02)]
        public void CalculateInterestBalanceAfterInterestPayment(double initialBalance, double r)
        {
            double expectedBalance = initialBalance * (1 + r);
            Account account = new Account(initialBalance, r);
            account.CalculateInterest();
            Assert.Equal(expectedBalance, account.Balance);
        }

        [Fact]
        public void CalculateInterestAccrued()
        {
            double r = -0.02;
            double initialBalance = 100;
            double expectedInterestAccrued = initialBalance * r;
            Account account = new Account(initialBalance, r);
            double interestAccrued = account.CalculateInterest();
            Assert.Equal(expectedInterestAccrued, interestAccrued);
        }
        #endregion
    }
}
