using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Fly : Enemy
{
    private Transform playerPos;
    public float speed;
    public float radius;
    protected AudioSource HurtAudio;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
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
}
