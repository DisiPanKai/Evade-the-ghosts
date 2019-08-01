using UnityEngine;

public class BonusButton : MonoBehaviour {

    public GameObject AlbumMenu;
    private Vector2 playerPos;

	void Start () 
    {
        GameObject displayedText = GameObject.Find("DisplayedText");
        transform.SetParent(displayedText.transform, false);
	}
	
	void Update () 
    {
        playerPos = GameObject.FindWithTag("Player").GetComponent<RectTransform>().anchoredPosition;
        if (playerPos.x != 0 || playerPos.y != 0)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool("getOut", true);
        }
        if (GetComponent<RectTransform>().anchoredPosition.y < -450f)
            Destroy(gameObject);
	}

    public void SummonBonusMenu()
    {
        Instantiate(AlbumMenu);
        BonusMenuScript bonusMenuScript = GameObject.Find("BonusOut").GetComponent<BonusMenuScript>();
        bonusMenuScript.CheckingBonusCost();
        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.MenuIsOpen = true;
    }
}
