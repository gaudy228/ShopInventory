using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Items/New Item")]
public class NewItem : ItemScriptableObject
{
    public float healAmount;

    public void Start()
    {
        itemType = ItemType.Food;
    }
}
