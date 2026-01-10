using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tech", menuName = "Tech Tree/Tech")]
public class Tech : ScriptableObject
{
    public string definition;
    public Texture2D image;
}
