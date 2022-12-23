using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem
{
    public event EventHandler OnItemListChanged;

    private List<Item> itemList;
    private float currentWeight;

    public InventorySystem()
    {
        itemList = new List<Item>();

    }

    public void addItem(Item item)
    {
        if(item.isStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach(Item inventoryItem in itemList)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += 1;
                    itemAlreadyInInventory = true;
                }
            }
            if(!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> getItemList()
    {
        return itemList;
    }

    public float getCurrentWeight()
    {
        currentWeight = 0;
        foreach(Item inventoryItem in itemList)
        {
            float temp = inventoryItem.getWeight();
            temp *= inventoryItem.amount;
            currentWeight += temp;
        }
        return currentWeight;
    }
}
