using UnityEngine;

[System.Serializable]
public class SavingGame
{
    public int savedKeyValue;
    public int savedKeyStorage;
    public int savedKeyPrice;

    public SavingGame()
    {
        GameController gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        savedKeyValue = gameController.KeyValue;
        savedKeyStorage = gameController.KeyStorage;
        savedKeyPrice = gameController.KeyPrice;
    }

}