using SealMarket.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SealMarket.Core.Entities
{
    public class Account : IAccount
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public decimal Balance { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public Account
        (
            int id, 
            int userId, 
            User user,
            decimal balance, 
            string login, 
            string passwordHash,
            string emailAddress,
            string phoneNumber
        )
        {
            Id = id;
            UserId = userId;
            User = user;
            Balance = balance;
            Login = login;
            PasswordHash = passwordHash;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
        }

        public void AddMoney(decimal quantity) => Balance += quantity;
   
        public void WithdrawMoney(decimal quantity)
        {
            if (quantity <= 0) 
                throw new ArgumentException("Quantity must be positive number.");

            if (quantity > Balance) 
                throw new ArgumentException("Quantity must not be more than balance.");

            Balance -= quantity;
        }

        public bool ChangePassword(string passwordHash) 
        {
            return true;
        }
    }
}
