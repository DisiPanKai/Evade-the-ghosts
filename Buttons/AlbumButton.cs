using UnityEngine;


public class AlbumButton : MonoBehaviour
{
    public GameObject album;
    private GameObject playerControllerObject;

    void Start()
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

    public void SummonAlbum()
    {
        Instantiate(album);
        GameController gameController =
           GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        //int ghostBought = (int)gameController.GhostAnimationNumberBought.Count; //к-ство купленных призраков
        //int maxUnblockedGhosts = (int)(gameController.GhostAmountForAlbum + ghostBought + gameController.GhostOpenedByChangeColorSkin); //сколько открытых призраков показано: формула состоит из суммы купленных, открытых на поле призраков 
        //    //и открытых бонусом перерисовки
        //if (maxUnblockedGhosts > 72)
        //    maxUnblockedGhosts = 72;
        AlbumScript albumScript = GameObject.Find("AlbumOut").GetComponent<AlbumScript>();
        albumScript.UnblockGhostInAlbum(gameController.GhostThatWeSaw);
        int[] boughtGhosts = gameController.GhostAnimationNumberBought.ToArray(); //конвертация коллекции в массив
        albumScript.UnblockGhostsWithStars(boughtGhosts);
        albumScript.LookAtTheStars(gameController.Stars);
        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.MenuIsOpen = true;
    }
}
