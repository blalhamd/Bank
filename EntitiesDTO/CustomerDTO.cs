using BankSystem.Entities;

namespace BankSystem.EntitiesDTO
{
    public class CustomerDTO
    {
        public string Name { get; set; }
        public string phone { get; set; }
        public string? address { get; set; }
        public string? gmail { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public int accountId { get; set; }
        public int BandId { get; set; }
    }
}
