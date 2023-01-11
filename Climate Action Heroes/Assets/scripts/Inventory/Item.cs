using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        cigarette,
        plastic_bag,
        plastic_bottle,
        glass_bottle,
    }

    public ItemType itemType;
    public int amount;

    public Sprite getSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.cigarette:        return ItemAssets.Instance.cigarette;
            case ItemType.glass_bottle:     return ItemAssets.Instance.glassBottle;
            case ItemType.plastic_bag:      return ItemAssets.Instance.plasticBag;
            case ItemType.plastic_bottle:   return ItemAssets.Instance.plasticBottle;
        }
    }

    public bool isStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.cigarette:
                return true;
            case ItemType.glass_bottle:
                return true;
            case ItemType.plastic_bag:
                return true;
            case ItemType.plastic_bottle:
                return true;
        }
         
    }

    public float getWeight()
    {
        switch (itemType)
        {
            default:
            case ItemType.cigarette:
                return 0.1f;
            case ItemType.plastic_bag:
                return 0.2f;
            case ItemType.plastic_bottle:
                return 0.5f;
            case ItemType.glass_bottle:
                return 1.0f;
        }
    }

    public int getSell()
    {
        switch (itemType)
        {
            default:
            case ItemType.cigarette:
                return 1;
            case ItemType.plastic_bag:
                return 5;
            case ItemType.plastic_bottle:
                return 10;
            case ItemType.glass_bottle:
                return 10;
        }
    }

    public static int getBuy(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.plastic_bag:
                return 10;
            case ItemType.plastic_bottle:
                return 15;
            case ItemType.glass_bottle:
                return 15;
        }

    }
}
