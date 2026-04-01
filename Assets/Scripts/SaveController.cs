using UnityEngine;
using System.IO;
using Unity.Cinemachine;
using System.Collections.Generic;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private InventoryController inventoryController;
    private HotbarController hotbarController;


    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        inventoryController = Object.FindFirstObjectByType<InventoryController>();
        hotbarController = Object.FindFirstObjectByType<HotbarController>();

        LoadGame();
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

        var confiner = Object.FindFirstObjectByType<CinemachineConfiner2D>();
        if (confiner != null && confiner.BoundingShape2D != null)
        {
            data.mapBoundary = confiner.BoundingShape2D.gameObject.name;
        }

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveLocation, json);
        
        Debug.Log("Game Saved to: " + saveLocation);
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

            inventoryController.SetInventoryItems(data.inventorySaveData);
            hotbarController.SetHotbarItems(data.hotbarSaveData);

            Debug.Log("Game Loaded");
        }
        else
        {
            SaveGame();
        }
    }
}