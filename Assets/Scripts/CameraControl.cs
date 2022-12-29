using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    public float smoothing;

    // Update is called once per frame
    void Update()
    {
       // transform.position = new Vector3(player.position.x, 0, -10);
    }

    private void LateUpdate()
    {
        if(player != null)
        {
            if(transform.position != player.position)
            {
                Vector3 playerPos = player.position;
                transform.position = Vector3.Lerp(transform.position, playerPos, smoothing);
            }
        }
    }
}
