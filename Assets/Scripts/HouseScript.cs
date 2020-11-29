using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScript : BaseScript
{
    private GameObject coinObject;

    // Start is called before the first frame update
    void Start()
    {
        coinObject = CheckCloseToTag("Coin", 3);
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
    }

    void CheckPlayer()
    {
        if (coinObject == null)
        {
            return;
        }
        GameObject maybePlayer = CheckCloseToTag("Player", 3);
        HidingCoinScript hcs = coinObject.GetComponent<HidingCoinScript>();
        if (maybePlayer != null)
        {
            hcs.ShowCoin();
            return;
        }
        hcs.HideCoin();
    }
}
