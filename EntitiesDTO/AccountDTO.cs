using BankSystem.Entities;

namespace BankSystem.EntitiesDTO
{
    public class AccountDTO
    {
        public string accountNumber { get; set; }
        public decimal balance { get; set; }
        public int bankId { get; set; }
        public int customerId { get; set; }


    }
}
