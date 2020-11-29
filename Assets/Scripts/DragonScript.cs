using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonScript : BaseScript
{
    private Animator animator;
    private bool fire = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Fire", fire);
    }

    // Update is called once per frame
    void Update()
    {
        if (!fire) return;
        GameObject playerNear = CheckCloseToTag("Player", 5);
        if (playerNear != null)
            playerNear.GetComponent<PlayerScript>().Kill();
    }

    public void SetFire(bool value)
    {
        fire = value;
        animator.SetBool("Fire", fire);
    }
}
