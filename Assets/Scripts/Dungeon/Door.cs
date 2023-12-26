using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void HideDoor()
    {
        anim.SetTrigger("Open");
    }

    public void ShowDoor()
    {
       anim.SetTrigger("Close");
    }

    
}
