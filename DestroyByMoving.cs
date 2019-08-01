using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DestroyByMoving : MonoBehaviour
{
    private RectTransform playerRectTransform;
    private Image image;
    private Animator animator;
    private float speed;

    void Start()
    {
        speed = 4f;
        image = GetComponent<Image>();
        var color = image.color;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerRectTransform = playerControllerObject.GetComponent<RectTransform>();

        }
        if (playerRectTransform == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
        var color = image.color;
        if (playerRectTransform.anchoredPosition.x != 0 || playerRectTransform.anchoredPosition.y != 0)
        {
            animator.SetBool("getOut", true);
            color.a -= speed * Time.deltaTime;
        }

        color.a = Mathf.Clamp(color.a, 0, 1);

        image.color = color;

        if (color.a == 0)
            Destroy(this.gameObject);
    }
}
