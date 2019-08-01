using UnityEngine;
using UnityEngine.UI;

public class PagesInAlbum : MonoBehaviour
{
    public Sprite[] pageIndexes;//спрайты обозначений страниц

    private float pageStep;
    private float moveTime;
    private float pageEndPos;
    private bool goLeft, goRight;
    public int pageNumber; //номер страницы начинается с 0 до 8.
    private GameObject pageIndexObj;
    private RectTransform rectTransform;

    void Awake()
    {
        pageStep = 550f;
        pageEndPos = 550f;
        pageNumber = 0;
        pageIndexObj = GameObject.Find("PageIndex");
        rectTransform = GetComponent<RectTransform>();
        moveTime = 3000f;

    }


    public void ChooseWearedSkinPage(int skinNumber)
    {
        int step = 9; //1 шаг в 9 скинов на одной странице
        int counter = 0; //счётчик
        int factor = 0; //множитель, а затем и расстояние, на которое нужно передвинуть страницы альбома,
        //для координат в соответствии со страницей, но сначала выступает, как счётчик для страницы
        int maxPages = 9; //максимум страниц

        for (int i = 0; i < maxPages; i++) //определяем, на какой странице находится текущий надетый скин
        {
            factor++;
            if (skinNumber >= 1 + counter && skinNumber <= 9 + counter)
                break;
            counter += step;
        }
        pageNumber = factor;
        pageIndexObj.GetComponent<Image>().sprite = pageIndexes[pageNumber];
        GameObject leftButtonObj = GameObject.Find("LeftButton");
        leftButtonObj.GetComponent<Button>().enabled = true;
        leftButtonObj.GetComponent<Animator>().Play("LeftButtonScale1", -1, 0f);
        if (pageNumber == 8)
        {
            GameObject rightButtonObj = GameObject.Find("RightButton");
            rightButtonObj.GetComponent<Button>().enabled = false;
            rightButtonObj.GetComponent<Animator>().Play("RightButtonScale0", -1, 0f);
        }
        factor *= (int)pageStep; //
        pageEndPos -= factor;
        rectTransform.anchoredPosition = new Vector2(pageEndPos, rectTransform.anchoredPosition.y);

    }

    void Update()
    {
        if (goRight)
        {
            rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x - (moveTime * Time.deltaTime), rectTransform.anchoredPosition.y);
            if (rectTransform.anchoredPosition.x <= pageEndPos)
            {
                goRight = false;
                rectTransform.anchoredPosition = new Vector3(pageEndPos, rectTransform.anchoredPosition.y);
            }
            return;
        }

        if (goLeft)
        {
            rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x + (moveTime * Time.deltaTime), rectTransform.anchoredPosition.y);
            if (rectTransform.anchoredPosition.x >= pageEndPos)
            {
                goLeft = false;
                rectTransform.anchoredPosition = new Vector3(pageEndPos, rectTransform.anchoredPosition.y);
            }
            return;
        }



        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextPage();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousPage();
        }

    }

    public void NextPage()
    {
        int _pageNumberForLeftButton = pageNumber; //переменная для того, чтобы определить когда мы переходим с 0 страницы на 1, должна появится левая кнопка
        pageNumber++;
        pageIndexObj.GetComponent<Image>().sprite = pageIndexes[pageNumber];
        goRight = true;
        pageEndPos = pageEndPos - pageStep;
        if (pageNumber == 8)
        {
            GameObject rightButtonObj = GameObject.Find("RightButton");
            rightButtonObj.GetComponent<Button>().enabled = false;
            rightButtonObj.GetComponent<Animator>().Play("RightButtonDecrease", -1, 0f);
        }
        if (_pageNumberForLeftButton == 0)
        {
            GameObject leftButtonObj = GameObject.Find("LeftButton");
            leftButtonObj.GetComponent<Button>().enabled = true;
            leftButtonObj.GetComponent<Animator>().Play("LeftButtonIncrease", -1, 0f);
        }


    }

    public void PreviousPage()
    {
        int _presentNumberForRightButton = pageNumber;//переменная для того, чтобы определить когда мы переходим с 8 страницы на 7, должна появится правая кнопка
        pageNumber--;
        pageIndexObj.GetComponent<Image>().sprite = pageIndexes[pageNumber];
        goLeft = true;
        pageEndPos = pageEndPos + pageStep;
        if (pageNumber == 0) //доходим до первой страницы, левая кнопка исчезает
        {
            GameObject leftButtonObj = GameObject.Find("LeftButton");
            leftButtonObj.GetComponent<Button>().enabled = false;
            leftButtonObj.GetComponent<Animator>().Play("LeftButtonDecrease", -1, 0f);
        }
        if (_presentNumberForRightButton == 8)
        {
            GameObject rightButtonObj = GameObject.Find("RightButton");
            rightButtonObj.GetComponent<Button>().enabled = true;
            rightButtonObj.GetComponent<Animator>().Play("RightButtonIncrease", -1, 0f);
        }
    }
}
