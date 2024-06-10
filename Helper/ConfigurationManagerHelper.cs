using System.Dynamic;
using System.IO;
using System.Reflection;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BiliBili_Anchor_Assistant.Helper
{
    public class ConfigurationManagerHelper<T>
    {
        private readonly T _defaultConfig;
        private readonly string _filePath;
        private readonly Json _json = new();
        private readonly bool _mode;

        public ConfigurationManagerHelper(string filePath, T defaultConfig, bool mode)
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            _defaultConfig = defaultConfig;
            _mode = mode;
        }

        public T LoadConfig()
        {
            if (!File.Exists(_filePath))
            {
                return _defaultConfig;
            }
            string configData = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<T>(configData) ?? _defaultConfig;
        }

        public void SaveConfig(T config)
        {
            if (_mode)
            {
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var property in properties)
                {
                    if (property.CanRead)
                    {
                        object oldData = property.GetValue(LoadConfig());
                        object newData = property.GetValue(config);
                        if (!string.IsNullOrEmpty(oldData?.ToString())
                            && !string.IsNullOrEmpty(newData?.ToString())
                            && oldData != newData)
                            _json.Set(property.Name, newData);
                        else
                            _json.Set(property.Name, oldData);
                    }
                }
                File.WriteAllText(_filePath, JsonSerializer.Serialize(_json.Get));
            }
            else
            {
                string json = JsonSerializer.Serialize(config);
                File.WriteAllText(_filePath, json);
            }
        }
    }

    public class Json
    {

        public ExpandoObject Get { get; } = new();

        public void Set(string key, object value)
        {
            (Get as IDictionary<string, object>)[key] = value;
        }
    }
}
