using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SealMarket.Core.Interfaces.Entities
{
    public interface IAccount
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        public string Login { get; set; }
        public string PasswordHash {  get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public void AddMoney(decimal quantity);
        public void WithdrawMoney(decimal quantity);
        public bool ChangePassword(string passwordHash);
    }
}
