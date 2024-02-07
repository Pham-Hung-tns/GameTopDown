using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private float radiusAttack;
    [SerializeField] private Transform positionAttack;
    [SerializeField] private float knockBackSpeed;
    [SerializeField] private float knockBackDelay;
    public override void UseWeapon()
    {
        base.UseWeapon();
        AudioManager.Instance.PlaySFX("Sword_Hit");
    }

    public override void DestroyWeapon()
    {
        Destroy(gameObject);
    }

    public void DetectedCharacter()
    {
        
        Collider2D[] Characters = null;
        if (Character is PlayerWeapon player)
        {
            Characters =  Physics2D.OverlapCircleAll(positionAttack.position, radiusAttack, LayerMask.GetMask(LAYER_ENEMY));
        }
        else
        {
            Characters = Physics2D.OverlapCircleAll(positionAttack.position, radiusAttack, LayerMask.GetMask(LAYER_PLAYER));
        }

        if (Characters.Length > 0) {
            StopAllCoroutines();
            foreach (Collider2D collider in Characters)
            {
                ITakeDamage obj = collider.GetComponent<ITakeDamage>();
                if (obj != null)
                {
                    obj.TakeDamage(weaponData.damage);
                    Vector3 knockBack = (collider.transform.position - Character.transform.position).normalized;
                    Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.AddForce(knockBack * knockBackSpeed, ForceMode2D.Force);
                        StartCoroutine(IEStopKnockBack(knockBackDelay, rb));
                    }
                }
            }
        }
    }
    private IEnumerator IEStopKnockBack(float delay, Rigidbody2D rb)
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(positionAttack.position, radiusAttack);
    }
}
