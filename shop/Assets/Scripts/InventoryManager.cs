using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject UIBG;
    public GameObject inventoryPanel;
    public Transform InventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] private List<string> _pathSlots = new List<string>();
    public bool isOpened;
    private InventoryManager inventoryManager;
    void Start()
    {
        for (int i = 0; i < InventoryPanel.childCount; i++)
        {
            if (InventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slots.Add(InventoryPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
        //UIBG.SetActive(false);
        //inventoryPanel.SetActive(false);
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    isOpened = !isOpened;   
        //    if (isOpened)
        //    {
        //        UIBG.SetActive(true);
        //        inventoryPanel.SetActive(true);
        //    }
        //    else
        //    {
        //        UIBG.SetActive(false);
        //        inventoryPanel.SetActive(false);
        //    }
        //}
    }
    public void AddItem(ItemScriptableObject _item, int _amount)
    {
        foreach(InventorySlot slot in slots)
        {
            if(slot.item == _item)
            {
                if(slot.amount + _amount <= _item.maximumAmount)
                {
                    slot.amount += _amount;
                    slot.itemAmount.text = slot.amount.ToString();
                    return;
                }
                
                break;
            }
        }
        foreach(InventorySlot slot in slots)
        {
            if(slot.isEmpty == true)
            {
                slot.item = _item;
                slot.amount = _amount;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);
                slot.itemAmount.text = _amount.ToString();
                break;
            }
        }
    }
    private void Load()
    {
        for (int i = 0; i < slots.Count;i++)
        {
            _pathSlots[i] = Path.Combine(Application.persistentDataPath, $"data{i}.json");
        }
        if(_pathSlots != null)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i] = JsonUtility.FromJson<InventorySlot>(File.ReadAllText(_pathSlots[i]));
            }
        }
    }
    private void Save()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            _pathSlots[i] = Path.Combine(Application.persistentDataPath, $"data{i}.json");
        }
        for (int i = 0; i < slots.Count; i++)
        {
            File.WriteAllText(_pathSlots[i], JsonUtility.ToJson(slots[i]));
        }
    }
}
