using Newtonsoft.Json;
public class Serializable
{
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static T? FromJson<T>(string json, Action<string>? onFailure = null)
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        catch (JsonSerializationException ex)
        {
            if(onFailure != null)
            {
                onFailure(ex.Message);
            }
            return default(T);
        }
    }
}
