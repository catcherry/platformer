using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitScript : BaseScript
{
    private GameObject dragon;

    void Start()
    {
        dragon = GameObject.FindWithTag("Dragon");
    }

    public void UpdateFire(bool fire)
    {
        if (dragon != null) dragon.GetComponent<DragonScript>().SetFire(fire);
    }

    public void Fire()
    {
        UpdateFire(true);
    }

    public void DisableFire()
    {
        UpdateFire(false);
    }
}