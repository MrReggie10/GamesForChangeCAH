using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingManager : MonoBehaviour
{
    [SerializeField] UI_RecyclingStorage uiRecycling;

    [SerializeField] private float maxWeight;
    private RecyclingSystem storage;

    [SerializeField] private int currentTrucks;

    [SerializeField] private List<Item> items;

    private void Awake()
    {
        storage = new RecyclingSystem();
        storage.setMaxWeight(maxWeight);
        Invoke("delayedAwake", 0.01f);
    }

    void delayedAwake()
    {
        uiRecycling.SetStorage(storage);
        storage.addItem(items[0]);
    }
}
