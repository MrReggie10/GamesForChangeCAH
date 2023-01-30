using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Crafting : MonoBehaviour
{
    [SerializeField] private Item[] itemArray;

    private Transform bg;
    private Transform container;
    private Transform button;
    private IShopCustomer shopCustomer;

    private void Awake()
    {
        bg = transform.Find("Crafting_BG");
        container = bg.Find("CraftingMenu_Container");
        button = container.transform.Find("CraftingMenu_Button");
        button.gameObject.SetActive(false);
    }

    private void Start()
    {
        foreach (Item item in itemArray)
        {
            CreateItemButton(item.itemType, item.getSprite(), item.getName(), Item.getRecipe(item.itemType));
        }

        Hide();
    }

    
    private void CreateItemButton(Item.ItemType type, Sprite itemSprite, string itemName, List<Item> items)
    {
        Transform buttonTransform = Instantiate(button, container);
        buttonTransform.gameObject.SetActive(true);

        buttonTransform.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        buttonTransform.Find("ItemCost_Container").GetComponent<UI_CraftingRecipe>().CreateCraftingList(items);

        button.transform.Find("ItemIcon").GetComponent<Image>().sprite = itemSprite;

        //button checks for click
        buttonTransform.GetComponent<Button>().onClick.AddListener(delegate { TryCraftItem(type); });
    }

    private void TryCraftItem(Item.ItemType type)
    {
        float ingredientsWeight = 0;
        if(Item.getRecipe(type) != null)
        {
            foreach(Item item in Item.getRecipe(type))
            {
                ingredientsWeight += item.getWeight() * item.amount;
            }
        }
        if(shopCustomer.TryFitWeight(Item.getWeight(type)-ingredientsWeight))
        {
            if (shopCustomer.TryUseItems(type))
            {
                shopCustomer.CraftItem(type);
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
