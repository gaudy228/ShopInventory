using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DragAndDropItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public InventorySlot oldSlot;
    private Transform player;
    private Vector3 vec;
    
    private void Start()
    {
        vec = new Vector3(1,1,0);
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        oldSlot = transform.GetComponentInParent<InventorySlot>();
        if (oldSlot.isEmpty == false)
        {
            oldSlot.SetIcon(oldSlot.item.icon);
            oldSlot.itemAmount.text = oldSlot.amount.ToString();
        }
        else
        {
            oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            oldSlot.iconGO.GetComponent<Image>().sprite = null;
            oldSlot.itemAmount.text = "";
        }
    }
   
    public void OnDrag(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        Debug.Log("OnBeginDrag");
        if (oldSlot.isEmpty)
            return;
       
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
        
        GetComponentInChildren<Image>().raycastTarget = false;
        
        transform.SetParent(transform.parent.parent);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        
        Debug.Log("OnEndDrag");
        if (oldSlot.isEmpty)
            return;
       
    
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        
        GetComponentInChildren<Image>().raycastTarget = true;

        transform.SetParent(oldSlot.transform);
            transform.position = oldSlot.transform.position;
        if (eventData.pointerCurrentRaycast.gameObject.name == "inventoryPanel")
        {
            
        }
        else if (eventData.pointerCurrentRaycast.gameObject.name == "UIBG")
        {
            
            GameObject itemObject = Instantiate(oldSlot.item.itemPrefab, player.position + vec, Quaternion.identity);
            
            itemObject.GetComponent<item>().amount = oldSlot.amount;
            
            NullifySlotData();
        }
        else if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>() != null)
        {
            
            ExchangeSlotData(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>());
        }
    }
    
    void NullifySlotData()
    {
        oldSlot.item = null;
        oldSlot.amount = 0;
        oldSlot.isEmpty = true;
        oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        oldSlot.iconGO.GetComponent<Image>().sprite = null;
        oldSlot.itemAmount.text = "";
    }

   
    void ExchangeSlotData(InventorySlot newSlot)
    {
       
        ItemScriptableObject item = newSlot.item;
        int amount = newSlot.amount;
        bool isEmpty = newSlot.isEmpty;
        GameObject iconGO = newSlot.iconGO;
        TMP_Text itemAmountText = newSlot.itemAmount;
        if(item == null)
        {
            if (oldSlot.amount > 1)
            {


                if (Input.GetKey(KeyCode.LeftControl))
                {
                    newSlot.item = oldSlot.item;
                    newSlot.amount = 1;
                    newSlot.isEmpty = false;
                    newSlot.iconGO = iconGO;
                    newSlot.itemAmount.text = newSlot.amount.ToString();
                    oldSlot.amount--;
                        
                  
                    oldSlot.itemAmount.text = oldSlot.amount.ToString();
                    newSlot.SetIcon(oldSlot.iconGO.GetComponent<Image>().sprite);
                    return;
                }
            }
        }
        if (newSlot.item != null)
        {
            if (oldSlot.item.name.Equals(newSlot.item.name))
            {
                if (Input.GetKey(KeyCode.LeftControl) && newSlot.amount > 1 && oldSlot.amount >1)
                {
                    newSlot.amount++;
                    newSlot.itemAmount.text = newSlot.amount.ToString();
                    oldSlot.amount--;
                    oldSlot.itemAmount.text = oldSlot.amount.ToString();
                    
                    return;
                }
                else
                {
                     newSlot.amount += oldSlot.amount;
                    newSlot.itemAmount.text = newSlot.amount.ToString();
                    NullifySlotData();
                    return;
                }
                   
            }
        }
        
        newSlot.item = oldSlot.item;
        newSlot.amount = oldSlot.amount;
        if (oldSlot.isEmpty == false)
        {
            newSlot.SetIcon(oldSlot.iconGO.GetComponent<Image>().sprite);
            newSlot.itemAmount.text = oldSlot.amount.ToString();
        }
        else
        {
            newSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            newSlot.iconGO.GetComponent<Image>().sprite = null;
            newSlot.itemAmount.text = "";
        }

        newSlot.isEmpty = oldSlot.isEmpty;

        
        oldSlot.item = item;
        oldSlot.amount = amount;
        if (isEmpty == false)
        {
            oldSlot.SetIcon(item.icon);
            oldSlot.itemAmount.text = amount.ToString();
        }
        else
        {
            oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            oldSlot.iconGO.GetComponent<Image>().sprite = null;
            oldSlot.itemAmount.text = "";
        }

        oldSlot.isEmpty = isEmpty;
    }
}
