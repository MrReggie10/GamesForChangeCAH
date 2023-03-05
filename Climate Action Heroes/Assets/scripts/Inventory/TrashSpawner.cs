using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{

    [SerializeField] private Item[] commonTrash;
    [SerializeField] private Item[] rareTrash;

    public int maxTrash;
    [SerializeField] private int phase3MaxTrash;
    private float trashToSpawn;
    private float currentBeachTrash = 0;

    [SerializeField] private float spawnInterval;
    private float timer = 0;


    // Update is called once per frame
    void Update()
    {
        if(ProgressionManager.progressionManager.GetPhase() == 3)
        {
            maxTrash = phase3MaxTrash;
        }

        timer += Time.deltaTime;
        if(timer > spawnInterval)
        {
            //delete
            ProgressionManager.progressionManager.AddXP(25);

            trashToSpawn = 0.2f * (maxTrash - currentBeachTrash);
            for (int i = 0; i < trashToSpawn; i++)
            {
                SpawnPieceOfTrash();
            }
            currentBeachTrash += trashToSpawn;
            timer = timer % spawnInterval;
        }
    }

    void SpawnPieceOfTrash()
    {
        float rand1 = Random.Range(0f, 1f);
        if(rand1 < 0.95)
        {
            int rand2 = Random.Range(0, commonTrash.Length);
            ItemWorld.SpawnItemWorld(new Vector3(Random.Range(-45f, -35f), Random.Range(-25f, 25f)), commonTrash[rand2]);

        }
        else if(ProgressionManager.progressionManager.GetPhase() >= 2)
        {
            int rand2 = Random.Range(0, rareTrash.Length);
            ItemWorld.SpawnItemWorld(new Vector3(Random.Range(-45f, -35f), Random.Range(-25f, 25f)), rareTrash[rand2]);

        }

    }

    public void changeCurrentBeachTrash()
    {
        currentBeachTrash--;
    }
}
