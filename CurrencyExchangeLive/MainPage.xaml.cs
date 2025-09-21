using System.Text.Json;

namespace CurrencyExchangeLive
{
    public partial class MainPage : ContentPage
    {
        private List<string> currencyList = new List<string>
        {
            "AED", "AFN", "ALL", "AMD", "ANG", "AOA", "ARS", "AUD", "AWG", "AZN", "BAM", "BBD", "BDT", "BGN",
            "BHD", "BIF", "BMD", "BND", "BOB", "BRL", "BSD", "BTN", "BWP", "BYN", "BZD", "CAD", "CDF", "CHF",
            "CLP", "CNY", "COP", "CRC", "CUP", "CVE", "CZK", "DJF", "DKK", "DOP", "DZD", "EGP", "ERN", "ETB",
            "EUR", "FJD", "FKP", "FOK", "GBP", "GEL", "GGP", "GIP", "GMD", "GNF", "GTQ", "GYD", "HKD", "HNL",
            "HRK", "HTG", "HUF", "IDR", "ILS", "IMP", "INR", "IQD", "IRR", "ISK", "JEP", "JMD", "JOD", "JPY",
            "KES", "KGS", "KHR", "KID", "KMF", "KRW", "KWD", "KYD", "KZT", "LAK", "LBP", "LKR", "LRD", "LSL",
            "LYD", "MAD", "MDL", "MGA", "MKD", "MMK", "MNT", "MOP", "MRU", "MUR", "MVR", "MWK", "MXN", "MYR",
            "MZN", "NAD", "NGN", "NIO", "NOK", "NPR", "NZD", "OMR", "PAB", "PEN", "PGK", "PHP", "PKR", "PLN",
            "PYG", "QAR", "RON", "RSD", "RUB", "RWF", "SAR", "SBD", "SCR", "SDG", "SEK", "SGD", "SHP", "SLE",
            "SLL", "SOS", "SRD", "SSP", "STN", "SYP", "SZL", "THB", "TJS", "TMT", "TND", "TOP", "TRY", "TTD",
            "TVD", "TWD", "TZS", "UAH", "UGX", "USD", "UYU", "UZS", "VES", "VND", "VUV", "WST", "XAF", "XCD",
            "XCG", "XDR", "XOF", "XPF", "YER", "ZAR", "ZMW", "ZWL"
        };

        private readonly HttpClient httpClient = new HttpClient();
        private const string ApiKey = "8870ef0ea4793f545f8302137f1e1b77";

        public MainPage()
        {
            InitializeComponent();
            FromCurrencyPicker.ItemsSource = currencyList;
            ToCurrencyPicker.ItemsSource = currencyList;

            apiResponseEditor.IsVisible = false;
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            // Parse decimal from User Entry
            if (!decimal.TryParse(ValueDecimal.Text, out decimal amount))
            {
                CurrencyResult.Text = "Invalid decimal value. Please try again.";
                return;
            }

            // Get selected currencies
            string? fromCurrency = FromCurrencyPicker.SelectedItem?.ToString();
            string? toCurrency = ToCurrencyPicker.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(fromCurrency) || string.IsNullOrEmpty(toCurrency))
            {
                CurrencyResult.Text = "Please select both currencies.";
                return;
            }

            // -----------------------------
            // Placeholder Logic
            // -----------------------------
            decimal placeholderRate = 1;   // Hardcoded for starter demo
            decimal placeholderConverted = amount * placeholderRate;

            CurrencyResult.Text = $"{amount:F2} {fromCurrency} is worth {placeholderConverted:F2} {toCurrency}. " +
                                  $"Exchange rate: 1:{placeholderRate:F4}";

            roboImage.Source = $"https://www.robohash.org/{placeholderConverted}{toCurrency}.png";
            roboName.Text = $"This robot is named '{placeholderConverted} {toCurrency}'."; 

            // -----------------------------
            // Live API Call (async)
            // -----------------------------
            /* try
            {
                string url = $"https://api.exchangerate.host/live?access_key={ApiKey}&source={fromCurrency}";
                var response = await httpClient.GetStringAsync(url);

                // Show raw JSON in collapsible editor
                apiResponseEditor.Text = JsonSerializer.Serialize(
                    JsonDocument.Parse(response).RootElement,
                    new JsonSerializerOptions { WriteIndented = true }
                );

                using var doc = JsonDocument.Parse(response);
                var root = doc.RootElement;

                string pairKey = $"{fromCurrency}{toCurrency}";
                decimal liveRate = root.GetProperty("quotes").GetProperty(pairKey).GetDecimal();
                decimal convertedAmount = amount * liveRate;

                // Update UI with real data
                CurrencyResult.Text = $"{amount:F2} {fromCurrency} is worth {convertedAmount:F2} {toCurrency}. " +
                                      $"Exchange rate: 1:{liveRate:F4}";

                roboImage.Source = $"https://www.robohash.org/{convertedAmount}{toCurrency}.png";
                roboName.Text = $"This robot is named '{convertedAmount} {toCurrency}'.";
            }
            catch (Exception ex)
            {
                CurrencyResult.Text = $"Error retrieving exchange rate: {ex.Message}";
            }*/
        }

        private void OnToggleJsonClicked(object sender, EventArgs e)
        {
            apiResponseEditor.IsVisible = !apiResponseEditor.IsVisible;
        }
    }
}
