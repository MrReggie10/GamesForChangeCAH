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
        straw,
        bottle_cap,

        wind_turbine_lvl_1,
    }

    public ItemType itemType;
    public int amount;

    public Item()
    {

    }

    public Item(ItemType itemType, int amount)
    {
        this.itemType = itemType;
        this.amount = amount;
    }

    public String getName()
    {
        switch(itemType)
        {
            default:
            case ItemType.cigarette:        return "Cigarette";
            case ItemType.glass_bottle:     return "Glass Bottle";
            case ItemType.plastic_bag:      return "Plastic Bag";
            case ItemType.plastic_bottle:   return "Plastic Bottle";
            case ItemType.straw:            return "Straw";
            case ItemType.bottle_cap:       return "Bottle Cap";

            case ItemType.wind_turbine_lvl_1:   return "Windmill Lvl 1";
        }
    }

    public Sprite getSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.cigarette:        return ItemAssets.Instance.cigarette;
            case ItemType.glass_bottle:     return ItemAssets.Instance.glassBottle;
            case ItemType.plastic_bag:      return ItemAssets.Instance.plasticBag;
            case ItemType.plastic_bottle:   return ItemAssets.Instance.plasticBottle;
            case ItemType.straw:            return ItemAssets.Instance.straw;
            case ItemType.bottle_cap:       return ItemAssets.Instance.bottleCap;

            case ItemType.wind_turbine_lvl_1:   return ItemAssets.Instance.windmill_lvl_1;
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
            case ItemType.straw:
                return true;
            case ItemType.bottle_cap:
                return true;

            case ItemType.wind_turbine_lvl_1:
                return false;
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
            case ItemType.straw:
                return 0.2f;
            case ItemType.bottle_cap:
                return 0.2f;
            case ItemType.wind_turbine_lvl_1:
                return 2.5f;
        }
    }

    public static float getWeight(ItemType itemType)
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
            case ItemType.straw:
                return 0.2f;
            case ItemType.bottle_cap:
                return 0.2f;
            case ItemType.wind_turbine_lvl_1:
                return 2.5f;
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
                return 20;
            case ItemType.straw:
                return 5;
            case ItemType.bottle_cap:
                return 5;
            case ItemType.wind_turbine_lvl_1:
                return 50;
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
                return 30;
            case ItemType.straw:
                return 10;
            case ItemType.bottle_cap:
                return 10;
        }

    }

    public static List<Item> getRecipe(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.wind_turbine_lvl_1:
                return new List<Item>{new Item(ItemType.plastic_bottle, 4), new Item(ItemType.straw, 2), new Item(ItemType.bottle_cap, 2)};
        }
    }
}
