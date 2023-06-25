using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_WeightCounter : MonoBehaviour
{
    [Header("Container Fields")]

    [SerializeField] private Transform weightNumber_Container;
    [SerializeField] private Transform weightTop_text;
    [SerializeField] private Transform weightBottom_text;

    void Awake()
    {
        
    }

    public void Refresh(InventorySystem inventory)
    {
        TextMeshProUGUI currentWeightText = weightTop_text.GetComponent<TextMeshProUGUI>();
        currentWeightText.SetText((Mathf.Round(inventory.getCurrentWeight() * 10) / 10).ToString());

        TextMeshProUGUI maxWeightText = weightBottom_text.GetComponent<TextMeshProUGUI>();
        maxWeightText.SetText(inventory.getMaxWeight().ToString());
    }
}
