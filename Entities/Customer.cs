namespace BankSystem.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string phone { get; set; }
        public string? address { get; set; }
        public string? gmail { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public Account? account { get; set; }
        public int accountId { get; set; }
        public int BandId { get; set; }
        public Bank bank { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();


        public override string ToString()
        {
            Console.WriteLine("______________________________________________________________________________________________________________________");
            return $"[{Id}]\t\t[{Name}]\t\t[{phone}]\t\t[{address}]\t\t[{gmail}]\n\n[{dateOfBirth}]\t\t[{accountId}]\t\t[{BandId}]\n";
        }

    }
}
