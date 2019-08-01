using UnityEngine;

public class OptionsButton : MonoBehaviour {
    public GameObject optionsMenu;
    private GameObject playerControllerObject;

	void Start () 
    {
        GameObject displayedText = GameObject.Find("DisplayedText");
        transform.SetParent(displayedText.transform, false);
	}

    void Update()
    {
        playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject.GetComponent<RectTransform>().anchoredPosition.x != 0 || playerControllerObject.GetComponent<RectTransform>().anchoredPosition.y != 0)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool("getOut", true);
        }
        if (GetComponent<RectTransform>().anchoredPosition.y < -450f)
            Destroy(gameObject);
    }
    public void SummonOptionsMenu()
    {
        Instantiate(optionsMenu);
    }
}
