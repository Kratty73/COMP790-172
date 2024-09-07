using UnityEngine;
using System.Collections.Generic;

public class SaveLoad : MonoBehaviour
{
    public highScore hs = new highScore();

    private string filePath;
    void Start()
    {
        filePath = Application.persistentDataPath + "/hs.json";
        try
        {
            string fileData = System.IO.File.ReadAllText(filePath);
            hs = JsonUtility.FromJson<highScore>(fileData);
        }
        catch
        {
            hs.hs = 0;
        }
    }

    public highScore load()
    {
        string fileData = System.IO.File.ReadAllText(filePath);
        hs = JsonUtility.FromJson<highScore>(fileData);
        return hs;
    }

    public void save(int score)
    {
        Debug.Log(score);
        hs.hs = score;
        System.IO.File.WriteAllText(filePath, JsonUtility.ToJson(hs));
    }


}

[System.Serializable]
public class highScore
{
    public int hs;
}