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
    public Sprite glassBottle;
    public Sprite straw;
    public Sprite bottleCap;

    public Sprite windmill_lvl_1;


}
