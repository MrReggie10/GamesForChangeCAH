using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingManager : MonoBehaviour
{
    private RecyclingSystem storage;

    [SerializeField] UI_RecyclingStorage uiRecycling;

    [SerializeField] private float maxWeight;
    [SerializeField] private int currentTrucks;

    [SerializeField] private List<Item> rareItems;
    [SerializeField] private List<Item> legItems;

    [SerializeField] private int oneOutOfThisIsLegendary;
    [SerializeField] private float spawnTime;

    private float timer = 0;

    private void Awake()
    {
        storage = new RecyclingSystem();
        storage.setMaxWeight(maxWeight);
        Invoke("delayedAwake", 0.01f);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawnTime)
        {
            //delete
            ProgressionManager.progressionManager.AddXP(25);

            timer = 0;
            for (int i = 0; i < currentTrucks*2; i++)
            {
                AddItem();
            }
        }
    }

    void delayedAwake()
    {
        uiRecycling.SetStorage(storage);
    }

    private void AddItem()
    {
        if(Random.Range(0, oneOutOfThisIsLegendary) == 1)
        {
            int i = Random.Range(0, legItems.Count - 1);
            if (legItems[i].getWeight() <= storage.getMaxWeight() - storage.getCurrentWeight())
            {
                storage.addItem(legItems[i]);
            }
        }
        else
        {
            int i = Random.Range(0, rareItems.Count - 1);
            if (rareItems[i].getWeight() <= storage.getMaxWeight() - storage.getCurrentWeight())
            {
                storage.addItem(rareItems[i]);
            }
        }

    }

    public void SetMaxWeight(float weight)
    {
        maxWeight = weight;
    }

    public void SetTrucks(int trucks)
    {
        currentTrucks = trucks;
    }
}
