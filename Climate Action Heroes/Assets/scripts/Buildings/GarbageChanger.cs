using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageChanger : MonoBehaviour
{
    public static GarbageChanger garbageChanger;

    [SerializeField] private List<GarbageObject> garbs;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        garbageChanger = this;
    }

    public void Take1Garb()
    {
        if(garbs.Count != 0)
        {
            int randint = Random.Range(0, garbs.Count - 1);
            garbs[randint].Show();
            if (garbs[randint].GetLevel() == 0)
            {
                garbs.RemoveAt(randint);
            }
        }
    }
}
