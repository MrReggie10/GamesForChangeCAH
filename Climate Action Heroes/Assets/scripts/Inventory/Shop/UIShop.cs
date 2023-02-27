using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIShop : MonoBehaviour
{
    [SerializeField] private Item[] itemArray;

    private Transform container;
    private Transform button;
    private IShopCustomer shopCustomer;

    private void Awake()
    {
        container = transform. Find("ShopMenuBuy_Container");
        button = container.transform.Find("ShopMenuBuy_Button");
        button.gameObject.SetActive(false);
    }

    private void Start()
    {
        foreach(Item item in itemArray)
        {
            CreateItemButton(item.itemType, item.getName(), Item.getBuy(item.itemType));
        }

        Hide();
    }

    private void CreateItemButton(Item.ItemType type, string itemName, int itemCost)
    {
        Transform buttonTransform = Instantiate(button, container);
        buttonTransform.gameObject.SetActive(true);

        buttonTransform.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        buttonTransform.Find("ItemCost").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());

        buttonTransform.Find("ItemIcon").GetComponent<Image>().sprite = Item.getSprite(type);

        //button checks for click
        buttonTransform.GetComponent<Button>().onClick.AddListener(delegate { TryBuyItem(type); });
    }

    public void TryBuyItem(Item.ItemType itemType)
    {
        if(shopCustomer.TryFitWeight(Item.getWeight(itemType)))
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
