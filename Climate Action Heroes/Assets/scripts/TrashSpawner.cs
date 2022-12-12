using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] commonTrash;
    [SerializeField] private GameObject[] rareTrash;
    [SerializeField] private GameObject[] legendaryTrash;

    public int maxTrash;
    private float numberOfTrash;
    [SerializeField] private float k;
    [SerializeField] private float spawnInterval;

    private float timer = 0;
    private float permTimer = 0;
    private float previousSpawnNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        permTimer += Time.deltaTime;
        numberOfTrash = (maxTrash - maxTrash * Mathf.Exp(k * permTimer));

        timer += Time.deltaTime;
        if(timer > spawnInterval)
        {
            for (int i = 0; i < numberOfTrash - previousSpawnNumber; i++)
            {
                SpawnPieceOfTrash();
            }
            previousSpawnNumber = numberOfTrash;
            timer = timer % 3;
        }
    }

    /*
    void onWaveAnimationPlay()
    {
        for(int i = 0; i < numberOfTrashThisTime; i++) {
            SpawnPieceOfTrash();
        }
    }
     */

    void SpawnPieceOfTrash()
    {
        float rand1 = Random.Range(0f, 1f);
        if(rand1 < 0.95)
        {
            int rand2 = Random.Range(0, commonTrash.Length);
            GameObject temp = Instantiate(commonTrash[rand2], transform);

            float rand3 = Random.Range(-10, 0);
            float rand4 = Random.Range(-50, 50);
            temp.transform.position += new Vector3(rand3, rand4, 0);
        }
        /*
        else if(rand1 < 0.999)
        {
            int rand2 = Random.Range(0, rareTrash.Length);
            GameObject temp = Instantiate(rareTrash[rand2], transform);

            float rand3 = Random.Range(-10, 0);
            float rand4 = Random.Range(-50, 50);
            temp.transform.position += new Vector3(rand3, rand4, 0);
        }
        else
        {
            int rand2 = Random.Range(0, legendaryTrash.Length);
            GameObject temp = Instantiate(legendaryTrash[rand2], transform);

            float rand3 = Random.Range(-10, 0);
            float rand4 = Random.Range(-50, 50);
            temp.transform.position += new Vector3(rand3, rand4, 0);
        }
        */
    }
}
