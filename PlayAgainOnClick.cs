using UnityEngine;
using UnityEngine.UI;

public class PlayAgainOnClick : MonoBehaviour {
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { GameObject.FindWithTag("GameController").GetComponent<GameController>().Restart(); });
    }
	
}
