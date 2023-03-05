using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Crafting : MonoBehaviour
{
    [SerializeField] private Item[] itemArray;
    private List<GameObject> buttonArray = new List<GameObject>();

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
            CreateItemButton(item.itemType, item.getSprite(), item.getName(), Item.getRecipe(item.itemType));
        }

        Hide();
    }
    
    private void CreateItemButton(Item.ItemType type, Sprite itemSprite, string itemName, List<Item> items)
    {
        GameObject buttonTransform = Instantiate(button, container).gameObject;
        buttonArray.Add(buttonTransform);
        buttonTransform.gameObject.SetActive(true);

        buttonTransform.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        buttonTransform.transform.Find("ItemCost_Container").GetComponent<UI_CraftingRecipe>().CreateCraftingList(items);

        buttonTransform.transform.Find("ItemIcon").GetComponent<Image>().sprite = itemSprite;

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
        buttonTransform.GetComponent<Button>().onClick.AddListener(delegate { TryCraftItem(type); });
    }

    private void TryCraftItem(Item.ItemType type)
    {

        /*
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
        */
            if (shopCustomer.TryUseItems(type))
            {
                shopCustomer.CraftItem(type);
            }
        //}
        
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
