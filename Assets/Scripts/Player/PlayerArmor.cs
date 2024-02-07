using System.Collections;
using UnityEngine;
public class PlayerArmor : MonoBehaviour
{
    [SerializeField] private float timeCooldownArmor;
    [SerializeField] PlayerConfig playerConfig;
    private float timer;
    private Coroutine coroutine;

    private void Start()
    {
        timer = timeCooldownArmor;
        PlayerHealth.OnPlayerTakeDamage -= ResetTimer;
        PlayerHealth.OnPlayerTakeDamage += ResetTimer;
    }
    public void ResetTimer()
    {
        CancelInvoke();
        timer = timeCooldownArmor;
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
        InvokeRepeating(nameof(RecoverArmor), 0.1f, 2f);
    }

    public void RecoverArmor()
    {
        if (playerConfig.currentArmor == playerConfig.MaxArmor)
        {
            CancelInvoke();
            return;
        }
        playerConfig.currentArmor += 1;
    }

}

