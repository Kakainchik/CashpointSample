namespace CashpointWPF.DB.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public Account Account { get; set; } = null!;
    }
}