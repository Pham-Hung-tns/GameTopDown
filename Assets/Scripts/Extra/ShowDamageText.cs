using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowDamageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dmgText;

    public void SetDamageText(float value)
    {
        dmgText.text = value.ToString();
    }

    public void DestroyText()
    {
        Destroy(gameObject);
    }
}
