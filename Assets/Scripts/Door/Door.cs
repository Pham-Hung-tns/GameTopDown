using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Door : MonoBehaviour
{
   public void HideDoor()
    {
        gameObject.SetActive(false);
    }

    public void ShowDoor()
    {
        gameObject.SetActive(true);
    }

    
}
