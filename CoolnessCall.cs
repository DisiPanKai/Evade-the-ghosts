using UnityEngine;

public class CoolnessCall : MonoBehaviour {
    public GameObject coolness;

	void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "CoolnessBoundary")
        {
            Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
            if (velocity.x < 0f)
            {
                Vector3 coolnessPosition = new Vector3(2.3f, transform.position.y, transform.position.z);
                Instantiate(coolness, coolnessPosition, Quaternion.identity);
            }
        }
    }
}
