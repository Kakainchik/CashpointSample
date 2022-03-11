using System.Collections.Generic;
using System.Linq;

namespace CashpointWPF.ATMLogic
{
    public class WindowsATM : IMachine
    {
        public IDictionary<int, int> Cash { get; set; }

        public WindowsATM(IDictionary<int, int> cash)
        {
            Cash = cash;
        }

        public IDictionary<int, int> Withdraw(int amount)
        {
            if(amount <= 0)
                throw new WithdrawException(ATMError.INVALID_INPUT,
                    "Amount cannnot be zero or less.");
            
            Dictionary<int, int> copy = new Dictionary<int, int>(Cash);
            Dictionary<int, int> rest = new Dictionary<int, int>();
            GoThroughBanknote(copy, rest, amount);

            rest = rest.Where(kvp => kvp.Value != 0)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            //Return banknotes
            return rest;
        }

        private void GoThroughBanknote(IDictionary<int, int> source,
            IDictionary<int, int> result,
            int amount)
        {
            int remains, max = 0;

            if(source.Count > result.Count)
            {
                //Look for a max banknote that was not used yet
                max = source.Where(pair => !result.ContainsKey(pair.Key))
                    .ToDictionary(pair => pair.Key, pair => pair.Value).Keys
                    .Max();
            }
            else
            {
                throw new WithdrawException(ATMError.NO_BANKNOTES,
                    "Not possible to withdraw this amount.");
            }

            //Required quantity of banknotes
            KeyValuePair<int, int> reqB = new KeyValuePair<int, int>(max, amount / max);

            //If there are not enough banknotes then withdraw all of them
            if(reqB.Value > source[reqB.Key])
            {
                remains = amount - reqB.Key * source[reqB.Key];
                reqB = new KeyValuePair<int, int>(reqB.Key, source[reqB.Key]);
            }
            else remains = amount - reqB.Key * reqB.Value;

            //If there is no more money to withdraw
            if(remains == 0)
            {
                source[reqB.Key] -= reqB.Value;
                result.Add(reqB.Key, reqB.Value);

                ConfirmWithdrawing(result);
                return;
            }

            //If rest amount is less than the smallest banknote
            if(remains < source.Keys.Min())
            {
                reqB = new KeyValuePair<int, int>(reqB.Key, reqB.Value - 1);
                result.Add(reqB.Key, reqB.Value);

                GoThroughBanknote(source, result, amount - reqB.Key * reqB.Value);
                return;
            }

            source[reqB.Key] -= reqB.Value;
            result.Add(reqB.Key, reqB.Value);

            GoThroughBanknote(source, result, remains);
        }

        private void ConfirmWithdrawing(IDictionary<int, int> actual)
        {
            foreach(var pair in actual)
            {
                Cash[pair.Key] -= pair.Value;
            }
        }
    }
}