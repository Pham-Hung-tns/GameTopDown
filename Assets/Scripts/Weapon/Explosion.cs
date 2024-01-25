using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Animator anim;
    private bool playerInExplore;
    private GameObject player = null;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void DestroyThis()
    {
        Destroy(gameObject, 0.3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
        }
    }

    private void Explore()
    {
        if (player != null)
        {
            ITakeDamage hit = player.GetComponent<ITakeDamage>();
            if (hit != null) ;
            hit.TakeDamage(2f);
        }
    }
}
