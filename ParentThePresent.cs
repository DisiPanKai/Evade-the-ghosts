using UnityEngine;

public class ParentThePresent : MonoBehaviour {

	
	void Start () 
    {
        GameObject gameUnits = GameObject.Find("GameUnits");
        transform.SetParent(gameUnits.transform, false);
        transform.SetSiblingIndex(1);
	}
	
}
