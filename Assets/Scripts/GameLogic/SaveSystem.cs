using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void SaveData(PlayerData player)
    {
        string path = Application.persistentDataPath + "/player.dat";
        BinaryFormatter formatter = new BinaryFormatter();
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        FileStream stream = null;
        try
        {
            stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, player);
            stream.Close();
        }
        finally
        {
            if (stream != null)
            {
                stream.Dispose();
            }
        }
    }
    public static PlayerData LoadData()
    {
        PlayerData data = null;
        string path = Application.persistentDataPath + "/player.dat";
        BinaryFormatter formatter = new BinaryFormatter();
        if (File.Exists(path))
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(path, FileMode.Open);
                data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
            }
        }
        return data;
    }
}
