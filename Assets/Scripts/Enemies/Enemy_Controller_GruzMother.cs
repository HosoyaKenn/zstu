using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller_GruzMother : Enemy
{
    private Transform playerPos;
    public float speed;
    public float radius;
    protected AudioSource HurtAudio;
    //[SerializeField] private GameObject flySpawn;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if(health < 10)
        {
            anim.SetTrigger("attack");
            
            if (playerPos != null)
            {
                float distance = (transform.position - playerPos.position).sqrMagnitude;
                if (distance < radius)
                {
                    
                    transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
                    HurtAudio.Play();

                }
            }
        }
       // if(health<=0)
        //{
       //    SpawnFlys();
       // }
    }

    //hurt
   void AttackPlayer()
    {
        if(health<10)
        {
            anim.SetTrigger("attack");
            
            StartAttack();
            
        }
    }


    void StartAttack()
    {
        if(playerPos != null)
        {
            
            float distance = (transform.position - playerPos.position).sqrMagnitude;
            if(distance < radius)
            {
                
                transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
                HurtAudio.Play();
            }
        }
    }

   // public void SpawnFlys()
   // {
  //      flySpawn.SetActive(true);
   // }
}
