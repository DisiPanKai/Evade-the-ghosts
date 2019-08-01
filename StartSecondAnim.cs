using UnityEngine;

public class StartSecondAnim : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GameObject.Find("FirstAnimation").GetComponent<Animator>();
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("TransitionToSecondAnim"))
        {
            GetComponent<Animator>().SetBool("startSecondAnim", true);
            Destroy(this);
        }
    }
}
