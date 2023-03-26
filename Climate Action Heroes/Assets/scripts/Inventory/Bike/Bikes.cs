using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Bikes
{
    public enum BikeType
    {
        blue_bike,
        red_bike,
        electric_bike,
        rainbow_bike,
    }

    public BikeType bikeType;

    public Bikes()
    {

    }

    public Bikes(BikeType bikeType)
    {
        this.bikeType = bikeType;
    }

    public String GetName()
    {
        switch(bikeType)
        {
            default:
            case BikeType.blue_bike:    return "Blue Bike";
            case BikeType.red_bike:     return "Red Bike";
            case BikeType.electric_bike:return "Electric Bike";
            case BikeType.rainbow_bike: return "Rainbow Bike";
            
        }
    }

    public static String GetName(BikeType bikeTypeStatic)
    {
        switch (bikeTypeStatic)
        {
            default:
            case BikeType.blue_bike:    return "Blue Bike";
            case BikeType.red_bike:     return "Red Bike";
            case BikeType.electric_bike:return "Electric Bike";
            case BikeType.rainbow_bike: return "Rainbow Bike";

        }
    }

    public Sprite GetSprite()
    {
        //FIX LATER
        switch (bikeType)
        {
            default:
            case BikeType.blue_bike:    return ItemAssets.Instance.cigarette;
            case BikeType.red_bike:     return ItemAssets.Instance.glassBottle;
            case BikeType.electric_bike:return ItemAssets.Instance.plasticBag;
            case BikeType.rainbow_bike: return ItemAssets.Instance.plasticBottle;
        }
    }

    public int GetCost()
    {
        switch (bikeType)
        {
            default:
            case BikeType.blue_bike:    return 500;
            case BikeType.red_bike:     return 2500;
            case BikeType.electric_bike:return 5000;
            case BikeType.rainbow_bike: return 20000;

        }
    }

    public static int GetCost(BikeType bikeTypeStatic)
    {
        switch (bikeTypeStatic)
        {
            default:
            case BikeType.blue_bike:    return 500;
            case BikeType.red_bike:     return 2500;
            case BikeType.electric_bike:return 5000;
            case BikeType.rainbow_bike: return 20000;

        }
    }

    public float GetStorage()
    {
        switch (bikeType)
        {
            default:
            case BikeType.blue_bike:    return 20f;
            case BikeType.red_bike:     return 40f;
            case BikeType.electric_bike:return 100f;
            case BikeType.rainbow_bike: return 250f;
        }
    }

    public static float GetStorage(BikeType bikeTypeStatic)
    {
        switch (bikeTypeStatic)
        {
            default:
            case BikeType.blue_bike:    return 20f;
            case BikeType.red_bike:     return 40f;
            case BikeType.electric_bike:return 100f;
            case BikeType.rainbow_bike: return 250f;
        }
    }

    public float GetMoveSpeed()
    {
        switch(bikeType)
        {
            default:
            case BikeType.blue_bike:    return 7f;
            case BikeType.red_bike:     return 10f;
            case BikeType.electric_bike:return 13f;
            case BikeType.rainbow_bike: return 20f;
        }
    }

    public static float GetMoveSpeed(BikeType bikeTypeStatic)
    {
        switch (bikeTypeStatic)
        {
            default:
            case BikeType.blue_bike:    return 7f;
            case BikeType.red_bike:     return 10f;
            case BikeType.electric_bike:return 13f;
            case BikeType.rainbow_bike: return 20f;
        }
    }

    public int GetPhase()
    {
        switch (bikeType)
        {
            default:
            case BikeType.blue_bike: return 2;
            case BikeType.red_bike: return 2;
            case BikeType.electric_bike: return 3;
            case BikeType.rainbow_bike: return 3;
        }
    }

    public static int GetPhase(BikeType bikeTypeStatic)
    {
        switch (bikeTypeStatic)
        {
            default:
            case BikeType.blue_bike: return 2;
            case BikeType.red_bike: return 2;
            case BikeType.electric_bike: return 3;
            case BikeType.rainbow_bike: return 3;
        }
    }
}
