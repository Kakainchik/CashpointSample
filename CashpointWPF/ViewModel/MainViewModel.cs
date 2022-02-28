#nullable disable

using CashpointWPF.Core;
using CashpointWPF.DB;
using CashpointWPF.DB.Entities;
using CashpointWPF.Model;
using CashpointWPF.Model.Mappers;
using CashpointWPF.ATMLogic;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Collections;
using System;
using CashpointWPF.Resources;

namespace CashpointWPF.ViewModel
{
    public class MainViewModel : ObservableObject, INotifyDataErrorInfo
    {
        #region Variables

        private const decimal DEFAULT_AMOUNT = 7654M;

        private readonly ErrorViewModel errorVM;

        private ClientMapper clientMapper;
        private ApplicationContext db;
        private IMachine machine;

        private string newClientName;
        private ClientDTO selectedClient;
        private int withdrawAmount;
        private string withdrawText;
        private ATMError machineError;
        private IDictionary<int, int> banknotesState;

        #endregion

        #region Properties

        public string NewClientName
        {
            get => newClientName;
            set
            {
                newClientName = value;
                OnPropertyChanged(nameof(NewClientName));
            }
        }

        public ClientDTO SelectedClient
        {
            get => selectedClient;
            set
            {
                selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
            }
        }

        public int WithdrawAmount
        {
            get => withdrawAmount;
            set
            {
                withdrawAmount = value;
                if(withdrawAmount <= 0)
                {
                    errorVM.AddError(nameof(WithdrawText), ErrorText.InvalidInput);
                }
            }
        }

        public string WithdrawText
        {
            get => withdrawText;
            set
            {
                withdrawText = value;
                OnPropertyChanged(nameof(WithdrawText));

                errorVM.ClearErrors(nameof(WithdrawText));
                //As MVVM in WPF does not allow validate UI directly we do this through string
                if(!int.TryParse(value, out withdrawAmount))
                {
                    errorVM.AddError(nameof(WithdrawText), ErrorText.InvalidInput);
                }
            }
        }

        public ATMError MachineError
        {
            get => machineError;
            set
            {
                machineError = value;
                OnPropertyChanged(nameof(MachineError));
            }
        }

        public IDictionary<int, int> BanknotesState
        {
            get => banknotesState;
            set
            {
                banknotesState = value;
                OnPropertyChanged(nameof(BanknotesState));
            }
        }

        public bool HasErrors => errorVM.HasErrors;
        public bool CanWithdraw => !HasErrors;

        public ObservableCollection<ClientDTO> Clients { get; set; }

        public ICommand CreateClientCommand { get; set; }
        public ICommand WithdrawCommand { get; set; }

        #endregion

        public MainViewModel(ApplicationContext context)
        {
            db = context;
            var dtos = db.Clients.Include(c => c.Account)
                .ToList();

            errorVM = new ErrorViewModel();
            clientMapper = new ClientMapper();
            banknotesState = new Dictionary<int, int>();
            InitializeCash();

            Clients = new ObservableCollection<ClientDTO>();
            machine = new WindowsATM(banknotesState);
            WithdrawText = string.Empty;

            //Load all clients into listView
            dtos.ForEach(c => Clients.Add(clientMapper.ToModel(c)));

            CreateClientCommand = new RelayCommand(OnCreateClient);
            WithdrawCommand = new RelayCommand(OnWithdraw);

            errorVM.ErrorsChanged += ErrorVM_ErrorsChanged;
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return errorVM.GetErrors(propertyName);
        }

        private async void OnCreateClient(object obj)
        {
            var name = obj as string;
            var client = new Client
            {
                Name = name,
                Account = new Account { Balance = DEFAULT_AMOUNT }
            };

            //Put new client into model
            var result = await db.Clients.AddAsync(client);
            await db.SaveChangesAsync();

            Clients.Add(clientMapper.ToModel(result.Entity));
        }

        private async void OnWithdraw(object obj)
        {
            if(SelectedClient.Account.Balance < WithdrawAmount)
            {
                MachineError = ATMError.NO_MONEY_AT_ACCOUNT;
                return;
            }

            try
            {
                machine.Withdraw(WithdrawAmount);

                var acc = await db.Accounts.SingleOrDefaultAsync(
                    p => p.Id == SelectedClient.Account.Id);
                if(acc != null)
                {
                    acc.Balance -= WithdrawAmount;
                    await db.SaveChangesAsync();

                    //Update visual state
                    SelectedClient.Account.Balance = acc.Balance;
                    //There is only way to update Dictionary in WPF
                    BanknotesState = new Dictionary<int, int>(machine.Cash);

                    //Reset error message
                    MachineError = ATMError.NONE;
                }
            }
            catch(WithdrawException ex)
            {
                MachineError = ex.Error;
            }
        }

        ///<remarks>
        ///A storing number of banknotes in Database is not a good idea,
        ///it has to belong to a certain ATM, therefore we simulate it
        ///</remarks>
        private void InitializeCash()
        {
            //Initial banknotes count
            const int cashCount = 5;

            banknotesState.Add(BanknoteDetails.B200, cashCount);
            banknotesState.Add(BanknoteDetails.B100, cashCount);
            banknotesState.Add(BanknoteDetails.B50, cashCount);
            banknotesState.Add(BanknoteDetails.B20, cashCount);
            banknotesState.Add(BanknoteDetails.B10, cashCount);

        }

        private void ErrorVM_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(CanWithdraw));
        }
    }
}