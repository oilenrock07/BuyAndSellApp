using HtmlAgilityPack;

namespace BuyAndSellApp.BusinessLogic.Extensions
{
    public static class HtmlNodeExtension
    {
        public static string Attribute(this HtmlNode node, string attributeName)
        {
            if (node != null)
                return node.Attributes[attributeName].Value.Trim();

            return "";
        }
    }
}
