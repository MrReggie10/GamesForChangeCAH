using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_WeightCounter : MonoBehaviour
{
    private Transform weightNumber_Container;
    private Transform weightTop_text;
    private Transform weightBottom_text;

    void Awake()
    {
        weightNumber_Container = transform.Find("WeightNumber_Container");
        weightTop_text = weightNumber_Container.Find("WeightTop_text");
        weightBottom_text = weightNumber_Container.Find("WeightBottom_text");
    }

    public void Refresh(InventorySystem inventory)
    {
        TextMeshProUGUI currentWeightText = weightTop_text.GetComponent<TextMeshProUGUI>();
        currentWeightText.SetText((Mathf.Round(inventory.getCurrentWeight() * 10) / 10).ToString());

        TextMeshProUGUI maxWeightText = weightBottom_text.GetComponent<TextMeshProUGUI>();
        maxWeightText.SetText(inventory.getMaxWeight().ToString());
    }
}
