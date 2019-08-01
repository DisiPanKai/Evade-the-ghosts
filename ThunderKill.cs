using UnityEngine;
using System.Collections;

public class ThunderKill : MonoBehaviour {
    void Start()
    {
        GameObject displayedText = GameObject.Find("DisplayedText");
        transform.SetParent(displayedText.transform, false);
    }
	void Update () 
    {
        if (this.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Thunderstorm Destroy"))
            Destroy(this.gameObject);
        
	}
}
