using System.IO;
using System.Text.Json;

namespace BiliBili_Anchor_Assistant.Helper
{
    public class JsonHelper<T>
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

        public JsonHelper(string filePath)
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
        }

        public List<T> LoadConfig()
        {
            if (!File.Exists(_filePath))
            {
                return new List<T>();
            }
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public void SaveConfig(List<T> config)
        {
            string json = JsonSerializer.Serialize(config, _jsonSerializerOptions);
            File.WriteAllText(_filePath, json);
        }
    }
}
