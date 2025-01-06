using Youth.Helpers.Converters;
using Youth.Models.Base;
using Youth.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Youth.Models
{
    public class InvoiceProduct : BaseModel
    {
        static readonly string TAG = "X:" + typeof(InvoiceProduct).Name;

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("invoiceId")]
        public String invoiceId { get; set; }

        [JsonProperty("productId")]
        public int productId { get; set; }

        [JsonProperty("colorId")]
        public int colorId { get; set; }

        [JsonProperty("sizeId")]
        public int sizeId { get; set; }

        [JsonProperty("orderQty")]
        public int orderQty { get; set; }

        [JsonProperty("deleted")]
        public bool deleted { get; set; }

        [JsonProperty("deleteTime")]
        public String deleteTime { get; set; }

        [JsonProperty("deleterId")]
        public String deleterId { get; set; }

        [JsonProperty("created_at")]
        public String created_at { get; set; }

        [JsonProperty("updated_at")]
        public String updated_at { get; set; }

        [JsonProperty("categoryId")]
        public int categoryId { get; set; }

        [JsonProperty("subCategoryId")]
        public int subCategoryId { get; set; }

        [JsonProperty("brandId")]
        public int brandId { get; set; }

        [JsonProperty("label")]
        public String label { get; set; }

        [JsonProperty("description")]
        public String description { get; set; }

        [JsonProperty("imageUrl")]
        public String imageUrl { get; set; }

        [JsonProperty("costPrice")]
        public double costPrice { get; set; }

        [JsonProperty("sellPrice")]
        public double sellPrice { get; set; }

        [JsonProperty("onDiscount")]
        public bool onDiscount { get; set; }

        [JsonProperty("discountPrice")]
        public double discountPrice { get; set; }

        [JsonProperty("hasColorOptions")]
        public bool hasColorOptions { get; set; }

        [JsonProperty("colorOptionsRequired")]
        public bool colorOptionsRequired { get; set; }

        [JsonProperty("hasSizeOptions")]
        public bool hasSizeOptions { get; set; }

        [JsonProperty("sizeOptionsRequired")]
        public bool sizeOptionsRequired { get; set; }

        [JsonProperty("hasFeatureOptions")]
        public bool hasFeatureOptions { get; set; }

        [JsonProperty("featureOptionsRequired")]
        public bool featureOptionsRequired { get; set; }

        [JsonProperty("itemsInStock")]
        public int itemsInStock { get; set; }

        [JsonProperty("maxOrder")]
        public int maxOrder { get; set; }

        [JsonProperty("minOrder")]
        public int minOrder { get; set; }

        [JsonProperty("salesCount")]
        public int salesCount { get; set; }

        [JsonProperty("returnsCount")]
        public int returnsCount { get; set; }

        [JsonProperty("ratting")]
        public String ratting { get; set; }

        [JsonProperty("views")]
        public int views { get; set; }

        [JsonProperty("likes")]
        public int likes { get; set; }

        [JsonProperty("colorLabel")]
        public String colorLabel { get; set; }

        [JsonProperty("colorCode")]
        public String colorCode { get; set; }

        [JsonProperty("colorAdditionalPrice")]
        public double colorAdditionalPrice { get; set; }

        [JsonProperty("sizeLabel")]
        public String sizeLabel { get; set; }

        [JsonProperty("sizeAdditionalPrice")]
        public double sizeAdditionalPrice { get; set; }

        [JsonProperty("level")]
        public int level { get; set; }

        [JsonProperty("name")]
        public String name { get; set; }

        [JsonProperty("user_name")]
        public String user_name { get; set; }

        [JsonProperty("profile_picture")]
        public String profile_picture { get; set; }

        [JsonProperty("currency")]
        public String currency { get; set; }

        public static async Task<JObject> fetchInvoiceProducts(String invoiceId)
        {
            var valueTask = Constants.GetAuthTocken();
            var access_token = await valueTask;

            using (var client = new HttpClient())
            {
                // Set the base address for the API
                client.BaseAddress = new Uri(Constants.apiUrl);

                // Add the authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                // Make the request and get the response
                HttpResponseMessage response = await client.GetAsync(Constants.API_FETCH_INVOICE_PRODUCTS + invoiceId);

                // Read the response content
                string result = await response.Content.ReadAsStringAsync();

                // Return the response content
                JObject obj = JObject.Parse(result);
                return obj;
            }
        }
    }
}
