using UnityEngine;
using System.Collections;

public class DestroyArrows : MonoBehaviour {
    void Start()
    {
        GameObject displayedText = GameObject.Find("DisplayedText");
        transform.SetParent(displayedText.transform, false);
    }
	void Update () 
    {
        if (transform.FindChild("Top Arrow") == null)
            Destroy(gameObject);
	}
}
