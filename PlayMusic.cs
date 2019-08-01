using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    private GameObject playerControllerObj;

    private bool _musicPlay;

    // Update is called once per frame
    void Update()
    {
        playerControllerObj = GameObject.FindWithTag("Player");
        if (playerControllerObj.GetComponent<RectTransform>().anchoredPosition.x != 0 ||
            playerControllerObj.GetComponent<RectTransform>().anchoredPosition.y != 0)
        {
            GetComponent<AudioSource>().Play();
            Destroy(this);
        }
    }
}
