using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public void Death()
    {
        FindObjectOfType<PlayerController>().CoinCount();
        Destroy(gameObject);
    }
}
