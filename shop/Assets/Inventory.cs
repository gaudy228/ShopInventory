using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public DataBase data;

  public List<InventoryItems> items = new List<InventoryItems>();

    public GameObject gameObjectShow;

    public GameObject InventoryMainObject;

    public int maxCount;

    public Camera cam;
    public EventSystem ev;
    public int currentID;
    public InventoryItems currentItem;

    public RectTransform movingObject;
    public Vector3 offset;
    public void Start()
    {
        if(items.Count == 0)
        {
            AddGraphics();
        }
        for(int i = 0; i < maxCount; i++) // тест
        {
            AddItem(i, data.items[Random.Range(0, data.items.Count)], Random.Range(1, 99));
        }
        UpdateInventory();  
    }
    public void Update()
    {
       
        
            if (currentID != -1 )
            {
                MoveObject();
            }
        
    }

    public void SearchForSameItem(Item item, int count)
    {
        for(int i = 0;i < maxCount;i++)
        {
            if (items[i].id == item.id)
            {
                if (items[0].count < 128)
                {
                    items[i].count += count;
                    if (items[i].count > 128)
                    {
                        count = items[i].count - 128;
                        items[i].count = 64;
                    }
                    else
                    {
                        count = 0;
                        i = maxCount;
                    }
                }
            }
        }
        if(count > 0)
        {
            for(int i = 0; i <maxCount; i++)
            {
                if (items[i].id == 0)
                {
                    AddItem(i, item, count);
                    i = maxCount;
                }
            }
        }
    }
    public void AddItem(int id, Item item, int count)
    {
        items[id].id = item.id;
        items[id].count = count;
        items[id].itemGameObj.GetComponent<Image>().sprite = item.img;
        if(count > 1 && item.id != 0)
        {
            items[id].itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = count.ToString();
        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }
    public void AddInventoryItem(int id, InventoryItems invItem)
    {
        items[id].id = invItem.id;
        items[id].count = invItem.count;
        items[id].itemGameObj.GetComponent<Image>().sprite = data.items[invItem.id].img;
        if (invItem.count > 1 && invItem.id != 0)
        {
            items[id].itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = invItem.count.ToString();
        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }
    public void AddGraphics()
    {
        for(int i = 0; i < maxCount; i++)
        {
            GameObject newItem = Instantiate(gameObjectShow, InventoryMainObject.transform) as GameObject;

            newItem.name = i.ToString();

            InventoryItems ii = new InventoryItems();
            ii.itemGameObj = newItem;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponentInChildren<Button>();
            tempButton.onClick.AddListener(delegate { SelectObject(); });
            items.Add(ii);
        }
    }
    public void UpdateInventory()
    {
        for (int i = 0;i < maxCount; i++)
        {
            if (items[i].id != 0 && items[i].count > 1)
            {
                items[i].itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = items[i].count.ToString();

            }
            else
            {
                items[i].itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
            items[i].itemGameObj.GetComponent<Image>().sprite = data.items[items[i].id].img;
        }
    }
    public void SelectObject()
    {
        if(currentID == -1)
        {
            currentID = int.Parse (ev.currentSelectedGameObject.name);
            currentItem = CopyInventoryItem(items[currentID]);
            movingObject.gameObject.SetActive(true);
            movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].img;

            AddItem(currentID, data.items[0],0 );
        }
        else
        {
            InventoryItems II = items[int.Parse(ev.currentSelectedGameObject.name)];
            if (currentItem.id != II.id)
            {
                AddInventoryItem(currentID, II);

               AddInventoryItem(int.Parse(ev.currentSelectedGameObject.name), currentItem);

            }
            else
            {
                if(II.count + currentItem.count <= 128)
                {
                    II.count += currentItem.count;
                }
                else
                {
                    AddItem(currentID, data.items[II.id], II.count + currentItem.count - 128);
                    II.count = 128;
                }
                II.itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = II.count.ToString();
            }
            
            currentID = -1;
            movingObject.gameObject.SetActive (false);
        }
    }
    public void MoveObject()
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (currentID != 0)
            {
       
                Vector3 pos = Input.mousePosition + offset;
        
                
                pos.z = InventoryMainObject.GetComponent<RectTransform>().position.z;
        
                movingObject.position = cam.ScreenToWorldPoint(pos);

            }
        }
            
    }
    public InventoryItems CopyInventoryItem(InventoryItems old)
    {
        InventoryItems New = new InventoryItems();

        New.id = old.id;
        New.itemGameObj = old.itemGameObj;
        New.count = old.count;

        return New;
    }
}
[System.Serializable]

public class InventoryItems
{
    public int id;
    public GameObject itemGameObj;

    public int count;
}