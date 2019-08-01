using UnityEngine;

public class BonusInPresent : MonoBehaviour {

    public int BonusNumber;
	

    public void BonusRemove()
    {
        GameObject.Find("BonusOut").GetComponent<BonusMenuScript>().RemoveOneBonus(BonusNumber);
    }
}
