using UnityEngine;

public class CoolnessKill : MonoBehaviour {

    void Start()
    {
        GameObject displayedText = GameObject.Find("DisplayedText");
        transform.SetParent(displayedText.transform, false);
    }
	void Update()
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Coolness Destroy"))
            Destroy(gameObject);
    }
}
