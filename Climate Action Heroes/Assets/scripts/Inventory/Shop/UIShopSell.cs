using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopSell : MonoBehaviour
{

    private IShopCustomer shopCustomer;

    private Transform sellBG;
    private Button sell1;
    private Button sellAll;

    private int itemIndex;

    private void Awake()
    {
        Hide();

        sellBG = transform.Find("Sell_BG");
        sell1 = sellBG.Find("Sell1_Button").GetComponent<Button>();
        sellAll = sellBG.Find("Sell2_Button").GetComponent<Button>();

        sell1.interactable = false;
        sellAll.interactable = false;
    }

    public void setItemForSale(int index)
    {
        itemIndex = index;
        sell1.interactable = true;
        sellAll.interactable = true;

        sell1.onClick.RemoveAllListeners();
        sellAll.onClick.RemoveAllListeners();

        sell1.onClick.AddListener(delegate { shopCustomer.Remove1ItemPlayer(itemIndex); });
        sellAll.onClick.AddListener(delegate { shopCustomer.RemoveAllItemPlayer(itemIndex); });
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
