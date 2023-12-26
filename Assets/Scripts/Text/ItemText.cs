using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    
    public void SetText(string name, Color color)
    {
        this.itemName.text = name;
        this.itemName.color = color;
    }
    
}
