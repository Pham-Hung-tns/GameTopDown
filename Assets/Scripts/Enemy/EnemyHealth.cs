using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ITakeDamage
{

    [SerializeField] private float health;
    public static event Action<Transform> OnEnemyKilledEvent;
    public static event Action OnChangeState;
    private SpriteRenderer spr;
    private float enemyHealth;
    private Coroutine coroutine;
    private Color initialColor;

    public float Health { get => health; set => health = value; }

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();   
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = Health;
        initialColor = spr.color;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        spr.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        spr.color = initialColor;
    }
}
