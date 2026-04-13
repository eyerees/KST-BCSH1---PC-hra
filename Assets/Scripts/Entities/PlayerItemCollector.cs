using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private InventoryController inventoryController;

    void Awake()
    {
        inventoryController = FindFirstObjectByType<InventoryController>();

        if (inventoryController == null)
        {
            Debug.LogError("PlayerItemCollector: No InventoryController found in scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (inventoryController == null) return;

        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                bool itemAdded = inventoryController.AddItem(item);
                if (itemAdded)
                {
                    item.PickUp();
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}