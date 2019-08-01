using System;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class PhotoScript : MonoBehaviour
{
    public bool clicked;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { GetComponent<PhotoScript>().TransferPlayerSpriteNumber(); });
        //clicked = false;
    }

    public void TransferPlayerSpriteNumber() //передаёт номер выбранного скина на игрока
    {
        if (!clicked)
        {
            String gameObjName = gameObject.name;
            int playerSpriteNumber = Convert.ToByte(Regex.Replace(gameObjName, @"[^\d]+", ""));//достаём из имени номер фотографии

            int photosAmount = 72; //количество фотографий
            for (int i = 1; i <= 72; i++) //при клике на этом фото ставим флажки, что у всех остальных фото не выбраны, кроме нашей фотографии. Также, убираем выбор с предыдущей фото, если таковой имелся
            {
                //if (playerSpriteNumber == i) //если выбран тот скин, на который мы кликнули, то идём дальше по списку
                //    continue;
                GameObject photoObj = GameObject.Find("Photo" + i);
                photoObj.GetComponent<PhotoScript>().clicked = false; 
                photoObj.GetComponent<Animator>().SetBool("click", false);
            }
            clicked = true;
            int hangerColor =
                GameObject.FindGameObjectWithTag("GameController")
                    .GetComponent<GameController>()
                    .PlacingColor(playerSpriteNumber - 1);
            Sprite hangerSprite = GameObject.Find("AlbumOut").GetComponent<AlbumScript>().HangersSprites[hangerColor];
            transform.FindChild("Hanger").GetComponent<Image>().sprite = hangerSprite;
            GetComponent<Animator>().SetBool("click", true);

            GameController gameController =
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            PlayerController playerController =
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            int skinNumber = playerSpriteNumber + 1; //добавляем единицу из-за того, что дефолтный скин = 1

            playerController.playerSkinNumber = skinNumber;
            playerController.playerColorNumber = gameController.PlacingColor(skinNumber - 2); // -2 - это костыль. Метод Placing Color старый и сделан под призраков, у которых номер скина
            //начинается с массива, который стартует с 0. Тут же, игрок, у которого номер скина начинается с 1. 1 - это дефолтный скин и определение цвета на него не нужен. Ниже есть else, который
            //пишет стандартный цвет. Поэтому, счёт начинается с 2. А в методе счёт начинается с 0. Поэтому, чтобы это всё синхронизировать, всё подставляем под тот метод
        }
        else //отмена выбора скина
        {
            GetComponent<Animator>().SetBool("click", false);
            clicked = false;
            PlayerController playerController =
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerController.playerSkinNumber = 1; //номер стандартного скина
            playerController.playerColorNumber = 9; //номер нейтрального цвета для стандартного скина
            //место для того, чтобы выбрать стандартный скин
        }

    }
}
