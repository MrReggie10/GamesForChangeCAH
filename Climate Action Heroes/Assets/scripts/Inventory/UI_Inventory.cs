using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    private InventorySystem inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private Transform ui_WeightCounter;
    private Transform weightNumber_Container;
    private Transform weightTop_text;
    private Transform weightBottom_text;


    public void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");

        ui_WeightCounter = transform.Find("UI_WeightCounter");
        weightNumber_Container = ui_WeightCounter.Find("WeightNumber_Container");
        weightTop_text = weightNumber_Container.Find("WeightTop_text");
        weightBottom_text = weightNumber_Container.Find("WeightBottom_text");
    }

    public void SetInventory(InventorySystem inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        //inventory interface
        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 125f;
        foreach (Item item in inventory.getItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Slot_Image").GetComponent<Image>();
            image.sprite = item.getSprite();
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("text").GetComponent<TextMeshProUGUI>();
            if(item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
            
            x++;
            if(x > 3)
            {
                x = 0;
                y--;
            }
            
        }


        //weight counter
        TextMeshProUGUI currentWeightText = weightTop_text.GetComponent<TextMeshProUGUI>();
        currentWeightText.SetText(inventory.getCurrentWeight().ToString());

        TextMeshProUGUI maxWeightText = weightBottom_text.GetComponent<TextMeshProUGUI>();
        maxWeightText.SetText(inventory.getMaxWeight().ToString());

    }

}
