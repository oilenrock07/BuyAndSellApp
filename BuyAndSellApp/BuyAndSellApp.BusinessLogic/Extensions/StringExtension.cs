namespace BuyAndSellApp.BusinessLogic.Extensions
{
    public static class StringExtension
    {
        public static string CleanPrice(this string price)
        {
            if (!string.IsNullOrEmpty(price))
            {
                if (price.Contains("?"))
                    price = price.Replace("?", "");

                return price.Trim();
            }

            return "";
        }
    }
}
