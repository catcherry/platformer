using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private int amount = 1;

    // Start is called before the first frame update
    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void OnCollisionEnter2D (Collision2D  collision)
    {
        GameObject other = collision.gameObject;
        if (other.name == "player")
        {
            PlayerScript playerScript = other.gameObject.GetComponent<PlayerScript>();
            playerScript.AddCoins(amount);
            playerScript.PlaySound(PlayerSounds.COIN_COLLECT);
            Destroy(this.gameObject);
        }
    }

    public void SetAmount(int amount)
    {
        this.amount = amount;
    }
}
