using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    private Renderer myRender;
    public int Blinks;
    public float time;
    public float dieTime;
    public float hitBoxCdTime;

    private Animator anim;
    private PolygonCollider2D polygonCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        myRender = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        HealthBar.HealthMax = health;
        HealthBar.HealthCurrent = health;
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damage)
    {
        health -= damage;
        if(health<0)
        {
            health = 0;
        }
        HealthBar.HealthCurrent = health;
        if(health <= 0 )
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            anim.SetTrigger("dead");
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 0.5f);
        }
        BlinkPlayer(Blinks,time);
        polygonCollider2D.enabled = false;
        StartCoroutine(ShowPlayerHitBox());
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator ShowPlayerHitBox()
    {
        yield return new WaitForSeconds(hitBoxCdTime);
        polygonCollider2D.enabled = true;
    }

    void BlinkPlayer(int numBlinks,float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks,seconds));
    }

    IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for (int i = 0;i < numBlinks * 2;i++)
        {
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
}
