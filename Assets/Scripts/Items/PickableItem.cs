using Realms.Exceptions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [SerializeField] ItemDataSO item;
    
    private PlayerControls actions;
    private ItemText nameText;
    private bool isPlayerInRange;
    private Color textColor;
    private void Awake()
    {
        actions = new PlayerControls();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowNameOfTheItem();
            UIManager.Instance.ShowPickupButton(true);
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            UIManager.Instance.ShowPickupButton(false);
            Destroy(nameText.gameObject);
        }
    }

    private void Update()
    {
        if(isPlayerInRange && actions.Interaction.PickItem.IsPressed())
        {
            item.PickUp();
            Destroy(gameObject);
        }
    }

    public void ShowNameOfTheItem()
    {
        if(item is WeaponDataSO weapon)
        {
            textColor = GameManager.Instance.ChooseColorForWeapon(weapon);
            nameText = ItemTextManager.Instance.ShowName(weapon.name, textColor, Vector3.up + transform.position);
        }
        else
        {
            nameText = ItemTextManager.Instance.ShowName(item.name, Color.white, Vector3.up + transform.position);

        }
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}
