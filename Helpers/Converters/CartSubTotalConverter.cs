using Youth.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Youth.Helpers.Converters
{
    internal class CartSubTotalConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            double priceToPay = 0;
            if (value != null)
            {
                try
                {
                    ShoppingCart shoppingCart = value as ShoppingCart;

                    if (shoppingCart.onDiscount)
                    {
                        priceToPay = shoppingCart.discountPrice * shoppingCart.orderQty;
                    }
                    else
                    {
                        priceToPay = shoppingCart.sellPrice * shoppingCart.orderQty;
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
