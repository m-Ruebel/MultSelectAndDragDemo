﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    string[] resoureNames =
        {
            "Gold",
            "Food",
            "Wood",
            "Stone"
        };
    int[] baseCapacity = //baseMaxGoldCapacity = 1000; baseMaxFoodCapacity = 1000; baseMaxWoodCapacity = 1000; baseMaxStoneCapacity = 1000;
    {
        1000, 1000, 1000, 1000
    };
    int[] starting = // startingGold; startingFood; startingWood; startingStone; 
{
        10, 50, 25, 10
    };
    int[] baseTick =  //baseGoldTick = 1, baseFoodTick = 10, baseWoodTick = 5, baseStoneTick = 2;
    {
        1, 10, 5, 2
    };
    public int[] current = new int[4];  //currentGold, currentFood, curremtWood, currentStone;
    int[] maxCapacity = new int[4];  // maxGoldCapacity, maxFoodCapacity, maxWoodCapacity, maxStoneCapacity;
    private readonly int numberOfResources = 4;
    float resourceTick = 5.0f;
    float tickTime = 5.0f;
    public static int[] addedMultipliers = new int[4];
    public static int[] addedResources = new int[4]; 
    // Start is called before the first frame update
    private static ResourceManager _resourceInstance;
    public static ResourceManager ResourceInstance
    {
        get
        {
            if (_resourceInstance == null)
            {
                Debug.Log("resourceManager is null");
            }
            return _resourceInstance;
        }
    }
    void Awake()
    {
        _resourceInstance = this;
        for (int i = 0; i < numberOfResources; i++)
        {
            maxCapacity[i] = baseCapacity[i];
            current[i] = starting[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > resourceTick)
        {
            Tick();
            resourceTick = Time.time + tickTime;
        }
    }
    void Tick()
    {
        int[] tempChange = new int[4];
        for (int i = 0; i < numberOfResources; i++) 
        {
            tempChange[i] = baseTick[i]; //add baseTickFirst
            tempChange[i] += addedResources[i]; //add addedResources
            int tempInt = tempChange[i];
            float mult = (addedMultipliers[i]) / 100;
            float resourceMult = tempInt * mult;
            tempChange[i] = mult != 0 ? (int)resourceMult : tempInt;
            UpdateResource(i, tempChange[i]);
        }
    }
    public void UpdateResource(int resource, int amount)
    {
        //Debug.Log(amount + " :amount");
        current[resource] += amount;
        //Debug.Log(current[resource] + " :current[resource]");
        if (current[resource] > maxCapacity[resource])
        {
            current[resource] = maxCapacity[resource];
            UIManager.Instance.UpdateAndRemoveErrorText("You have a max capacity of " + resoureNames[resource]);
        }
        else if (current[resource] < 0)
        {
            current[resource] = 0;
            UIManager.Instance.UpdateAndRemoveErrorText("You have ran our of " + resoureNames[resource]);
        }
        UIManager.Instance.UpdateResourceText(new Vector2(resource, current[resource]));
    }
    private void LateUpdate()
    {
        
    }
}