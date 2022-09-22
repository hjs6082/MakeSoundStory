using System.IO;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int money; 
    
    public GameData(int _money)
	{
		money = _money;
	}
}

[System.Serializable]
public class MusicData
{
    public string name;

    public List<bool> music = new List<bool>();

    public MusicData(string _name, List<List<bool>> _music)
    {
        name = _name;

        for(int i = 0; i < _music.Count; i++)
        {
            for(int j = 0; j < _music[i].Count; j++)
            {
                music.Add(_music[i][j]);
            }
        }
    }
}

[System.Serializable]
public class SampleMusic
{
    public int[] stats;

    public SampleMusic(int[] stats)
    {
        
    }
}

public static class SaveSystem
{
    private static string SavePath => Application.persistentDataPath + "/saves/";

    public static void Save<T>(T saveData, string saveFileName)
    {
        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
        }

        string saveJson = JsonUtility.ToJson(saveData);

        string saveFilePath = SavePath + saveFileName + ".json";
        File.WriteAllText(saveFilePath, saveJson);
        Debug.Log("Save Success: " + saveFilePath);
    }

    public static T Load<T>(string saveFileName)
    {
        string saveFilePath = SavePath + saveFileName + ".json";

        if (!File.Exists(saveFilePath))
        {
            Debug.LogError("No such saveFile exists");

            return default(T);
        }

        string saveFile = File.ReadAllText(saveFilePath);
        T saveData = JsonUtility.FromJson<T>(saveFile);
        return saveData;
    }
}