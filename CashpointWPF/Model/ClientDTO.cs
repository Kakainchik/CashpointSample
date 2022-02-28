using CashpointWPF.Core;

namespace CashpointWPF.Model
{
    public class ClientDTO : ObservableObject
    {
        private string name;
        private AccountDTO account;

        public int Id { get; set; }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public AccountDTO Account
        {
            get => account;
            set
            {
                account = value;
                OnPropertyChanged(nameof(Account));
            }
        }

        public ClientDTO(string name, AccountDTO account, int id = default)
        {
            Name = name;
            Account = account;
            Id = id;
        }
    }
}