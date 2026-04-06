using UnityEngine;
using System.IO;
using Unity.Cinemachine;
using System.Collections.Generic;
using System.Linq;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private InventoryController inventoryController;
    private HotbarController hotbarController;
    private Chest[] chests;

    void Start()
    {
        IntializeComponents();
        LoadGame();
    }

    private void IntializeComponents()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        //C:\Users\Ira\AppData\LocalLow\DefaultCompany\Bramble
        inventoryController = Object.FindFirstObjectByType<InventoryController>();
        hotbarController = Object.FindFirstObjectByType<HotbarController>();
        chests = Object.FindObjectsByType<Chest>(FindObjectsSortMode.None);
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            data.playerPosition = player.transform.position;
        }

        data.inventorySaveData = inventoryController.GetInventoryItems();
        data.hotbarSaveData = hotbarController.GetHotbarItems();
        data.chestSaveData = GetChestsState();

        var confiner = Object.FindFirstObjectByType<CinemachineConfiner2D>();
        if (confiner != null && confiner.BoundingShape2D != null)
        {
            data.mapBoundary = confiner.BoundingShape2D.gameObject.name;
        }

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveLocation, json);
        
        Debug.Log("Game Saved to: " + saveLocation);
    }

    private List<ChestSaveData> GetChestsState()
    {
        List<ChestSaveData> chestStates = new List<ChestSaveData>();

        foreach (Chest chest in chests)
        {
            ChestSaveData chestSaveData = new ChestSaveData
            {
                chestID = chest.ChestID,
                isOpened = chest.IsOpened
            };
            chestStates.Add(chestSaveData);
        }

        return chestStates;
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            string json = File.ReadAllText(saveLocation);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = data.playerPosition;
            }

            var confiner = Object.FindFirstObjectByType<CinemachineConfiner2D>();
            if (confiner != null && !string.IsNullOrEmpty(data.mapBoundary))
            {
                GameObject boundaryObj = GameObject.Find(data.mapBoundary);
                if (boundaryObj != null)
                {
                    confiner.BoundingShape2D = boundaryObj.GetComponent<Collider2D>();
                }
            }

            MapController_Manual.Instance.HiglightArea(data.mapBoundary);

            inventoryController.SetInventoryItems(data.inventorySaveData);
            hotbarController.SetHotbarItems(data.hotbarSaveData);

            LoadChestsState(data.chestSaveData);

            Debug.Log("Game Loaded");
        }
        else
        {
            SaveGame();

            inventoryController.SetInventoryItems(new List<InventorySaveData>());
            hotbarController.SetHotbarItems(new List<InventorySaveData>());

            MapController_Manual.Instance.HiglightArea("");
        }
    }

    private void LoadChestsState(List<ChestSaveData> chestStates)
    {
        if (chests == null || chestStates == null) return;

        foreach (Chest chest in chests)
        {
            ChestSaveData chestSaveData = chestStates.FirstOrDefault(c => c.chestID == chest.ChestID);
            if (chestSaveData != null)            
            {
                chest.SetOpened(chestSaveData.isOpened);
            }
        }
    }
}