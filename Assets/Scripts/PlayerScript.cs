using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerSounds
{
    STAB,
    COIN_COLLECT
}
public class PlayerScript : BaseHero
{

    private int currentLives = 3;
    private float jumpForce = 8f;
    private int maxJumps = 2;

    public int coins;

    private Rigidbody2D rb;
    private SpriteRenderer img;
    private Animator animator;
    private AudioSource audioSource;

    private bool onGround = true;
    private bool ignoreInput = false;
    private bool falling = false;
    private bool jumping = false;

    private Vector2 move;
    public Vector3 startPosition;
    private GameObject enemyCloseToPlayer = null;

    public Image uiSword1;
    public Image uiSword2;
    public Image uiSword3;

    public AudioClip coinCollectedSound;
    public AudioClip stabSound;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        img = GetComponent<SpriteRenderer>();
        startPosition = new Vector3(-6.64F, -2.08F, 0);
        rb.transform.position = startPosition;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (onGround) maxJumps = 2;

        if (Input.GetKeyDown(KeyCode.Space) && (maxJumps > 0))
        {
            jumping = true;
            rb.velocity = Vector2.up * jumpForce;
            onGround = false;
            maxJumps--;
        }
    }

    public void AddLives(int amount) {
        currentLives += amount;
    }

    void FixedUpdate()
    {
        img.enabled = !isBlinking;
        isBlinking = false;

        animator.SetBool("Sword", false);
        animator.SetBool("Walk", false);

        move.y = rb.velocity.y;

        if (!ignoreInput)
        {
            

            if (Input.GetKey("f"))
            {
                animator.SetBool("Sword", true);
                PlaySound(PlayerSounds.STAB);
                if (enemyCloseToPlayer != null)
                    enemyCloseToPlayer.GetComponent<EnemyScript>().RemoveHealth();
            }

            if (Input.GetKey("a") || Input.GetKey("d"))
            {
                animator.SetBool("Walk", true);
            }

            if (Input.GetKey("a"))
            {
                move.x = -speed;
                img.flipX = true;
            }
            else if (Input.GetKey("d"))
            {
                move.x = speed;
                img.flipX = false;
            }
            else
            {
                move.x = 0;
            }
        }


        if (falling)
        {
            move.y -= 10;
        }

        rb.velocity = move;

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0)
        {
            Die(false);
        }



        GameObject objectCloseToPlayer = CheckCloseToTag("Enemy", 10);
        enemyCloseToPlayer = objectCloseToPlayer;

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Collider2D other = collision.collider;

        if (other.tag == "Pit")
        {
            other.gameObject.GetComponent<PitScript>().DisableFire();
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D other = collision.collider;

        if (other.tag == "Ground")
        {
            onGround = true;
            jumping = false;
        }
        else if (other.tag == "Pit")
        {
            other.gameObject.GetComponent<PitScript>().Fire();
        }
       
    }

    public int GetLives()
    {
        return currentLives;
    }

    public bool IsInputIgnored()
    {
        return ignoreInput;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public void PlaySound(PlayerSounds sound)
    {
        AudioClip clip = null;
        switch (sound)
        {
            case PlayerSounds.COIN_COLLECT:
                clip = coinCollectedSound;
                break;
            case PlayerSounds.STAB:
                clip = stabSound;
                break;
            default:
                break;
        }
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }

    public void UpdateUI()
    {

        Image[] obj =
        {
            uiSword1,
            uiSword2,
            uiSword3
        };

        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].enabled = currentLives > i;
        }

    }

    public void Die()
    {
        Die(true);
    }

    public override void Die(bool resetLives=true)
    {
        if (resetLives)
            currentLives = 3;
        hp = 100;
        RevertIgnoreGroundCollision();
        falling = false;
        jumping = false;
        rb.transform.position = startPosition;
        ignoreInput = false;
        onGround = true;
        enemyCloseToPlayer = null;
        UpdateUI();
    }
    
    public int GetCoins()
    {
        return coins;
    }

    public void Kill()
    {
        hp -= 15;
        Blink();
        if (hp <= 0)
        {
            hp = 100;
            currentLives--;
            if (currentLives == 0)
            {
                ignoreInput = true;
                falling = true;
                IgnoreGroundCollision();
                Invoke("Die", 1f);
            }
        }
        UpdateUI();
    }

    public void RemoveCoins(int c)
    {
        coins -= c;
    }

    public override void RemoveHealth()
    {
        base.RemoveHealth();
        if (hp <= 0)
        {
            hp = 100;
            currentLives--;
            if (currentLives == 0)
            {
                ignoreInput = true;
                falling = true;
                IgnoreGroundCollision();
                Invoke("Die", 1f);
            }
        }
        UpdateUI();
    }
}