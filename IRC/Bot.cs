using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IRC
{
    public class Bot
    {
        public static async Task<string> Execute(string command)
        {
            if (command == "execute")
            {
                var url = "https://stockx.com/api/browse?productCategory=sneakers&sort=most-active&order=DESC";
                var http = new HttpClient();
                var result = await http.GetAsync(url);
                var json = await result.Content.ReadAsStringAsync();
                Rootobject products = JsonConvert.DeserializeObject<Rootobject>(json);
                var sb = new StringBuilder();
                for (int i = 0; i < 3; i++)
                {
                    sb.Append(
                        i + 1 + ") " +
                        products.Products[i].shoe + " " +
                        products.Products[i].name + " " +
                        products.Products[i].retailPrice + "$ ");
                }
                return sb.ToString();

            }

            return "Unknown command";
        }
    }


    public class Rootobject
    {
        public Product[] Products { get; set; }
    }

    public class Product
    {
        public string id { get; set; }
        public string uuid { get; set; }
        public string brand { get; set; }
        public object[] breadcrumbs { get; set; }
        public string category { get; set; }
        public int charityCondition { get; set; }
        public object childId { get; set; }
        public string colorway { get; set; }
        public string condition { get; set; }
        public object countryOfManufacture { get; set; }
        public string dataType { get; set; }
        public object description { get; set; }
        public bool hidden { get; set; }
        public object ipoDate { get; set; }
        public int minimumBid { get; set; }
        public string gender { get; set; }
        public object[] doppelgangers { get; set; }
        public Media media { get; set; }
        public string name { get; set; }
        public string productCategory { get; set; }
        public string releaseDate { get; set; }
        public int releaseTime { get; set; }
        public int retailPrice { get; set; }
        public string shoe { get; set; }
        public string shortDescription { get; set; }
        public string styleId { get; set; }
        public string tickerSymbol { get; set; }
        public string title { get; set; }
        public Trait[] traits { get; set; }
        public int type { get; set; }
        public string urlKey { get; set; }
        public int year { get; set; }
        public object shoeSize { get; set; }
        public Market market { get; set; }
        public string[] _tags { get; set; }
        public bool lock_selling { get; set; }
        public string[] selling_countries { get; set; }
        public string[] buying_countries { get; set; }
        public string objectID { get; set; }
    }

    public class Media
    {
        public string imageUrl { get; set; }
        public string smallImageUrl { get; set; }
        public string thumbUrl { get; set; }
        public object[] gallery { get; set; }
        public bool hidden { get; set; }
    }

    public class Market
    {
        public int productId { get; set; }
        public object skuUuid { get; set; }
        public string productUuid { get; set; }
        public int lowestAsk { get; set; }
        public string lowestAskSize { get; set; }
        public int parentLowestAsk { get; set; }
        public int numberOfAsks { get; set; }
        public int salesThisPeriod { get; set; }
        public object salesLastPeriod { get; set; }
        public int highestBid { get; set; }
        public string highestBidSize { get; set; }
        public int numberOfBids { get; set; }
        public int annualHigh { get; set; }
        public int annualLow { get; set; }
        public int deadstockRangeLow { get; set; }
        public int deadstockRangeHigh { get; set; }
        public float volatility { get; set; }
        public int deadstockSold { get; set; }
        public float pricePremium { get; set; }
        public int averageDeadstockPrice { get; set; }
        public int lastSale { get; set; }
        public string lastSaleSize { get; set; }
        public int salesLast72Hours { get; set; }
        public int changeValue { get; set; }
        public float changePercentage { get; set; }
        public float absChangePercentage { get; set; }
        public int totalDollars { get; set; }
        public int updatedAt { get; set; }
        public int lastLowestAskTime { get; set; }
        public int lastHighestBidTime { get; set; }
        public DateTime lastSaleDate { get; set; }
        public DateTime createdAt { get; set; }
        public int deadstockSoldRank { get; set; }
        public int pricePremiumRank { get; set; }
        public int averageDeadstockPriceRank { get; set; }
        public object featured { get; set; }
    }

    public class Trait
    {
        public bool filterable { get; set; }
        public bool highlight { get; set; }
        public string name { get; set; }
        public object value { get; set; }
        public bool visible { get; set; }
        public string format { get; set; }
    }

}
