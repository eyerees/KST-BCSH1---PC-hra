using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardController : MonoBehaviour
{
    public static RewardController Instance { get; private set; }

    private ItemDictionary itemDictionary;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        itemDictionary = FindAnyObjectByType<ItemDictionary>();
    }

    public void GiveQuestRewards(QuestReward reward)
    {
        switch (reward.type)
        {
            case RewardType.Item:
                GiveItemReward(reward.rewardID, reward.amount);
                break;
            case RewardType.Gold:
                // TODO: implement gold reward
                break;
            case RewardType.Experience:
                // TODO: implement XP reward
                break;
        }
    }

    // Overload for QuestProgress (which actually holds the rewards list)
    public void GiveQuestRewards(QuestProgress questProgress)
    {
        if (questProgress == null || questProgress.rewards == null) return;
        foreach (QuestReward reward in questProgress.rewards)
            GiveQuestRewards(reward);
    }

    public void GiveItemReward(int itemID, int amount)
    {
        if (itemDictionary == null) return;

        GameObject itemPrefab = itemDictionary.GetItemPrefab(itemID);
        if (itemPrefab == null) return;

        Item itemComponent = itemPrefab.GetComponent<Item>();
        if (itemComponent == null) return;

        for (int i = 0; i < amount; i++)
        {
            if (!InventoryController.Instance.AddItem(itemComponent))
            {
                GameObject dropItem = Instantiate(itemPrefab, transform.position + Vector3.down, Quaternion.identity);
                dropItem.GetComponent<BounceEffect>()?.StartBounce();
            }
            else
            {
                itemComponent.ShowPopUp();
            }
        }
    }
}