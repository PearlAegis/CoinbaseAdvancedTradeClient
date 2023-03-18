﻿using CoinbaseAdvancedTradeClient.Models.Api.Products;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Pages
{
    public class ProductsPage : Page
    {
        [JsonProperty("product")]
        public Product Product { get; set; }

        [JsonProperty("products")]
        public List<Product> Products { get; set; }

        [JsonProperty("num_products")]
        public int NumberReturned { get; set; }
    }
}
