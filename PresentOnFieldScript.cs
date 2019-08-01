using System.Collections;
using UnityEngine;

public class PresentOnFieldScript : MonoBehaviour
{
    public int presentNumber;
	void Update () 
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("DestroyPresent"))
            Destroy(transform.parent.gameObject);
	}

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GetComponent<Animator>().SetBool("givePresent", true);
            yield return new WaitForSeconds(1f); //ждём, пока подарок распакуется
            //StartCoroutine(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().UnpackTheGift());
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().UnpackTheGift(presentNumber);
        }
    }
}
