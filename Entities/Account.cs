namespace BankSystem.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string accountNumber { get; set; }
        public decimal balance { get; set; }
        public Bank? bank { get; set; }
        public int bankId { get; set; }
        public Customer? customer { get; set; }
        public int customerId { get; set; }

        public override string ToString()
        {
            Console.WriteLine("__________________________________________________________________________________________");
            return $"[{Id}]\t\t[{accountNumber}]\t\t[{balance}]\t\t[{bankId}]\t\t[{customerId}]\n";
        }
    }
}
