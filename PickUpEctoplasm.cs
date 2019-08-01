using UnityEngine;

public class PickUpEctoplasm : MonoBehaviour //скрипт для подбор эктоплазмы. Прикреплён к эктоплазме
{
    public int scoreValue; //количество очков, которое даёт экто

    void Awake()
    {
        scoreValue = 1;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        GameController gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        if (other.tag == "Player") //если встречаемся с игроком, запускаем анимацию "эктоплазменного пира"
        {
            gameController.CarpetTutorial(scoreValue);
            Vector3 ectoPos = transform.parent.GetComponent<RectTransform>().anchoredPosition;
            gameController.EctoPositionForGhostSpawn = ectoPos; //записываем координату, чтобы при создании призрака он не появлялся с игроком на одной линии
            int forPositionState = 0;
            for (int i = 0; i < gameController.ectoplasmSpawnCoordinates.Length; i++)
                if (gameController.ectoplasmSpawnCoordinates[i] == ectoPos)
                {
                    forPositionState = i;
                    break;
                }
            gameController.PositionState[forPositionState] = false;
            //gameController.AllEctoPositions.Remove(ectoPos);
            GameObject player = GameObject.FindWithTag("Player");
            transform.SetParent(player.transform, false);
            Animator animator = GetComponent<Animator>();
            animator.SetBool("eat", true);
            gameController.AddToGhostScore(scoreValue);//обновляем счётчик для призраков
            gameController.EctoplasmOnFieldValue -= 1; //уменьшаем значение экто на поле
            gameController.AddToKeyStorage(scoreValue); //добавляем к-ство очков для фото

        }
    }

    void Update()
    {
        if (transform.localScale.x == 0)
            Destroy(gameObject);
    }
}
