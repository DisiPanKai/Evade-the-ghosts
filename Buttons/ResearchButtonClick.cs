using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResearchButtonClick : MonoBehaviour {

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { GameObject.FindWithTag("GameController").GetComponent<GameController>().ResearchTheGhost(); });
    }
}
