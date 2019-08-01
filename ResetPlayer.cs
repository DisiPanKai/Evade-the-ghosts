using UnityEngine;
using UnityEngine.UI;

public class ResetPlayer : MonoBehaviour {


	void Start () 
    {
        GetComponent<Button>().onClick.AddListener(delegate { GameObject.Find("ResetPlayer").GetComponent<ResetPlayer>().ResetPlayerPosition(); });
	}
	
	
	public void ResetPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 0f);
    }
}
