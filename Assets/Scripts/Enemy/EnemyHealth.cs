using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ITakeDamage
{

    [SerializeField] private float health;
    private SpriteRenderer spr;
    private float enemyHealth;
    private Coroutine coroutine;
    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();   
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float amount)
    {
        enemyHealth -= amount;
        if(coroutine != null)
        {
            coroutine = null;
        }
        coroutine =  StartCoroutine(IEChangeColor());

        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator IEChangeColor()
    {
        spr.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        spr.color = Color.white;
    }
}
