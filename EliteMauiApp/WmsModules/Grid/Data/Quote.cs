using System;
using System.ComponentModel;
using System.IO;
using Elite.LMS.Maui.ViewModels;
using Newtonsoft.Json.Linq;

namespace Elite.LMS.Maui.WmsModules.Grid.Data {
    public class Quote : NotificationObject {
        string companyName = String.Empty;
        double price;
        double delta;
        double lowPrice;
        double highPrice;

        public string CompanyName {
            get { return companyName; }
            set {
                if (companyName == value)
                    return;

                this.companyName = value;
                OnPropertyChanged(nameof(CompanyName));
            }
        }

        public double Price {
            get { return price; }
            set {
                if (price == value)
                    return;

                this.price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public double Delta {
            get { return delta; }
            set {
                if (delta == value)
                    return;

                this.delta = value;
                OnPropertyChanged(nameof(Delta));
            }
        }

        public double LowPrice {
            get { return lowPrice; }
            set {
                if (lowPrice == value)
                    return;

                lowPrice = value;
                OnPropertyChanged(nameof(LowPrice));
            }
        }

        public double HighPrice {
            get { return highPrice; }
            set {
                if (highPrice == value)
                    return;

                highPrice = value;
                OnPropertyChanged(nameof(HighPrice));
            }
        }

        public Quote Clone() {
            Quote result = new Quote();
            result.CompanyName = this.CompanyName;
            result.Price = this.Price;
            result.Delta = this.Delta;
            result.LowPrice = this.LowPrice;
            result.HighPrice = this.HighPrice;
            return result;
        }
    }

    public class MarketSimulator {
        BindingList<Quote> quotes;
        readonly DateTime now;
        readonly Random random;

        public MarketSimulator() {
            this.now = DateTime.Now;
            this.random = new Random((int)now.Ticks);
            this.quotes = new BindingList<Quote>();
            PopulateQuotes();
        }

        public BindingList<Quote> Quotes { get { return quotes; } }

        void PopulateQuotes() {
            var assembly = this.GetType().Assembly;
            Stream stream = assembly.GetManifestResourceStream("StockSource.json");
            JObject jObject = JObject.Parse(new StreamReader(stream).ReadToEnd());
            quotes = jObject["StockItems"].ToObject<BindingList<Quote>>();
        }

        public void SimulateNextStep() {
            foreach (var item in quotes) {
                UpdateQuote(item);
            }
        }

        void UpdateQuote(Quote quote) {
            double value = quote.Price;

            int percentChange = random.Next(0, 201) - 100;
            double newValue = value + value * (5 * percentChange / 10000.0);
            if (newValue < 0)
                newValue = value - value * (5 * percentChange / 10000.0);

            quote.Price = newValue;
            quote.Delta = newValue - value;

            if (quote.LowPrice > quote.Price)
                quote.LowPrice = quote.Price;

            if (quote.HighPrice < quote.Price)
                quote.HighPrice = quote.Price;
        }
    }
}
