using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite cigarette;
    public Sprite plasticBag;
    public Sprite plasticBottle;
    public Sprite straw;
    public Sprite bottleCap;
    public Sprite foodWrapper;

    public Sprite tinCan;
    public Sprite rubber;
    public Sprite copperSheet;
    public Sprite ironSheet;
    public Sprite glassBottle;
    public Sprite cardboard;

    public Sprite windRotor;
    public Sprite waterTurbine;
    public Sprite solarCell;

    public Sprite windmill_lvl_1;
    public Sprite windmill_lvl_2;
    public Sprite windmill_lvl_3;

    public Sprite dam_lvl_1;
    public Sprite dam_lvl_2;
    public Sprite dam_lvl_3;

    public Sprite solarPanel_lvl_1;
    public Sprite solarPanel_lvl_2;
    public Sprite solarPanel_lvl_3;

    public Sprite blueBike;
    public Sprite redBike;
    public Sprite electricBike;
    public Sprite rainbowBike;
}
