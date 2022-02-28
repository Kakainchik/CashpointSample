using CashpointWPF.ATMLogic;
using CashpointWPF.Resources;
using System;
using System.Globalization;
using System.Windows.Data;

namespace CashpointWPF.Converters
{
    public class ATMErrorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch((ATMError)value)
            {
                case ATMError.INVALID_INPUT:
                    {
                        return ErrorText.InvalidInput;
                    }
                case ATMError.NO_BANKNOTES:
                    {
                        return ErrorText.NoBanknotes;
                    }
                case ATMError.NO_MONEY_AT_ACCOUNT:
                    {
                        return ErrorText.NoMoneyAtAccount;
                    }
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}