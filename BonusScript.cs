using UnityEngine;


public class BonusScript : MonoBehaviour
{
    public int bonusID;
	

	void Start ()
	{
        GameObject ectoplasms = GameObject.Find("Ectoplasms");
        transform.SetParent(ectoplasms.transform, false);
	}
}
