using UnityEngine;
using System.IO;
public static class JsonSaveManager<T>
{
    static string SavePath(string path) => $"{Application.dataPath}/{path}.json";      
    
    public static void Save(T data, string path)
    {
        using (StreamWriter sw = new StreamWriter(SavePath(path), false))
        {
            string jsonstr = JsonUtility.ToJson(data, true);
            sw.Write(jsonstr);
            sw.Flush();
        }
    }

    public static T Load(string path)
    {
        if (File.Exists(SavePath(path)))
        {
            using (StreamReader sr = new StreamReader(SavePath(path)))
            {
                string datastr = sr.ReadToEnd();
                return JsonUtility.FromJson<T>(datastr);
            }
        }

        return default;
    }
}