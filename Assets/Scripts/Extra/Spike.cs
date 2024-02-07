using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public const string SHOW_SPIKE = "show";
    public const string HIDE_SPIKE = "hide";

    [SerializeField] private Animator anim;
    private string currentState;
    private static float timer;

    private void Start()
    {
        timer = 3f; 
        currentState = SHOW_SPIKE;
        ChangeAnim(SHOW_SPIKE);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if(currentState == SHOW_SPIKE)
            {
                ChangeAnim(HIDE_SPIKE);
            }
            else
            {
                ChangeAnim(SHOW_SPIKE);
            }
            timer = 3f;
        }
    }

    private void ChangeAnim(string newState)
    {
        if(currentState != newState)
        {
            anim.ResetTrigger(newState);
            currentState = newState;
            anim.SetTrigger(currentState);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth obj = collision.GetComponent<PlayerHealth>();
            if(obj != null)
            {
                DungeonCM.Instance.ShakeCM(3f, 1f);
                obj.TakeDamage(1f);
            }
        }    
    }
}
