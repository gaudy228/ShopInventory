
using System.IO;
using UnityEngine;


public class SaveManager : MonoBehaviour
{
    private InventorySlot slot;
    private string filePath;
    private void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
        slot = GetComponent<InventorySlot>();
        slot = Load();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {

        }
    }
    private InventorySlot Load()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            InventorySlot data = JsonUtility.FromJson<InventorySlot>(json);
            Debug.Log("Данные загружены: " + json);
            return data;
        }
        else
        {
            Debug.LogError("Файл не найден!");
            return null;
        }
    }
    private void Save()
    {
        for (int i = 0; i < slot.allItems.Count; i++)
        {
            if (slot.item == slot.allItems[i])
            {
                slot.indexItem = i;
            }
        }
        InventorySlot data = new InventorySlot
        {
            indexItem = 1,
            amount = 5,
            isEmpty = false,
        };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
        Debug.Log("Данные сохранены: " + json);
    }
}
