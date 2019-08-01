using UnityEngine;

public class KeyFillBar : MonoBehaviour
{
    private GameController gameController;
    public bool doStep;
    public float step;
    private RectTransform rectTransform;
    private float moveTime;
    public float endPos;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        doStep = false;
        rectTransform = GetComponent<RectTransform>();
        moveTime = 0.1f;
        endPos = -210f;
    }


    void Update()
    {
        if (doStep)
        {
            step = Mathf.Lerp(rectTransform.anchoredPosition.x, endPos, moveTime);
            rectTransform.anchoredPosition = new Vector3(step, rectTransform.anchoredPosition.y);
            if (rectTransform.anchoredPosition.x >= endPos - 0.2f)
            {
                rectTransform.anchoredPosition = new Vector3(endPos, rectTransform.anchoredPosition.y);
                doStep = false;
                if (rectTransform.anchoredPosition.x > 215f)
                {
                    float beginning = -210f;
                    rectTransform.anchoredPosition = new Vector3(beginning, rectTransform.anchoredPosition.y);
                    gameController.KeyBarReset = false;
                    gameController.FillKeyBar();
                }
            }
        }
    }
}
