using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Item/Create New Item")]
public class Item : ScriptableObject
{
    public int ItemId;
    public string itemName;
    public int itemvalue;
    public Sprite Icon;
}
