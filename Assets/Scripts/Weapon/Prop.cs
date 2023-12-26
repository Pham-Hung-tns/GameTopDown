using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class Prop : MonoBehaviour, ITakeDamage
{
    [SerializeField] float durability;
    private int counter;
    public void TakeDamage(float amount)
    {
        counter++;
        if (counter > durability)
        {
            Destroy(gameObject);
        }
    }
}
