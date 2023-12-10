using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] PlayerConfig playerConfig;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            RecoverHealth(1);
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(2);
        }
    }
    public void RecoverHealth(float amount)
    {
        playerConfig.currentHealth += amount;
        if (playerConfig.currentHealth > playerConfig.MaxHealth)
        {
            playerConfig.currentHealth = playerConfig.MaxHealth;
        }
    }

    public void TakeDamage(float amount)
    {
        //playerConfig.currentHealth -= amount;
        if (playerConfig.currentArmor > 0)
        {
            float remaningDamage = amount - playerConfig.currentArmor;
            playerConfig.currentArmor = Mathf.Max(playerConfig.currentArmor - amount, 0f);
            if (remaningDamage > 0)
            {
                playerConfig.currentHealth = Mathf.Max(playerConfig.currentHealth - remaningDamage, 0f);
            }
        }
        else
        {
            playerConfig.currentHealth = Mathf.Max(playerConfig.currentHealth - amount, 0f);
        }
        if (playerConfig.currentHealth <= 0f)
        {
            PlayerDead();
        }
    }

    private void PlayerDead()
    {
        Destroy(gameObject);
    }
}
