
namespace BankSystem.Entities
{
    public class Bank
    {
        public int Id { get; set; }
        public string NameBank { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public float NumberBranches { get; set; } = 0f;
        public ICollection<Account> accounts { get; set; } = new List<Account>();
        public ICollection<Customer> customers { get; set; } = new List<Customer>();

        public override string ToString()
        {
            Console.WriteLine("__________________________________________________________________________________________");
            return $"[{Id}]\t\t[{NameBank}]\t\t[{phone}]\t\t[{address}]\t\t[{NumberBranches}]\n";
        }

    }
}
