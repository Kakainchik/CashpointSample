using System.Collections.Generic;

namespace CashpointWPF.ATMLogic
{
    public interface IMachine
    {
        IDictionary<int, int> Cash { get; set; }

        IDictionary<int, int> Withdraw(int amount);
    }
}