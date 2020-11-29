using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : CoinScript
{
    public override void Start()
    {
        base.Start();
        SetAmount(10);
    }
}
