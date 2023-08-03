namespace BankSystem.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal amount { get; set; }
        public DateTime? dateTime { get; set; }
        public ICollection<Customer> Customers { get; set; } = new List<Customer>();

        public override string ToString()
        {
            Console.WriteLine("__________________________________________________________________________________________");
            return $"[{Id}]\t\t[{amount}]\t\t[{dateTime}]\n";
        }

    }
}
