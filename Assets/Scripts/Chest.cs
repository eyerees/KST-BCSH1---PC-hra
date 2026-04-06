using System.Runtime.InteropServices;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{

    public bool IsOpened { get; private set; }
    public string ChestID { get; private set; }
    public GameObject itemPrefab;    
    public Sprite openedSprite;
    void Start()
    {
        ChestID ??= GlobalHelper.GenerateUniqueID(gameObject);
    }

    public void Interact()
    {
        if (!CanInteract()) return;
        OpenChest();
    }

    public bool CanInteract()
    {
        return !IsOpened;
    }

    private void OpenChest()
    {
        SetOpened(true);
        SoundEffectManager.Play("ChestOpen");

        if (itemPrefab)
        {
            GameObject droppedItem = Instantiate(itemPrefab, transform.position + Vector3.up, Quaternion.identity);
            droppedItem.GetComponent<BounceEffect>().StartBounce();
        }
    }
    public void SetOpened(bool opened)
    {
        IsOpened = opened;
        if (IsOpened)
        {
            GetComponent<SpriteRenderer>().sprite = openedSprite;
        }
    }
}