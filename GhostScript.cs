using UnityEngine;

public class GhostScript : MonoBehaviour
{
    private Vector3 ghostPos;
    private float border;
    public bool poof;
	void Start () 
    {
        GameObject gameUnits = GameObject.Find("GameUnits");
        transform.SetParent(gameUnits.transform, false);
	    border = 350f;
	    poof = false;
    }

    void Update()
    {
        ghostPos = GetComponent<RectTransform>().anchoredPosition;
        if (ghostPos.x > border || ghostPos.x < -border || ghostPos.y > border || ghostPos.y < -border)
        {
            if (poof)
            {
                GetComponent<BoxCollider2D>().enabled = true;
                transform.GetChild(0).GetChild(0).GetComponent<CircleCollider2D>().enabled = true;
                transform.GetChild(0).GetComponent<Animator>().Play("DeadState");
                poof = false;
            }
            GameController gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
            gameController.RandomGhostSpawn = Random.Range(0, gameController.ghostSpawnCoordinates.Length);
            gameController.CheckingPosition();
            GetComponent<RectTransform>().anchoredPosition = gameController.ghostSpawnCoordinates[gameController.RandomGhostSpawn];
            gameController.RunGhostRun(gameObject);
        }
    }
}
