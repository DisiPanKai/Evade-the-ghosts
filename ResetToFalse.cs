using UnityEngine;

public class ResetToFalse : MonoBehaviour //скидывает все состоянии
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("SpeedUp") || animator.GetCurrentAnimatorStateInfo(0).IsName("SlowGhosts") || animator.GetCurrentAnimatorStateInfo(0).IsName("MoreEcto"))
	    {
            animator.SetBool("speedUp", false);
            animator.SetBool("slowGhosts", false);
            animator.SetBool("moreEcto", false);
	    }
	}
}
