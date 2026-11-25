using NUnit.Framework.Internal;
using System;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
[Serializable] public struct RandomItemSettings
{
    RandomItemSettings(GameObject Item, int Chance)
    {
        this.item = Item;
        this.chanceToGet = Chance;
    }
    public GameObject item;
    public int chanceToGet;
}
[Serializable]
public struct PlacementSetting
{
    public int placement;
    public RandomItemSettings[] itemOptions;
}
public class GenerateRandomItem : MonoBehaviour
{
    [SerializeField] PlacementSetting[] placementSettings;
    [SerializeField] private GameObject playerPrefab;
    int ItemChanceCount = 0;
    CameraManager cameraScript;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraScript = GameObject.Find("CameraManager").GetComponent<CameraManager>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (placementSettings.Length < 1)
                return;
            PlacementSetting itemArray = new PlacementSetting();
            CameraManager.player CurrentPlayer = cameraScript.FindPlayer(other.gameObject);
            itemArray.placement = 0;
            itemArray.itemOptions = placementSettings[0].itemOptions;   // Defaults to first option
            for(int i = 0; i < placementSettings.Length; ++i)
            {
                if (placementSettings[i].placement == CurrentPlayer.placement)
                {
                    itemArray.itemOptions = placementSettings[i].itemOptions;
                    itemArray.placement = placementSettings[i].placement;
                }
            }
            for (int i = 0; i < itemArray.itemOptions.Length; ++i)
            {
                ItemChanceCount += itemArray.itemOptions[i].chanceToGet;
            }

            PlayerPowerups playerPowerups = other.gameObject.GetComponent<PlayerPowerups>();
            
            if(playerPowerups != null)
            {
                int RandNum = UnityEngine.Random.Range(0, ItemChanceCount); 
                for(int i = 0; i < itemArray.itemOptions.Length; ++i)
                {
                   RandNum -= itemArray.itemOptions[i].chanceToGet;
                    if(RandNum <= 0)
                    {
                        playerPowerups.powerupObj = itemArray.itemOptions[i].item;  // Moved this here to avoid issues with
                        break;
                    }   
                }
            }
            ItemChanceCount = 0;    // Forgot to reset item chance count
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
