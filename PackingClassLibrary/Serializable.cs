using Newtonsoft.Json;
public abstract class Serializable
{
    public string? ToJson()
    {
        try
        {
            return JsonConvert.SerializeObject(this);
        }
        catch (JsonSerializationException ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public static string ListToJson<T>(List<T> list)
    {
        return JsonConvert.SerializeObject(list);
    }

    public static T? FromJson<T>(string? json, Action<Exception>? onFailure = null)
    {
        if (json == null)
        {
            if (onFailure != null)
            {
                onFailure( new Exception("String to deserialize is null."));
            }
            return default(T);
        }
        try
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        catch (Exception ex)
        {
            if(onFailure != null)
            {
                onFailure(ex);
            }
            return default(T);
        }
    }
}
