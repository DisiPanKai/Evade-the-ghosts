using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumScript : MonoBehaviour
{
    public Sprite[] closedGhosts;
    public Sprite[] starsPicture;
    public Sprite[] HangersSprites;
    public Sprite emptyPhoto;
    private bool goUp;
    private RectTransform rectTransform;
    private float moveTime;
    
    

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        moveTime = 3000f;
        ChooseWearedSkin();
    }


    void Update()
    {
        if (goUp)
        {
            rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + (moveTime * Time.deltaTime));
        }
        if (rectTransform.anchoredPosition.y > 1300f)
        {
            Destroy(transform.parent.gameObject);
        }
    }


    private void ChooseWearedSkin() //когда открываем альбом, этот метод выбирает текущий скин на игроке
    {
        int numberOfPhoto = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerSkinNumber - 1; //
        if (numberOfPhoto != 0)
        {
            PhotoScript photoScript = GameObject.Find("Photo" + numberOfPhoto).GetComponent<PhotoScript>();
            photoScript.TransferPlayerSpriteNumber();
            PagesInAlbum pagesInAlbum = GameObject.Find("Pages").GetComponent<PagesInAlbum>();
            pagesInAlbum.ChooseWearedSkinPage(numberOfPhoto); //передаём номер скина
        }
    }


    public void AlbumGetOut() //убрать альбом с экрана
    {
        goUp = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().WearTheSkin();
        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.MenuIsOpen = false;
    }


    public void UnblockGhostInAlbum(List<int> ghostThatWeSaw) //прорисовка закрытых призраков
    {
        foreach (int skinId in ghostThatWeSaw)
        {
            GameObject.Find("Photo" + (skinId + 1)).transform.FindChild("Photos").GetComponent<Image>().sprite =
            closedGhosts[skinId];
        }
        //for (int i = 1; i <= maxUnblockedGhosts; i++)
        //{
        //    GameObject.Find("Photo" + i).transform.FindChild("Photos").GetComponent<Image>().sprite =
        //        closedGhosts[i - 1];
        //}
    }
    

    public void UnblockGhostsWithStars(int[] boughtAndThoseWeSawGhosts) //прорисовка открытых призраков
    {
        int arrayLength = boughtAndThoseWeSawGhosts.Length;
        for (int i = 0; i < arrayLength; i++)
        {
            Transform transformPhoto = GameObject.Find("Photo" + (boughtAndThoseWeSawGhosts[i] + 1)).transform;
            transformPhoto.GetComponent<Button>().enabled = true;
            transformPhoto.FindChild("Photos").GetComponent<Image>().sprite = emptyPhoto; //рамка фото на фоне призрака
            transformPhoto.FindChild("Stars").GetComponent<Image>().enabled = true; 
            Transform ghostObj = transformPhoto.FindChild("Ghost");
            ghostObj.GetComponent<Image>().enabled = true;
            ghostObj.GetComponent<Animator>().enabled = true; //включение анимации призрака
            RuntimeAnimatorController currentController = ghostObj.GetComponent<Animator>().runtimeAnimatorController; //замена анимации
            String currentAnimationName = currentController.animationClips[0].name;
            AnimatorOverrideController ghostOverrideController = new AnimatorOverrideController();
            ghostOverrideController.runtimeAnimatorController = currentController;
            GameController gameController =
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            ghostOverrideController[currentAnimationName] = gameController.ghostAnimationClips[boughtAndThoseWeSawGhosts[i]];
            ghostObj.GetComponent<Animator>().runtimeAnimatorController = ghostOverrideController;
        }
    }


    public void LookAtTheStars(int[] stars) //заполнение звёздочек под скинами
    {
        for (int i = 1; i < 73; i++)
                GameObject.Find("Photo" + i).transform.FindChild("Stars").GetComponent<Image>().sprite =
                    starsPicture[stars[i]];
    }


}
