using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectCharacter : MonoBehaviour
{
    [SerializeField] PlayerConfig playerConfig;
    public PlayerConfig PlayerConfig => playerConfig;


    //private void OnMouseDown()
    //{
    //    MenuManager.Instance.ShowStats(this);
    //}
}
