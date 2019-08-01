using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class BonusMenuScript : MonoBehaviour
{
    public GameObject bonusPrefab; //образец для бонусов
    public Sprite[] openedBonuses; //спрайты кнопок открытых бонусов
    public Sprite[] closedBonuses; //спрайты кнопок закрытых бонусов
    public Sprite[] bonusesOnField; //спрайты вылетающих бонусов 
    public Sprite[] helpText; //текст, который объясняет, что даёт бонус
    RectTransform rectTransform;
    public bool[] presentState = new bool[7]; //массив для определения, закрыт или открыт подарок.
    int presentNumber; //номер выбранного подарка
    int[][] bonusesCost = {new []{1, 3, 5, 8, 20}, 
        new []{4, 6, 8, 10, 22}, 
        new []{7, 9, 11, 12, 24}, 
        new []{10, 12, 14, 14, 26}, 
        new []{13, 15, 17, 16, 28}, 
        new []{16, 18, 20, 18, 30}, 
        new []{19, 21, 23, 20, 32}}; //стоимость бонусов

    Text[] bonusesTexts = new Text[5]; //текст стоимости бонусов на экране игрока

    public List<List<int>> bonusChoosed = new List<List<int>>();//лист для сохранения выбранных бонусов
    public GameObject[] bonusesObj; //объекты кнопок бонусов

    Text starsAvaliableText; //текст для передачи оставшегося к-ства звёзд

    int starsAvaliable; //сначала перменная используется как общее к-ство звёзд, затем, отнимаем к-ство звёзд использованными бонусами
    int usedStarsFromPresent;
    Image chooseTextImage; //объект для вставки подсказок

    private int _starsForCheats;


    void Start()
    {
        Initialiase();
        IsThereAnyBonusesInPresent();
    }


    void Initialiase() //подготовка меню
    {
        rectTransform = GetComponent<RectTransform>();
        GameController gameController =
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        //starsAvaliable = _gameController.Stars.Length;
        _starsForCheats = gameController.StarsForCheats;
        starsAvaliable = gameController.Stars.Sum();
        starsAvaliable += _starsForCheats;
        GameObject.Find("StarsTotalValueText").GetComponent<Text>().text = "" + starsAvaliable;
        starsAvaliableText = GameObject.Find("StarsAvaliableValueText").GetComponent<Text>();
        chooseTextImage = GameObject.Find("ChooseText").GetComponent<Image>();
        CheckingBonusCost();
        bonusesTexts[0] = GameObject.Find("SpeedBonusCost Text").GetComponent<Text>();
        bonusesTexts[1] = GameObject.Find("TimeBonusCost Text").GetComponent<Text>();
        bonusesTexts[2] = GameObject.Find("EctoBonusCost Text").GetComponent<Text>();
        bonusesTexts[3] = GameObject.Find("ColorBonusCost Text").GetComponent<Text>();
        bonusesTexts[4] = GameObject.Find("BoomBonusCost Text").GetComponent<Text>();
        //for (int i = 0; i < 7; i++)
        //    bonusChoosed.Add(new List<int> { });
        if (gameController.Millionare)
            for (int i = 0; i < 7; i++)
                presentState[i] = true;
        else
            presentState[0] = true;
    }


    void IsThereAnyBonusesInPresent() //при появлении меню и переключении подарка проверяет, есть ли в текущем подарке бонусы
    {
        GameController _gameController =
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        bonusChoosed = _gameController.ActiveBonuses;
        if (bonusChoosed[0].Count != 0)
            BuildBonuses();
        int presentAmount = bonusChoosed.Count;
        for (int i = 0; i < presentAmount; i++)
        {
            int bonusesAmount = bonusChoosed[i].Count;
            for (int j = 0; j < bonusesAmount; j++)
            {
                int bonusId = bonusChoosed[i][j];
                int oneBonusCost = bonusesCost[i][bonusId];
                starsAvaliable -= oneBonusCost;
            }
        }
        starsAvaliableText.text = "" + starsAvaliable;
        CalculateUsedStarsForCurrentPresent();
    }


    void CalculateUsedStarsForCurrentPresent() //сколько звёдочек уже потрачено в текущем подарке
    {
        usedStarsFromPresent = 0;
        int bonusesCount = bonusChoosed[presentNumber].Count;
        for (int i = 0; i < bonusesCount; i++)
        {
            int _currentBonus = bonusChoosed[presentNumber][i];
            usedStarsFromPresent += bonusesCost[presentNumber][_currentBonus];
        }
    }


    void Update()
    {
        if (rectTransform.anchoredPosition.y > 1300f)
        {
            Destroy(transform.parent.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            starsAvaliable += 10;
            _starsForCheats += 10;
            GameObject.Find("StarsTotalValueText").GetComponent<Text>().text = "" + starsAvaliable;
            starsAvaliableText.text = "" + starsAvaliable;
            CheckingBonusCost();
        }
    }


    public void BonusMenuGetOut() //убрать меню
    {
        GetComponent<Animator>().Play("PresentOut");
        GameController gameController =
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gameController.ActiveBonuses = bonusChoosed;
        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.MenuIsOpen = false;
        gameController.StarsForCheats = _starsForCheats;
    }


    public void NextPresent()
    {
        DestroyBonuses();
        int presentNumberForLeftButton = presentNumber; //переменная для того, чтобы определить когда мы переходим с 1 подарка на 2, должна появится левая кнопка
        PresentChange(1);
        BuildBonuses();
        if (presentNumber == 6) //доходим до седьмого подарка, левая кнопка исчезает
        {
            GameObject rightButtonObj = GameObject.Find("RightButton");
            rightButtonObj.GetComponent<Button>().enabled = false;
            rightButtonObj.GetComponent<Animator>().Play("RightButtonDecrease", -1, 0f);
        }
        if (presentNumberForLeftButton == 0)
        {
            GameObject leftButtonObj = GameObject.Find("LeftButton");
            leftButtonObj.GetComponent<Button>().enabled = true;
            leftButtonObj.GetComponent<Animator>().Play("LeftButtonIncrease", -1, 0f);
        }
    }


    public void PreviousPresent()
    {
        DestroyBonuses();
        int presentNumberForRightButton = presentNumber;//переменная для того, чтобы определить когда мы переходим с 7 подарка на 6, должна появится правая кнопка
        PresentChange(-1);
        BuildBonuses();
        if (presentNumber == 0) //доходим до первого подарка, левая кнопка исчезает
        {
            GameObject leftButtonObj = GameObject.Find("LeftButton");
            leftButtonObj.GetComponent<Button>().enabled = false;
            leftButtonObj.GetComponent<Animator>().Play("LeftButtonDecrease", -1, 0f);
        }
        if (presentNumberForRightButton == 6)
        {
            GameObject rightButtonObj = GameObject.Find("RightButton");
            rightButtonObj.GetComponent<Button>().enabled = true;
            rightButtonObj.GetComponent<Animator>().Play("RightButtonIncrease", -1, 0f);
        }
    }


    void PresentChange(int presentMove) //смена подарка
    {
        GameObject.Find("BoxFull").GetComponent<Animator>().Play("DeadState", -1, 0f);
        GameObject.Find("Present" + presentNumber).GetComponent<Image>().enabled = false;//выключаем текущий подарок
        if (presentMove == 1)
            presentNumber++;
        else
            presentNumber--;
        GameObject presentObj = GameObject.Find("Present" + presentNumber);
        presentObj.GetComponent<Image>().enabled = true; //включаем следующий подарок
        ChangeCostBonuses();
        if (presentState[presentNumber]) //действия, если подарок уже открыт
        {
            GameObject.Find("ResetSkillsButton").GetComponent<Button>().enabled = true;
            CheckingBonusCost();
            presentObj.GetComponent<Animator>().Play("BoxOpen" + presentNumber, -1, 0f);
            chooseTextImage.sprite = helpText[5];
            chooseTextImage.GetComponent<RectTransform>().sizeDelta = new Vector2(504f, 42f);
            CalculateUsedStarsForCurrentPresent();
        }
        else //если закрыт
        {
            presentObj.GetComponent<Animator>().Play("BoxClosed" + presentNumber, -1, 0f);
            for (int i = 0; i < 5 /* к-ство бонусов*/; i++)
            {
                bonusesObj[i].GetComponent<Button>().enabled = false;
                bonusesObj[i].GetComponent<Image>().sprite = closedBonuses[i];
            }
            GameObject.Find("ResetSkillsButton").GetComponent<Button>().enabled = false;

        }

    }


    public void ChangeCostBonuses() //заполнение стоимости бонусов в соответствии с номером подарка
    {
        for (int i = 0; i < 5; i++)
        {
            bonusesTexts[i].text = "" + bonusesCost[presentNumber][i];
        }
    }


    public void CheckingBonusCost() //проверка, нужно ли открывать кнопки бонусов, чтобы игрок смог выбрать бонусы, и отображение, доступны ли бонусы
    {
        for (int i = 0; i < 5 /* к-ство бонусов*/; i++)
        {
            if (starsAvaliable >= bonusesCost[presentNumber][i])
            {
                bonusesObj[i].GetComponent<Button>().enabled = true;
                bonusesObj[i].GetComponent<Image>().sprite = openedBonuses[i];
            }
            else
            {
                bonusesObj[i].GetComponent<Button>().enabled = false;
                bonusesObj[i].GetComponent<Image>().sprite = closedBonuses[i];
            }
        }
    }


    /*методы для кнопок бонусов. 
     * номера бонусов
     * 1 - скорость
     * 2 - замедление призраков
     * 3 - больше экто
     * 4 - смена цвета
     * 5 - хлопушка
    */
    public void UseSpeedBonus()
    {
        AddBonus(0);
    }


    public void UseTimeBonus()
    {
        AddBonus(1);
    }


    public void UseEctoBonus()
    {
        AddBonus(2);
    }


    public void UseColorBonus()
    {
        AddBonus(3);
    }


    public void UseBoomBonus()
    {
        AddBonus(4);
    }


    void AddBonus(int bonusNumber) //добавление бонуса, выходящего из подарка, в ряд бонусов
    {
        chooseTextImage.sprite = helpText[bonusNumber];
        chooseTextImage.GetComponent<RectTransform>().sizeDelta = new Vector2(504f, 52f);
        DestroyBonuses();
        bonusChoosed[presentNumber].Add(bonusNumber);
        bonusChoosed[presentNumber].Sort();

        usedStarsFromPresent += bonusesCost[presentNumber][bonusNumber];
        starsAvaliable -= bonusesCost[presentNumber][bonusNumber];
        starsAvaliableText.text = "" + starsAvaliable;

        CheckingBonusCost();
        BuildBonuses();
        GameObject.Find("Present" + presentNumber).transform.Find("Present Animation").GetComponent<Animator>().Play("BoxOpen" + presentNumber, -1, 0f);
    }


    public void RemoveOneBonus(int bonusNumber) //удалить конкретный бонус и построить новый ряд
    {
        int bonusCount = bonusChoosed[presentNumber].Count;
        if (bonusCount == 5)
            GameObject.Find("BoxFull").GetComponent<Animator>().Play("DeadState", -1, 0f);
        bonusChoosed[presentNumber].Remove(bonusNumber);
        int bonusCost = bonusesCost[presentNumber][bonusNumber]; //определяем стоимость бонуса
        usedStarsFromPresent -= bonusCost; //уменьшаем количество использованных звёзд
        starsAvaliable += bonusCost; //возвращение потраченых звёзд на бонус в общую копилку
        starsAvaliableText.text = "" + starsAvaliable;
        DestroyBonuses();
        CheckingBonusCost();
        BuildBonuses();
    }


    void CheckFullnessOfTheBox() //проверяет, заполнен ли подарок пятью(это максимум) бонусами
    {
        int bonusesCount = bonusChoosed[presentNumber].Count;
        if (bonusesCount == 5)
        {
            GameObject.Find("BoxFull").GetComponent<Animator>().Play("BoxFull", -1, 0f);
            for (int i = 0; i < 5; i++)
            {
                bonusesObj[i].GetComponent<Button>().enabled = false;
                bonusesObj[i].GetComponent<Image>().sprite = closedBonuses[i];
            }
        }
    }


    void BuildBonuses() //выстраивает в порядок бонусы текущего подарка
    {
        int bonusesCount = bonusChoosed[presentNumber].Count;

        float bonusDistance = 60f;
        float bonusPos = -(bonusDistance * (bonusesCount - 1)) / 2f;

        for (int i = 0; i < bonusesCount; i++)
        {
            GameObject bonusClone = Instantiate(bonusPrefab);
            bonusClone.transform.SetParent(GameObject.Find("Present" + presentNumber).transform);//привязываем бонус к текущему боксу
            bonusClone.transform.SetAsFirstSibling();
            int currentBonus = bonusChoosed[presentNumber][i]; //id бонуса, который надо поместить как картинку
            Transform childObjOfBonus = bonusClone.transform.GetChild(0);
            childObjOfBonus.GetComponent<Image>().sprite = bonusesOnField[currentBonus];
            childObjOfBonus.GetComponent<BonusInPresent>().BonusNumber = currentBonus;
            bonusClone.tag = "Bonuses" + presentNumber;
            bonusClone.name = "Bonus" + currentBonus;
            RectTransform bonusRect = bonusClone.GetComponent<RectTransform>();
            bonusRect.anchoredPosition = new Vector3(bonusPos, 0f);
            bonusRect.localScale = new Vector3(1, 1);
            bonusPos += bonusDistance;
            CheckFullnessOfTheBox();
        }
    }


    public void ThrowAwayAllBonuses() //выкинуть все бонусы из текущего подарка
    {
        DestroyBonuses();
        chooseTextImage.sprite = helpText[5];
        GameObject.Find("BoxFull").GetComponent<Animator>().Play("DeadState", -1, 0f);
        bonusChoosed[presentNumber].Clear();
        CheckingBonusCost();
        starsAvaliable += usedStarsFromPresent;
        starsAvaliableText.text = "" + starsAvaliable;
        usedStarsFromPresent = 0;
    }


    void DestroyBonuses() //уничтожает все объекты бонусов
    {
        GameObject[] bonusesInPresent = GameObject.FindGameObjectsWithTag("Bonuses" + presentNumber);
        if (bonusesInPresent != null) //если бонусы есть над подарком, то удаляем их
            for (int i = 0; i < bonusesInPresent.Length; i++)
                Destroy(bonusesInPresent[i]);
        GameObject.Find("Present" + presentNumber).GetComponent<Animator>().Play("BoxOpen" + presentNumber, -1, 0f);
    }
}
