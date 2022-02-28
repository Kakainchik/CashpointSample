using CashpointWPF.Core;

namespace CashpointWPF.Model
{
    public class AccountDTO : ObservableObject
    {
        private decimal balanse;

        public int Id { get; set; }

        public decimal Balance
        {
            get => balanse;
            set
            {
                balanse = value;
                OnPropertyChanged(nameof(Balance));
            }
        }

        public AccountDTO(decimal amount = default, int id = default)
        {
            Id = id;
            balanse = amount;
        }
    }
}