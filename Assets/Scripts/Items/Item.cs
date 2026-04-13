using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int ID;
    public string Name;
    public int quantity = 1;

    public GameObject worldPrefab;

    public virtual void UseItem()
    {
        Debug.Log("Using item: " + Name);
    }

    public virtual void PickUp()
    {
        Sprite itemIcon = GetComponent<Image>().sprite;
        if (ItemPickupUIController.Instance != null)
        {
            ItemPickupUIController.Instance.ShowItemPickupPopup(Name, itemIcon);
        }
    }
}

