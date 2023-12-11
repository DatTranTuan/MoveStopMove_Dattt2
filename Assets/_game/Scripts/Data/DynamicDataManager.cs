using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DynamicDataManager : Singleton<DynamicDataManager>
{
    public void SaveData<T>(T t)
    {
        string data = JsonUtility.ToJson(t);
        File.WriteAllText(Application.persistentDataPath + "/" + typeof(T).ToString() + ".json", data);
    }
    public T LoadData<T>()
    {
        string data = File.ReadAllText(Application.persistentDataPath + "/" + typeof(T).ToString() + ".json");
        return JsonUtility.FromJson<T>(data);
    }
    public bool ExistData<T>()
    {
        return File.Exists(Application.persistentDataPath + "/" + typeof(T).ToString() + ".json");
    }
    public void DeleteData<T>(T t)
    {
        if (!ExistData<T>()) return;
        File.Delete(Application.persistentDataPath + "/" + typeof(T).ToString() + ".json");
    }
}