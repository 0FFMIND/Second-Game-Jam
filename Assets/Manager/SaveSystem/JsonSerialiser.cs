using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class JsonSerialiser
{
    private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings();
    private static JsonSerializer _serializer = JsonSerializer.Create(_settings);
    public static string Serialize<T>(T value)
    {
        return JsonConvert.SerializeObject(value, _settings);
    }
    public static JToken SerializeKey<T>(T key)
    {
        return JToken.FromObject(key, _serializer);
    }
    public static T DeserializeKey<T>(string key, JObject data)
    {
        return data[key].ToObject<T>(_serializer);
    }

}
