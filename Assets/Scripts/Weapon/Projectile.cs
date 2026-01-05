using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;
    public float Damage { get; set; }
    public float Speed { get; set; }
    public Vector3 Direction { get; set; }

    private GameObject owner;

    // Start is called before the first frame update
    void Start()
    {
        Speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Direction * (speed *Time.deltaTime), Space.World);
    }

    public void Initialize(GameObject owner, float speed, float damage)
    {
        this.owner = owner;
        this.speed = speed;
        this.Damage = damage;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == owner) return;

        ITakeDamage td = collision.GetComponent<ITakeDamage>();
        if(td != null)
        {
            // no knockback info here, pass zero
            td.TakeDamage(Damage, owner, Vector2.zero, 0f);
            ReturnBullet();
            return;
        }

        if (collision.CompareTag(Settings.collisionTilemapTag))
        {
            ReturnBullet();
            return;
        }
    }

    private void ReturnBullet()
    {
        if (ObjPoolManager.Instance != null)
        {
            ObjPoolManager.Instance.ReturnBullet(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
