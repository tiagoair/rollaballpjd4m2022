using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public int damage = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 direction = collision.transform.position - transform.position;
            direction = direction.normalized;
            
<<<<<<< HEAD
            collision.gameObject.GetComponent<PlayerController>().ChangeHealth(-damage, true, direction);
=======
            collision.gameObject.GetComponent<PlayerController>().ChangeHealth(-damage, withForce: true, direction);
>>>>>>> 5881a230993fc023e62fdd753b5ce76553509068
        }
    }
}
