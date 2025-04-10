using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Default, Food, Weapon}
public class ItemScriptableObject : ScriptableObject

{
    public ItemType itemType;
    public string itemName;
    public GameObject itemPrefab;
    public Sprite icon;
    public int maximumAmount;
    
}
