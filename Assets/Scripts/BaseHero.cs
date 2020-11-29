using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero : BaseScript
{
    public int hp = 100;
    public bool isBlinking = false;
    public float cooldownTime = 0;
    public float speed = 5f;

    public void Blink()
    {
        isBlinking = true;
    }

    public virtual void Die()
    {
        Die(true);
    }

    public virtual void Die(bool resetLives=true)
    {
        Destroy(this.gameObject);
    }

    public virtual void RemoveHealth()
    {
        if (cooldownTime > Time.time) return;
        hp -= 15;
        Blink();
        cooldownTime = Time.time + 1.5f;
    }

    public int GetHealth()
    {
        return hp;
    }

    public void AddHealth(int count)
    {
        hp += count;
    }

    public GameObject GetGround()
    {
        GameObject[] ground_ = GameObject.FindGameObjectsWithTag("Ground");
        GameObject ground = ground_[ground_.Length - 1];
        return ground;
    }

    public void IgnoreGroundCollision()
    {
        GameObject ground = GetGround();
        Physics2D.IgnoreCollision(ground.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    public void RevertIgnoreGroundCollision()
    {
        GameObject ground = GetGround();
        Physics2D.IgnoreCollision(ground.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
    }

}