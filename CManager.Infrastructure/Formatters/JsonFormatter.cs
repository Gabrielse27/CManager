using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CManager.Infrastructure.Formatters
{
    public class JsonFormatter
    {

        // Vi skapar inställningar för hur JSON-koden ska se ut.
        // "WriteIndented = true" gör att JSON-texten blir snygg och läsbar
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        // En metod för att omvandla (Serialisera) ett objekt till en textsträng (JSON).
        // <T> betyder att metoden är Generisk – den fungerar för ALLA typer av objekt 
        // (t.ex. Customer, Product, List<Customer> osv).
        public string Serialize<T>(T data)
        {
            // Här använder vi System.Text.Json för att göra om objektet "data" till en sträng.
            return JsonSerializer.Serialize(data, _options);
        }

        // En metod för att omvandla tillbaka (Deserialisera) en textsträng till ett objekt.
        // Den tar emot JSON-text och skapar ett C#-objekt av typen T.
        public T? Deserialize<T>(string json)
        {
            // Här läser vi textsträngen och bygger upp objektet igen.
            return JsonSerializer.Deserialize<T>(json, _options);
        }


    }
}
