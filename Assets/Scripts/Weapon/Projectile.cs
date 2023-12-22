using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;
    [SerializeField] private float damaged;

    public Vector3 Direction { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Direction * (speed * Time.deltaTime),Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<ITakeDamage>() != null)
        {
            collision.GetComponent<ITakeDamage>().TakeDamage(1f);            
        }
        Destroy(gameObject);
    }
}
