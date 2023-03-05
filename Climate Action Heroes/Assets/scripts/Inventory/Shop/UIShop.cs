using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIShop : MonoBehaviour
{
    [SerializeField] private Item[] itemArray;
    private List<GameObject> buttonArray = new List<GameObject>();

    private Transform container;
    private GameObject button;
    private IShopCustomer shopCustomer;

    private void Awake()
    {
        container = transform. Find("ShopMenuBuy_Container");
        button = container.transform.Find("ShopMenuBuy_Button").gameObject;
        button.gameObject.SetActive(false);

        Invoke("DelayedAwake", 0.01f);
    }

    private void DelayedAwake()
    {
        ProgressionManager.progressionManager.OnPhaseChange += ReloadButtons;
    }

    private void ReloadButtons(object sender, EventArgs e)
    {
        for (int i = 0; i < buttonArray.Count; i++)
        {
            if (itemArray[i].GetPhase() > ProgressionManager.progressionManager.GetPhase())
            {
                buttonArray[i].transform.Find("Locked_BG").gameObject.SetActive(true);
                buttonArray[i].transform.GetComponent<Button>().enabled = false;
            }
            else
            {
                buttonArray[i].transform.Find("Locked_BG").gameObject.SetActive(false);
                buttonArray[i].transform.GetComponent<Button>().enabled = true;
            }
        }
    }

    private void Start()
    {
        Invoke("DelayedStart", 0.01f);
    }

    private void DelayedStart()
    {
        foreach (Item item in itemArray)
        {
            CreateItemButton(item.itemType, item.getName(), Item.getBuy(item.itemType));
        }

        Hide();
    }

    private void CreateItemButton(Item.ItemType type, string itemName, int itemCost)
    {
        GameObject buttonTransform = Instantiate(button, container);
        buttonArray.Add(buttonTransform);
        buttonTransform.SetActive(true);

        buttonTransform.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        buttonTransform.transform.Find("ItemCost").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());

        buttonTransform.transform.Find("ItemIcon").GetComponent<Image>().sprite = Item.getSprite(type);

        if (Item.GetPhase(type) > ProgressionManager.progressionManager.GetPhase())
        {
            buttonTransform.transform.Find("Locked_BG").gameObject.SetActive(true);
            GameObject tempBG = buttonTransform.transform.Find("Locked_BG").gameObject;
            tempBG.transform.Find("Locked_text").GetComponent<TextMeshProUGUI>().SetText("Locked until Phase " + Item.GetPhase(type));
            buttonTransform.transform.GetComponent<Button>().enabled = false;
        }
        else
        {
            buttonTransform.transform.Find("Locked_BG").gameObject.SetActive(false);
            buttonTransform.transform.GetComponent<Button>().enabled = true;
        }

        //button checks for click
        buttonTransform.GetComponent<Button>().onClick.AddListener(delegate { TryBuyItem(type); });
    }

    public void TryBuyItem(Item.ItemType itemType)
    {
        if(shopCustomer.TryFitWeight(Item.getWeight(itemType)-0.01f))
        {
            if (shopCustomer.TrySpendCashAmount(Item.getBuy(itemType)))
            {
                shopCustomer.BoughtItem(itemType);
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
