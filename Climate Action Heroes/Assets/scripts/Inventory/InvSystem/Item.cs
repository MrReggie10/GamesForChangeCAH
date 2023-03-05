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
        food_wrapper,
        plastic_bag,
        plastic_bottle,
        straw,
        bottle_cap,

        tin_can,
        rubber,
        copper_sheet,
        iron_sheet,
        glass_bottle,
        cardboard,

        wind_rotor,
        water_turbine,
        solar_cell,

        wind_turbine_lvl_1,
        wind_turbine_lvl_2,
        wind_turbine_lvl_3,

        dam_lvl_1,
        dam_lvl_2,
        dam_lvl_3,

        solar_panel_lvl_1,
        solar_panel_lvl_2,
        solar_panel_lvl_3,
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
            case ItemType.plastic_bag:      return "Plastic Bag";
            case ItemType.plastic_bottle:   return "Plastic Bottle";
            case ItemType.straw:            return "Straw";
            case ItemType.bottle_cap:       return "Bottle Cap";
            case ItemType.food_wrapper:     return "Food Wrapper";

            case ItemType.tin_can:          return "Tin Can";
            case ItemType.rubber:           return "Rubber";
            case ItemType.copper_sheet:     return "Copper Sheet";
            case ItemType.iron_sheet:       return "Iron Sheet";
            case ItemType.glass_bottle:     return "Glass Bottle";
            case ItemType.cardboard:        return "Cardboard";

            case ItemType.wind_rotor:       return "Wind Rotor";
            case ItemType.water_turbine:    return "Water Turbine";
            case ItemType.solar_cell:       return "Solar Cell";

            case ItemType.wind_turbine_lvl_1:   return "Windmill Lvl 1";
            case ItemType.wind_turbine_lvl_2:   return "Windmill Lvl 2";
            case ItemType.wind_turbine_lvl_3:   return "Windmill Lvl 3";

            case ItemType.dam_lvl_1:        return "Dam Lvl 1";
            case ItemType.dam_lvl_2:        return "Dam Lvl 2";
            case ItemType.dam_lvl_3:        return "Dam Lvl 3";

            case ItemType.solar_panel_lvl_1:    return "Solar Panel Lvl 1";
            case ItemType.solar_panel_lvl_2:    return "Solar Panel Lvl 2";
            case ItemType.solar_panel_lvl_3:    return "Solar Panel Lvl 3";
        }
    }

    public Sprite getSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.cigarette:        return ItemAssets.Instance.cigarette;
            case ItemType.plastic_bag:      return ItemAssets.Instance.plasticBag;
            case ItemType.plastic_bottle:   return ItemAssets.Instance.plasticBottle;
            case ItemType.straw:            return ItemAssets.Instance.straw;
            case ItemType.bottle_cap:       return ItemAssets.Instance.bottleCap;
            case ItemType.food_wrapper:     return ItemAssets.Instance.foodWrapper;

            case ItemType.tin_can:          return ItemAssets.Instance.tinCan;
            case ItemType.rubber:           return ItemAssets.Instance.rubber;
            case ItemType.copper_sheet:     return ItemAssets.Instance.copperSheet;
            case ItemType.iron_sheet:       return ItemAssets.Instance.ironSheet;
            case ItemType.glass_bottle:     return ItemAssets.Instance.glassBottle;

            case ItemType.wind_rotor:       return ItemAssets.Instance.windRotor;
            case ItemType.water_turbine:    return ItemAssets.Instance.waterTurbine;
            case ItemType.solar_cell:       return ItemAssets.Instance.solarCell;

            case ItemType.wind_turbine_lvl_1:   return ItemAssets.Instance.windmill_lvl_1;
            case ItemType.wind_turbine_lvl_2:   return ItemAssets.Instance.windmill_lvl_2;
            case ItemType.wind_turbine_lvl_3:   return ItemAssets.Instance.windmill_lvl_3;

            case ItemType.dam_lvl_1:        return ItemAssets.Instance.dam_lvl_1;
            case ItemType.dam_lvl_2:        return ItemAssets.Instance.dam_lvl_2;
            case ItemType.dam_lvl_3:        return ItemAssets.Instance.dam_lvl_3;

            case ItemType.solar_panel_lvl_1:return ItemAssets.Instance.solarPanel_lvl_1;
            case ItemType.solar_panel_lvl_2:return ItemAssets.Instance.solarPanel_lvl_2;
            case ItemType.solar_panel_lvl_3:return ItemAssets.Instance.solarPanel_lvl_3;
        }
    }

    public static Sprite getSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.cigarette: return ItemAssets.Instance.cigarette;
            case ItemType.plastic_bag: return ItemAssets.Instance.plasticBag;
            case ItemType.plastic_bottle: return ItemAssets.Instance.plasticBottle;
            case ItemType.straw: return ItemAssets.Instance.straw;
            case ItemType.bottle_cap: return ItemAssets.Instance.bottleCap;
            case ItemType.food_wrapper: return ItemAssets.Instance.foodWrapper;

            case ItemType.tin_can: return ItemAssets.Instance.tinCan;
            case ItemType.rubber: return ItemAssets.Instance.rubber;
            case ItemType.copper_sheet: return ItemAssets.Instance.copperSheet;
            case ItemType.iron_sheet: return ItemAssets.Instance.ironSheet;
            case ItemType.glass_bottle: return ItemAssets.Instance.glassBottle;

            case ItemType.wind_rotor: return ItemAssets.Instance.windRotor;
            case ItemType.water_turbine: return ItemAssets.Instance.waterTurbine;
            case ItemType.solar_cell: return ItemAssets.Instance.solarCell;

            case ItemType.wind_turbine_lvl_1: return ItemAssets.Instance.windmill_lvl_1;
            case ItemType.wind_turbine_lvl_2: return ItemAssets.Instance.windmill_lvl_2;
            case ItemType.wind_turbine_lvl_3: return ItemAssets.Instance.windmill_lvl_3;

            case ItemType.dam_lvl_1: return ItemAssets.Instance.dam_lvl_1;
            case ItemType.dam_lvl_2: return ItemAssets.Instance.dam_lvl_2;
            case ItemType.dam_lvl_3: return ItemAssets.Instance.dam_lvl_3;

            case ItemType.solar_panel_lvl_1: return ItemAssets.Instance.solarPanel_lvl_1;
            case ItemType.solar_panel_lvl_2: return ItemAssets.Instance.solarPanel_lvl_2;
            case ItemType.solar_panel_lvl_3: return ItemAssets.Instance.solarPanel_lvl_3;
        }
    }

    public bool isStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.cigarette:
                return true;
            case ItemType.food_wrapper:
                return true;
            case ItemType.plastic_bag:
                return true;
            case ItemType.plastic_bottle:
                return true;
            case ItemType.straw:
                return true;
            case ItemType.bottle_cap:
                return true;

            case ItemType.tin_can:
                return true;
            case ItemType.rubber:
                return true;
            case ItemType.copper_sheet:
                return true;
            case ItemType.iron_sheet:
                return true;
            case ItemType.glass_bottle:
                return true;
            case ItemType.cardboard:
                return true;

            case ItemType.wind_rotor:
                return false;
            case ItemType.water_turbine:
                return false;
            case ItemType.solar_cell:
                return false;

            case ItemType.wind_turbine_lvl_1:
                return false;
            case ItemType.wind_turbine_lvl_2:
                return false;
            case ItemType.wind_turbine_lvl_3:
                return false;

            case ItemType.dam_lvl_1:
                return false;
            case ItemType.dam_lvl_2:
                return false;
            case ItemType.dam_lvl_3:
                return false;

            case ItemType.solar_panel_lvl_1:
                return false;
            case ItemType.solar_panel_lvl_2:
                return false;
            case ItemType.solar_panel_lvl_3:
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
            case ItemType.food_wrapper:
                return 0.1f;
            case ItemType.plastic_bag:
                return 0.2f;
            case ItemType.plastic_bottle:
                return 0.5f;
            case ItemType.straw:
                return 0.2f;
            case ItemType.bottle_cap:
                return 0.2f;

            case ItemType.tin_can:
                return 1.0f;
            case ItemType.rubber:
                return 1.0f;
            case ItemType.copper_sheet:
                return 2.0f;
            case ItemType.iron_sheet:
                return 2.0f;
            case ItemType.glass_bottle:
                return 1.0f;
            case ItemType.cardboard:
                return 0.5f;

            case ItemType.wind_rotor:
                return 10.0f;
            case ItemType.water_turbine:
                return 10.0f;
            case ItemType.solar_cell:
                return 10.0f;

            case ItemType.wind_turbine_lvl_1:
                return 2.5f;
            case ItemType.wind_turbine_lvl_2:
                return 7.5f;
            case ItemType.wind_turbine_lvl_3:
                return 25.0f;

            case ItemType.dam_lvl_1:
                return 5.0f;
            case ItemType.dam_lvl_2:
                return 12.0f;
            case ItemType.dam_lvl_3:
                return 40.0f;

            case ItemType.solar_panel_lvl_1:
                return 4.0f;
            case ItemType.solar_panel_lvl_2:
                return 10.0f;
            case ItemType.solar_panel_lvl_3:
                return 35.0f;
        }
    }

    public static float getWeight(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.cigarette:
                return 0.1f;
            case ItemType.food_wrapper:
                return 0.1f;
            case ItemType.plastic_bag:
                return 0.2f;
            case ItemType.plastic_bottle:
                return 0.5f;
            case ItemType.straw:
                return 0.2f;
            case ItemType.bottle_cap:
                return 0.2f;

            case ItemType.tin_can:
                return 1.0f;
            case ItemType.rubber:
                return 1.0f;
            case ItemType.copper_sheet:
                return 2.0f;
            case ItemType.iron_sheet:
                return 2.0f;
            case ItemType.glass_bottle:
                return 1.0f;
            case ItemType.cardboard:
                return 0.5f;

            case ItemType.wind_rotor:
                return 10.0f;
            case ItemType.water_turbine:
                return 10.0f;
            case ItemType.solar_cell:
                return 10.0f;

            case ItemType.wind_turbine_lvl_1:
                return 2.5f;
            case ItemType.wind_turbine_lvl_2:
                return 7.5f;
            case ItemType.wind_turbine_lvl_3:
                return 25.0f;

            case ItemType.dam_lvl_1:
                return 5.0f;
            case ItemType.dam_lvl_2:
                return 12.0f;
            case ItemType.dam_lvl_3:
                return 40.0f;

            case ItemType.solar_panel_lvl_1:
                return 4.0f;
            case ItemType.solar_panel_lvl_2:
                return 10.0f;
            case ItemType.solar_panel_lvl_3:
                return 35.0f;
        }
    }

    public int getSell()
    {
        switch (itemType)
        {
            default:
            case ItemType.cigarette:
                return 1;
            case ItemType.food_wrapper:
                return 2;
            case ItemType.plastic_bag:
                return 5;
            case ItemType.plastic_bottle:
                return 10;
            case ItemType.straw:
                return 5;
            case ItemType.bottle_cap:
                return 5;

            case ItemType.tin_can:
                return 25;
            case ItemType.rubber:
                return 25;
            case ItemType.copper_sheet:
                return 50;
            case ItemType.iron_sheet:
                return 50;
            case ItemType.glass_bottle:
                return 25;
            case ItemType.cardboard:
                return 10;

            case ItemType.wind_rotor:
                return 250;
            case ItemType.water_turbine:
                return 250;
            case ItemType.solar_cell:
                return 250;

            case ItemType.wind_turbine_lvl_1:
                return 50;
            case ItemType.wind_turbine_lvl_2:
                return 150;
            case ItemType.wind_turbine_lvl_3:
                return 500;

            case ItemType.dam_lvl_1:
                return 100;
            case ItemType.dam_lvl_2:
                return 240;
            case ItemType.dam_lvl_3:
                return 800;

            case ItemType.solar_panel_lvl_1:
                return 80;
            case ItemType.solar_panel_lvl_2:
                return 200;
            case ItemType.solar_panel_lvl_3:
                return 700;
        }
    }

    public static int getBuy(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.cigarette:
                return 2;
            case ItemType.food_wrapper:
                return 5;
            case ItemType.plastic_bag:
                return 10;
            case ItemType.plastic_bottle:
                return 25;
            case ItemType.straw:
                return 10;
            case ItemType.bottle_cap:
                return 10;

            case ItemType.tin_can:
                return 50;
            case ItemType.rubber:
                return 50;
            case ItemType.copper_sheet:
                return 100;
            case ItemType.iron_sheet:
                return 100;
            case ItemType.glass_bottle:
                return 50;
            case ItemType.cardboard:
                return 25;

            case ItemType.wind_rotor:
                return 750;
            case ItemType.water_turbine:
                return 750;
            case ItemType.solar_cell:
                return 750;
        }

    }

    public static List<Item> getRecipe(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.wind_turbine_lvl_1:
                return new List<Item>{new Item(ItemType.plastic_bottle, 2), new Item(ItemType.straw, 4), new Item(ItemType.bottle_cap, 4)};
            case ItemType.wind_turbine_lvl_2:
                return new List<Item> { new Item(ItemType.iron_sheet, 2), new Item(ItemType.cardboard, 2), new Item(ItemType.bottle_cap, 5), new Item(ItemType.straw, 5) };
            case ItemType.wind_turbine_lvl_3:
                return new List<Item> { new Item(ItemType.wind_rotor, 1), new Item(ItemType.plastic_bag, 50), new Item(ItemType.straw, 50) };

            case ItemType.dam_lvl_1:
                return new List<Item> { new Item(ItemType.plastic_bag, 10), new Item(ItemType.food_wrapper, 10), new Item(ItemType.plastic_bottle, 5) };
            case ItemType.dam_lvl_2:
                return new List<Item> { new Item(ItemType.glass_bottle, 5), new Item(ItemType.tin_can, 5), new Item(ItemType.plastic_bag, 12), new Item(ItemType.food_wrapper, 12) };
            case ItemType.dam_lvl_3:
                return new List<Item> { new Item(ItemType.water_turbine, 1), new Item(ItemType.food_wrapper, 100), new Item(ItemType.bottle_cap, 100) };

            case ItemType.solar_panel_lvl_1:
                return new List<Item> { new Item(ItemType.tin_can, 1), new Item(ItemType.rubber, 1), new Item(ItemType.copper_sheet, 1) };
            case ItemType.solar_panel_lvl_2:
                return new List<Item> { new Item(ItemType.glass_bottle, 2), new Item(ItemType.iron_sheet, 2), new Item(ItemType.copper_sheet, 3), new Item(ItemType.rubber, 3) };
            case ItemType.solar_panel_lvl_3:
                return new List<Item> { new Item(ItemType.solar_cell, 1), new Item(ItemType.cardboard, 10), new Item(ItemType.plastic_bottle, 50) };
        }
    }

    public int GetPhase()
    {
        switch(itemType)
        {
            default:
            case ItemType.cigarette:
                return 1;
            case ItemType.food_wrapper:
                return 1;
            case ItemType.plastic_bag:
                return 1;
            case ItemType.plastic_bottle:
                return 1;
            case ItemType.straw:
                return 1;
            case ItemType.bottle_cap:
                return 1;

            case ItemType.tin_can:
                return 2;
            case ItemType.rubber:
                return 2;
            case ItemType.copper_sheet:
                return 2;
            case ItemType.iron_sheet:
                return 2;
            case ItemType.glass_bottle:
                return 2;
            case ItemType.cardboard:
                return 2;

            case ItemType.wind_rotor:
                return 3;
            case ItemType.water_turbine:
                return 3;
            case ItemType.solar_cell:
                return 3;

            case ItemType.wind_turbine_lvl_1:
                return 1;
            case ItemType.wind_turbine_lvl_2:
                return 2;
            case ItemType.wind_turbine_lvl_3:
                return 3;

            case ItemType.dam_lvl_1:
                return 2;
            case ItemType.dam_lvl_2:
                return 2;
            case ItemType.dam_lvl_3:
                return 3;

            case ItemType.solar_panel_lvl_1:
                return 3;
            case ItemType.solar_panel_lvl_2:
                return 3;
            case ItemType.solar_panel_lvl_3:
                return 3;
        }
    }

    public static int GetPhase(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.cigarette:
                return 1;
            case ItemType.food_wrapper:
                return 1;
            case ItemType.plastic_bag:
                return 1;
            case ItemType.plastic_bottle:
                return 1;
            case ItemType.straw:
                return 1;
            case ItemType.bottle_cap:
                return 1;

            case ItemType.tin_can:
                return 2;
            case ItemType.rubber:
                return 2;
            case ItemType.copper_sheet:
                return 2;
            case ItemType.iron_sheet:
                return 2;
            case ItemType.glass_bottle:
                return 2;
            case ItemType.cardboard:
                return 2;

            case ItemType.wind_rotor:
                return 3;
            case ItemType.water_turbine:
                return 3;
            case ItemType.solar_cell:
                return 3;

            case ItemType.wind_turbine_lvl_1:
                return 1;
            case ItemType.wind_turbine_lvl_2:
                return 2;
            case ItemType.wind_turbine_lvl_3:
                return 3;

            case ItemType.dam_lvl_1:
                return 2;
            case ItemType.dam_lvl_2:
                return 2;
            case ItemType.dam_lvl_3:
                return 3;

            case ItemType.solar_panel_lvl_1:
                return 3;
            case ItemType.solar_panel_lvl_2:
                return 3;
            case ItemType.solar_panel_lvl_3:
                return 3;
        }
    }
}
