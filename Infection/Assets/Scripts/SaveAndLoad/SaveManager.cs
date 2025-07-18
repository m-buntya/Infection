using System.IO;
using UnityEngine;

public static class SaveManager
{

    public static void Save<T>(T data, string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("保存完了：" + path);
    }

    public static T Load<T>(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        if (!File.Exists(path))
        {
            Debug.Log("セーブデータが見つからない：" + path);
            return default;
        }

        string json = File.ReadAllText(path);
        T data = JsonUtility.FromJson<T>(json);
        Debug.Log("ロード完了：" + path);
        return data;
    }
}