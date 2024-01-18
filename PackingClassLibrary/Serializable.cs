using Newtonsoft.Json;
public abstract class Serializable
{
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static string ListToJson<T>(List<T> list)
    {
        return JsonConvert.SerializeObject(list);
    }

    public static T? FromJson<T>(string? json, Action<string>? onFailure = null)
    {
        if (json == null)
        {
            if (onFailure != null)
            {
                onFailure( "String to deserialize is null.");
            }
            return default(T);
        }
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
