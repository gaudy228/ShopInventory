using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemScriptableObject item;
    public int amount;
    public bool isEmpty = true;
    public GameObject iconGO;
    public TMP_Text itemAmount;
    public GameObject Drag;
    private void Awake()
    {
        Drag = transform.GetChild(0).gameObject;
        iconGO = Drag.transform.GetChild(0).gameObject;
        itemAmount = Drag.transform.GetChild(1).GetComponent<TMP_Text>();
    }
    public void SetIcon(Sprite icon)
    {
        iconGO.GetComponent<Image>().color = new Color( 1,1,1,1 );
        iconGO.GetComponent<Image>().sprite = icon;
    }
}
