using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;

    public int health,damage;
    public float flashTime;

    private SpriteRenderer sr;
    private Color originalColor;

    private PlayerHealth playerHealth;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    public void Update()
    {
        Killed();
    }


    public void Killed()
    {
        if (health <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            anim.SetTrigger("dead");
            //Invoke("Dead", 1f);
        }
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        FlashColor(flashTime);
        health -= damage;
    }

    void FlashColor(float time)
    {
        sr.color = Color.cyan;
        Invoke("ResetColor", time);
    }

    void ResetColor()
    {
        sr.color = originalColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(damage);
            }

        }
    }

}
