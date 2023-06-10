using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerlinePropController : MonoBehaviour
{
    public static PowerlinePropController powerlinePropController;

    [SerializeField] private List<PowerlineProp> props;

    private void Awake()
    {
        powerlinePropController = this;
    }

    public void SetQuestActive()
    {
        foreach (PowerlineProp prop in props)
        {
            prop.SetPropActive();
        }
    }
}
