using System.Collections;
using UnityEngine;

/// <summary>
/// Simple door controller used by InstantiatedRoom to lock/unlock room entrances.
/// Assumes a Collider2D blocks passage when enabled.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D doorCollider;

    private static readonly int OpenHash = Settings.open;
    private bool isLocked;

    private void Awake()
    {
        if (doorCollider == null)
            doorCollider = GetComponent<Collider2D>();
    }

    public void LockDoor()
    {
        isLocked = true;
        if (doorCollider != null) doorCollider.enabled = true;
        if (animator != null) animator.ResetTrigger(OpenHash);
    }

    public void UnlockDoor(float delay = 0f)
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(UnlockRoutine(delay));
    }

    private IEnumerator UnlockRoutine(float delay)
    {
        if (delay > 0f)
            yield return new WaitForSeconds(delay);

        isLocked = false;
        if (animator != null) animator.SetTrigger(OpenHash);
        if (doorCollider != null) doorCollider.enabled = false;
    }
}

