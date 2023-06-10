using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingSystem
{
    public event EventHandler OnStorageListChanged;

    private List<Item> storageList;
    private float currentWeight;
    private float maxWeight;

    public RecyclingSystem()
    {
        storageList = new List<Item>();
    }

    public void addItem(Item item)
    {
        if (item.isStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in storageList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += 1;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                storageList.Add(item);
            }
        }
        else
        {
            storageList.Add(item);
        }
        OnStorageListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveOneItem(int index)
    {
        storageList[index].amount -= 1;
        if (storageList[index].amount <= 0)
        {
            storageList.RemoveAt(index);
        }

        OnStorageListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveAll(int index)
    {
        storageList.RemoveAt(index);

        OnStorageListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> getStorageList()
    {
        return storageList;
    }

    public float getCurrentWeight()
    {
        currentWeight = 0;
        foreach (Item inventoryItem in storageList)
        {
            float temp = inventoryItem.getWeight();
            temp *= inventoryItem.amount;
            currentWeight += temp;
        }
        return currentWeight;
    }

    public float getMaxWeight()
    {
        return maxWeight;
    }

    public void setMaxWeight(float max)
    {
        maxWeight = max;
    }
}
