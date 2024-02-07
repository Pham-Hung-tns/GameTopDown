using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Transform itemPos;

    [Header("Item")]
    [SerializeField] private bool usePredefinedItemFromChest; // if true, use predefined item
    [SerializeField] private GameObject predefinedItem; // predefined item
    
    private Animator animator;
    private bool openChest;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (openChest) return;
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX("Chest_Open");
            ShowItem();
            animator.SetTrigger("Open");
        }
    }

    private void ShowItem()
    {

        if (usePredefinedItemFromChest)
        {
            Instantiate(predefinedItem, transform.position, Quaternion.identity, itemPos.parent);
        }
        else
        {
            Instantiate(LevelManager.Instance.RandomItemInEachChest(), transform.position, Quaternion.identity, itemPos.parent);
        }
            openChest = true;
    }
}
