using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_RecyclingTruckShop : MonoBehaviour
{
    private Transform shop_BG;
    private Transform shop_container;
    private Transform count_text;
    private Transform purchase_button;
    private Transform cost_text;

    [SerializeField] private RecyclingManager manager;

    [SerializeField] private int[] cost;
    [SerializeField] private int[] truckUpgrades;
    private int counter = 0;

    private IShopCustomer shopCustomer;

    private void Awake()
    {
        shop_BG = transform.Find("RecyclingTruckShop_BG");
        shop_container = shop_BG.Find("RecyclingTruckShop_Container");
        count_text = shop_container.Find("Count_text");
        purchase_button = shop_container.Find("Upgrade_button");
        cost_text = purchase_button.Find("Cost_text");

        manager.SetMaxWeight(truckUpgrades[counter]);
        cost_text.GetComponent<TextMeshProUGUI>().SetText(cost[counter].ToString());
        count_text.GetComponent<TextMeshProUGUI>().SetText(truckUpgrades[counter].ToString());

        purchase_button.GetComponent<Button>().onClick.AddListener(delegate { TryUpdateTrucks(); });
    }

    private void Start()
    {
        Hide();
    }

    private void TryUpdateTrucks()
    {
        if (shopCustomer.TrySpendCashAmount(cost[counter]))
        {
            counter++;

            manager.SetTrucks(truckUpgrades[counter]);
            count_text.GetComponent<TextMeshProUGUI>().SetText(truckUpgrades[counter].ToString());

            if (counter >= cost.Length - 1)
            {
                cost_text.GetComponent<TextMeshProUGUI>().SetText("Max");
                purchase_button.GetComponent<Button>().enabled = false;
            }
            else
            {
                cost_text.GetComponent<TextMeshProUGUI>().SetText(cost[counter].ToString());
            }
        }
    }

    public void Show(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

