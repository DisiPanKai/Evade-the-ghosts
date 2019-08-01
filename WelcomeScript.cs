using UnityEngine;

public class WelcomeScript : MonoBehaviour
{

    void Update()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<RectTransform>().anchoredPosition;
        if (playerPosition.x != 0f || playerPosition.y != 0f)
        {
            GetComponent<Animator>().Play("WelcomeOff", -1, 0f);
            GameObject.Find("Carpet2").GetComponent<Animator>().Play("CollectOn", -1, 0f);
            Destroy(this);
        }
    }
}
