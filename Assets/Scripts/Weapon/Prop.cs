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

    public void TakeDamage(float amount, GameObject attacker, Vector2 knockbackDir, float knockbackForce)
    {
        //TODO: Add knockback effect to prop
        Debug.Log("Prop take damage with knockback");
    }
}
