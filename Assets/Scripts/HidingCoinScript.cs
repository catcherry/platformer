using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingCoinScript : CoinScript
{
    private SpriteRenderer img;

    public override void Start()
    {
        base.Start();
        img = GetComponent<SpriteRenderer>();
        HideCoin();
    }

    public void ShowCoin()
    {
        img.enabled = true;
    }

    public void HideCoin()
    {
        img.enabled = false;
    }

    public override void OnCollisionEnter2D(Collision2D collision) { 
        if (!img.enabled) return;
        base.OnCollisionEnter2D(collision);
    }
}
