using UnityEngine;

public class DestroyEcto : MonoBehaviour {

    void Start()
    {
        GameObject ectoplasms = GameObject.Find("Ectoplasms");
        transform.SetParent(ectoplasms.transform, false);
    }
    void Update()
    {
        if (transform.FindChild("Ectoplasm") == null)
            Destroy(gameObject);
    }
}
