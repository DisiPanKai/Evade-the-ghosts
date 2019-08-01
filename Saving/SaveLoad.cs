using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    public static SavingGame savedGame;



    public static void Save()
    {
        try
        {
            if (!Directory.Exists("Saves"))
                Directory.CreateDirectory("Saves");
            savedGame = new SavingGame();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create("Saves/savedGame.gd");

            bf.Serialize(file, savedGame);
            file.Close();
            Debug.Log(Application.persistentDataPath);

        }
        catch (IOException errorMsg)
        {
            Debug.Log(errorMsg);
        }

    }

    public static void Load()
    {
        if (File.Exists("Saves/savedGame.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open("Saves/savedGame.gd", FileMode.Open);
            savedGame = (SavingGame)bf.Deserialize(file);
            GameController gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
            gameController.KeyValue = savedGame.savedKeyValue;
            gameController.KeyStorage = savedGame.savedKeyStorage;
            gameController.KeyPrice = savedGame.savedKeyPrice;
            gameController.UpdateKeyText();
            gameController.UpdatePhotoAmountText();
            gameController.FillKeyBar();
            file.Close();
        }
        else
        {
            Debug.Log("Don't be shy");
        }
    }
}
