using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new HttpClient();
        var response = await client.GetAsync("https://api.kucoin.com/api/v1/market/stats?symbol=BTC-USDT", HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<ApiResponse>(stream);

        var price = result.Data["BTC"].Quote["USD"].Price;
        var change24h = result.Data["BTC"].Quote["USD"].PercentChange24h;

        Console.WriteLine($"Price: {price} USD");
        Console.WriteLine($"Change (24h): {change24h}%");
    }
}

class ApiResponse
{
    public Dictionary<string, CoinData> Data { get; set; }
}

class CoinData
{
    public Dictionary<string, QuoteData> Quote { get; set; }
}

class QuoteData
{
    public decimal Price { get; set; }
    public decimal PercentChange24h { get; set; }
}
