using NUnit.Framework.Internal;
using System;
using Unity.Mathematics;
using UnityEngine;
[Serializable] public struct RandomItemSettings
{
    RandomItemSettings(GameObject Item, int Chance)
    {
        this.Item = Item;
        this.ChanceToGet = Chance;
    }
    public GameObject Item;
    public int ChanceToGet;
}
public class GenerateRandomItem : MonoBehaviour
{
    [SerializeField] private RandomItemSettings[] ItemArray = new RandomItemSettings[] { };
    int ItemChanceCount = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < ItemArray.Length; ++i)
        {
            ItemChanceCount += ItemArray[i].ChanceToGet;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            
            if(player != null)
            {
                int RandNum = UnityEngine.Random.Range(0, ItemChanceCount + 1); 
                for(int i = 0; i < ItemArray.Length; ++i)
                {
                   RandNum -= ItemArray[i].ChanceToGet;
                    if(RandNum <= 0)
                    {
                        RandNum = i;
                        break;
                    }   
                }
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
