using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private ItemDictionary itemDictionary;

    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;

    Dictionary<int, int> itemsCountCache = new();
    public event Action onInventoryChanged;
    public static InventoryController Instance;

    void Awake()
    {
        Instance = this;
        itemDictionary = FindFirstObjectByType<ItemDictionary>();
        if (inventoryPanel.transform.childCount == 0 && slotCount > 0)
        {
            for (int i = 0; i < slotCount; i++) Instantiate(slotPrefab, inventoryPanel.transform);
        }
    }

    public bool AddItem(Item worldItem)
    {
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject prefab = itemDictionary.GetItemPrefab(worldItem.ID);
                if (prefab == null)
                {
                    Debug.LogWarning("AddItem: prefab not found for ID " + worldItem.ID);
                    return false;
                }

                GameObject newItem = Instantiate(prefab, slotTransform);
                RectTransform rt = newItem.GetComponent<RectTransform>();
                rt.anchoredPosition = Vector2.zero;
                rt.localScale = Vector3.one;
                rt.localPosition = new Vector3(0, 0, 0);

                slot.currentItem = newItem;
                RebuildItemCounts();
                return true;
            }
        }

        Debug.Log("Inventory is full!");
        return false;
    }

    public void RebuildItemCounts()
    {
        itemsCountCache.Clear();

        Slot[] slots = inventoryPanel.GetComponentsInChildren<Slot>();
        foreach (var slot in slots)
        {
            if (slot.currentItem != null && slot.currentItem.TryGetComponent(out Item item))
            {
                itemsCountCache[item.ID] = itemsCountCache.GetValueOrDefault(item.ID, 0) + item.quantity;
            }
        }
        onInventoryChanged?.Invoke();
    }

    public Dictionary<int, int> GetItemCounts() => itemsCountCache;


    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> invData = new List<InventorySaveData>();

        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                if (item != null)
                {
                    invData.Add(new InventorySaveData
                    {
                        itemID = item.ID,
                        slotIndex = slotTransform.GetSiblingIndex()
                    });
                }
            }
        }

        return invData;
    }

    public void SetInventoryItems(List<InventorySaveData> inventorySaveData)
    {
        for (int i = inventoryPanel.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(inventoryPanel.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }

        foreach (InventorySaveData data in inventorySaveData)
        {
            if (data.slotIndex < inventoryPanel.transform.childCount)
            {
                Transform slotTransform = inventoryPanel.transform.GetChild(data.slotIndex);
                Slot slot = slotTransform.GetComponent<Slot>();
                GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);

                if (itemPrefab != null && slot != null)
                {
                    GameObject item = Instantiate(itemPrefab, slotTransform);
                    RectTransform rt = item.GetComponent<RectTransform>();
                    rt.anchoredPosition = Vector2.zero;
                    rt.localScale = Vector3.one;
                    rt.localPosition = new Vector3(0, 0, 0);

                    slot.currentItem = item;
                }
            }
        }

        if (inventoryPanel.TryGetComponent<LayoutGroup>(out LayoutGroup layout))
        {
            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(inventoryPanel.GetComponent<RectTransform>());
        }
        RebuildItemCounts();
    }

}