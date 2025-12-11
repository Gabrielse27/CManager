using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CManager.Infrastructure.Formatters
{
    public class JsonFormatter
    {
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public string Serialize<T>(T data)
        {
            return JsonSerializer.Serialize(data, _options);
        }

        public T? Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, _options);
        }


    }
}
