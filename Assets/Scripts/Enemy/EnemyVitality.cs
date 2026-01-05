using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVitality : MonoBehaviour, ITakeDamage
{

    public static event Action<Transform> OnEnemyKilledEvent;
    public static event Action OnChangeState;
    private float enemyHealth;
    private Coroutine coroutine;

    public float Health { get => enemyHealth; set => enemyHealth = value; }

    // Start is called before the first frame update
    public void Initialized(EnemyConfig config)
    {
        enemyHealth = config.Health;
    }

    public void TakeDamage(float amount)
    {
        AudioManager.Instance.PlaySFX("Enemy_Damage");
        enemyHealth -= amount;
        DamageManager.Instance.ShowDmg(amount, transform);
        if(coroutine != null)
        {
            coroutine = null;
        }
        coroutine =  StartCoroutine(IEChangeColor());

        if(enemyHealth <= 0)
        {
            OnEnemyKilledEvent?.Invoke(transform);
            OnChangeState?.Invoke();
            Destroy(gameObject);
        }
    }

    private IEnumerator IEChangeColor()
    {
        //spr.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        //spr.color = initialColor;
    }

    public void TakeDamage(float amount, GameObject attacker, Vector2 knockbackDir, float knockbackForce)
    {
        //TODO: Implement knockback
        Debug.Log("Enemy Take Damage with knockback");
    }
}
