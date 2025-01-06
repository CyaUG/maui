using Youth.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Youth.Helpers.Converters
{
    internal class EventCartSubTotalConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            double priceToPay = 0;
            if (value != null)
            {
                try
                {
                    EventCart eventCart = value as EventCart;

                    if (eventCart.onDiscount)
                    {
                        priceToPay = eventCart.discountPrice * eventCart.orderQty;
                    }
                    else
                    {
                        priceToPay = eventCart.amount * eventCart.orderQty;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("IsViewerConverter: " + ex);
                }
            }

            return priceToPay;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();

        }

    }
}
