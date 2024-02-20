using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Elite.LMS.Maui.WmsModules.Grid.Data {
    public class InvoicesRepository {
        public IList<Invoice> Invoices { get; private set; }

        public InvoicesRepository() {
            System.Reflection.Assembly assembly = GetType().Assembly;
            Stream stream = assembly.GetManifestResourceStream("Invoices.json");
            JObject jObject = JObject.Parse(new StreamReader(stream).ReadToEnd());
            Invoices = jObject[nameof(Invoices)].ToObject<List<Invoice>>();
        }
    }
}
