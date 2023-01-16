using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_CashAmount : MonoBehaviour
{
    private Transform cashText;
    [SerializeField] private Transform cashTextShop;

    private void Awake()
    {
        cashText = transform.Find("CashAmount_text");
    }

    public void setCashText(int cash)
    {
        cashText.GetComponent<TextMeshProUGUI>().SetText(cash.ToString());
        cashTextShop.GetComponent<TextMeshProUGUI>().SetText(cash.ToString());
    }
}
