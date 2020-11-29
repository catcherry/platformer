using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDoorScript : BaseScript
{
    void Update()
    {
        GameObject playerIsNear = CheckCloseToTag("Player", 5);
        if (playerIsNear != null && Input.GetKeyDown(KeyCode.Return))
        {
            GetComponent<ShopScript>().Show();
        }
    }
}
