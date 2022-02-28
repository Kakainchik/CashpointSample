namespace CashpointWPF.DB.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;
    }
}