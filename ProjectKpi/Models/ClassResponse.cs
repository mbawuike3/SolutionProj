namespace ProjectKpi.Models
{
    public class ClassResponse
    {
        public List<Data>? data { get; set; }
        public long timestamp { get; set; }
        public string? code {  get; set; }
        public string? Errormessage { get; set; }
    }
    public class Data
    {
        public string id { get; set; } = string.Empty;
        public string rank { get; set; } = string.Empty;
        public string symbol { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string supply { get; set; } = string.Empty;
        public string maxSupply { get; set; } = string.Empty;
        public string marketCapUsd { get; set; } = string.Empty;
        public string volumeUsd24Hr { get; set; } = string.Empty;
        public string priceUsd { get; set; } = string.Empty;
        public string changePercent24Hr { get; set; } = string.Empty;
        public string vwap24Hr { get; set; } = string.Empty;
        public string explorer { get; set; } = string.Empty;
    }
}
