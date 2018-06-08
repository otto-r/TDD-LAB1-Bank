using System;
using System.Collections.Generic;
using System.Text;
using BankAccountLab;

namespace BankTest
{
    public class TestAccount
    {
        public static Account CreateAccount(double initialBalannce)
        {
            return new Account(initialBalannce);
        }
    }
}
