using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVitality : MonoBehaviour
{
    // 1 palyer nên dùng static oke. Event này dùng để show Pannel khi player chết
    public static event Action OnPlayerDeathEvent;

    private PlayerConfig _data;

    private float timer;
    private Coroutine coroutine;

    // Sử dụng Property để UI có thể lắng nghe sự thay đổi nếu cần
    public float CurrentHealth { get; private set; }
    public float CurrentArmor { get; private set; }
    public float CurrentEnergy { get; private set; }

    public void Initialize(PlayerConfig data)
    {
        _data = data;
        CurrentHealth = _data.MaxHealth;
        CurrentArmor = _data.MaxArmor;
        CurrentEnergy = _data.MaxEnergy;
    }

    #region Player Health Methods
    public void TakeDamage(float amount)
    {
        // sau này sẽ thêm âm thanh và hiển thị damage
        //AudioManager.Instance.PlaySFX("Human_Damage");
        //DamageManager.Instance.ShowDmg(amount, transform);

        // nếu nhận damage, đặt lại thời gian hồi giáp
        ResetTimer();

        // trừ giáp trước, trừ máu sau
        if (CurrentArmor > 0)
        {
            float remaningDamage = amount - CurrentArmor;
            CurrentArmor = Mathf.Max(CurrentArmor - amount, 0f);
            if (remaningDamage > 0)
            {
                CurrentHealth = Mathf.Max(CurrentHealth - remaningDamage, 0f);
            }
        }
        else
        {
            CurrentHealth = Mathf.Max(CurrentHealth - amount, 0f);
        }
        if (CurrentHealth <= 0f)
        {
            // sau này sẽ thêm trên scene
            //GameManager.Instance.gameData.totalCoin += CoinManager.Instance.totalCoins;
            //SaveSystem.Save(GameManager.Instance.gameData);
            //AudioManager.Instance.PlaySFX("Human_Defeat");

            OnPlayerDeathEvent?.Invoke();
            // PlayerDead(); : tạo thêm hàm destroy player sau này
        }
    }

    public void RecoverHealth(float amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > _data.MaxHealth)
        {
            CurrentHealth = _data.MaxHealth;
        }
    }
    #endregion


    #region Player Energy Methods
    public void RecoverEnergy(float amount)
    {
        CurrentEnergy += amount;
        if (CurrentEnergy > _data.MaxEnergy)
        {
            CurrentEnergy = _data.MaxEnergy;
        }
    }

    public bool TryConsumeEnergy(float amount)
    {
        if (CurrentEnergy >= amount)
        {
            CurrentEnergy -= amount;
            return true;
        }
        return false;
    }
    #endregion


    #region Player Armor Methods
    public void ResetTimer()
    {
        CancelInvoke();
        timer = _data.timeCooldownArmor;
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(IECheckCoolDown());
    }

    private IEnumerator IECheckCoolDown()
    {
        yield return new WaitForSeconds(timer);
        CheckCoolDown();
    }
    public void CheckCoolDown()
    {
        InvokeRepeating(nameof(RecoverArmor), 0.1f, _data.timeRecoverArmor);
    }

    public void RecoverArmor()
    {
        if (CurrentArmor == _data.MaxArmor)
        {
            CancelInvoke();
            return;
        }
        CurrentArmor += 1;
    }
    #endregion
}
