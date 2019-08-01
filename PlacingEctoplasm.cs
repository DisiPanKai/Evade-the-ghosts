using UnityEngine;
using UnityEngine.UI;

public class PlacingEctoplasm : MonoBehaviour
{
    public GameObject ecto;
    private GameController gameController;
    public int scoreValue; //к-ство очков для экто
    public int colorNumber; //цвет экто
    public int ghostSkinNumber; //номер призрака в массиве
    public int ghostNumber; //номер призрака на поле
    public int counter; //счётчик пройденых точек для спауна экто
    public int EctosOnFieldLimit; //допустимое количество экто на поле
    private bool _ectoSpawned; //костыль для того, чтобы на том месте, где есть подарок или бонус, не появилась экто


    void Awake()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();

        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        if (gameController != null)
            counter = Random.Range(3, 5) + Mathf.RoundToInt(gameController.GhostAmount * 0.7f);

        scoreValue = 1;
        EctosOnFieldLimit = 7;
        _ectoSpawned = false;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameController.EctoplasmOnFieldValue <= EctosOnFieldLimit)
        {
            if (other.tag == "Ectoplasm" || other.tag == "BonusOnField" || other.tag == "PresentOnField")
            {
                if (_ectoSpawned)
                    _ectoSpawned = false;
                else
                {
                    counter += 1;
                    return;
                }
            }

            if (other.tag == "EctoplasmSpawnCoordinates")
                counter -= 1;


            if (counter == 0)
            {
                Vector3 velocity = transform.parent.parent.GetComponent<Rigidbody2D>().velocity;
                float scaler = GameObject.Find("GameUnits").GetComponent<RectTransform>().localScale.x;
                float ghostSpeedWithScale = gameController.GhostSpeed * scaler;
                Vector3 placeEctoplasm = transform.parent.parent.GetComponent<RectTransform>().anchoredPosition;

                if (velocity.x == -ghostSpeedWithScale || velocity.x == ghostSpeedWithScale)
                //определение, в каком квадрате находится призрак и записывание координат
                {
                    if (45f < placeEctoplasm.x)
                    {
                        if (placeEctoplasm.x > 45f && placeEctoplasm.x < 135f)
                            placeEctoplasm = new Vector3(90f, placeEctoplasm.y);
                        else
                            placeEctoplasm = new Vector3(180f, placeEctoplasm.y);
                    }
                    else
                        if (placeEctoplasm.x > -45f)
                            placeEctoplasm = new Vector3(0f, placeEctoplasm.y);
                        else
                            if (placeEctoplasm.x < -135f)
                                placeEctoplasm = new Vector3(-180f, placeEctoplasm.y);
                            else
                                placeEctoplasm = new Vector3(-90f, placeEctoplasm.y);

                }
                else
                {
                    if (45f < placeEctoplasm.y)
                    {
                        if (placeEctoplasm.y > 45f && placeEctoplasm.y < 135f)
                            placeEctoplasm = new Vector3(placeEctoplasm.x, 90f);
                        else
                            placeEctoplasm = new Vector3(placeEctoplasm.x, 180f);
                    }
                    else
                        if (placeEctoplasm.y > -45f)
                            placeEctoplasm = new Vector3(placeEctoplasm.x, 0f);
                        else
                            if (placeEctoplasm.y < -135f)
                                placeEctoplasm = new Vector3(placeEctoplasm.x, -180f);
                            else
                                placeEctoplasm = new Vector3(placeEctoplasm.x, -90f);

                }
                int forPositionState = 0; //определяем, в какой клетке стоит экто для PositionState
                for (int i = 0; i < gameController.ectoplasmSpawnCoordinates.Length; i++)
                    if (gameController.ectoplasmSpawnCoordinates[i] == placeEctoplasm)
                    {
                        forPositionState = i;
                        break;
                    }
                gameController.PositionState[forPositionState] = true;//записываем позицию, как занятую
                //gameController.AllEctoPositions.Add(placeEctoplasm);//добавляем существующую координату экто на поле

                if (scoreValue == 1) //если маленькая экто
                {
                    GameObject newEcto = Instantiate(ecto, placeEctoplasm, Quaternion.identity) as GameObject;
                    newEcto.GetComponentInChildren<Image>().sprite = gameController.ectoSprites[colorNumber];
                }
                else //если большая
                {
                    GameObject newEcto = Instantiate(ecto, placeEctoplasm, Quaternion.identity) as GameObject;
                    newEcto.transform.Find("Ectoplasm").GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                    newEcto.GetComponentInChildren<Image>().sprite = gameController.bigEctoSprites[colorNumber];
                    newEcto.GetComponentInChildren<PickUpEctoplasm>().scoreValue = 2;
                }
                counter = Random.Range(3, 5) + Mathf.RoundToInt(gameController.GhostAmount * 0.7f);
                gameController.EctoplasmOnFieldValue += 1;
                _ectoSpawned = true;
            }
        }
    }
}
