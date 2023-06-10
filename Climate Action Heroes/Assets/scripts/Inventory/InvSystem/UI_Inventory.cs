using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    private InventorySystem inventory;
    private Transform inventory_BG;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    [SerializeField] private UI_WeightCounter weightCounter;
    [SerializeField] private UIShopSell uiShopSell;

    private bool animationPlaying = false;
    [SerializeField] private Animator BGAnimator;

    private IShopCustomer shopCustomer;

    public void Awake()
    {
        inventory_BG = transform.Find("Inventory_BG");
        itemSlotContainer = inventory_BG.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
    }

    private void Start()
    {
        Invoke("DelayedStart", 0.01f);
    }

    private void DelayedStart()
    {
        this.gameObject.SetActive(false);
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
        for(int refreshCounter = 0; refreshCounter < inventory.getItemList().Count; refreshCounter++)
        {
            Transform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer);
            itemSlotRectTransform.gameObject.SetActive(true);

            Image image = itemSlotRectTransform.Find("Slot_Image").GetComponent<Image>();
            image.sprite = inventory.getItemList()[refreshCounter].getSprite();
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("text").GetComponent<TextMeshProUGUI>();
            if (inventory.getItemList()[refreshCounter].amount > 1)
            {
                uiText.SetText(inventory.getItemList()[refreshCounter].amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }

            //button checks for click
            MarkButton(itemSlotRectTransform, refreshCounter);
        }

        weightCounter.Refresh(inventory);
    }

    private void MarkButton(Transform itemSlot, int index)
    {
        itemSlot.GetComponent<Button>().onClick.RemoveAllListeners();

        itemSlot.GetComponent<Button>().onClick.AddListener(delegate { MarkForSale(index); });
    }

    public void MarkForSale(int i)
    {
        FindObjectOfType<AudioManager>().PlaySound("buttonPress");

        uiShopSell.setItemForSale(i);
    }

    public void Show(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;
        if (!animationPlaying)
        {
            this.gameObject.SetActive(true);

            BGAnimator.SetBool("MenuOpen", true);

            StartCoroutine("WaitForAnimation");
        }
    }

    private IEnumerator WaitForAnimation()
    {
        animationPlaying = true;

        yield return new WaitForSeconds(1);

        animationPlaying = false;
        if (!BGAnimator.GetBool("MenuOpen"))
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Hide()
    {
        if (!animationPlaying)
        {
            BGAnimator.SetBool("MenuOpen", false);

            StartCoroutine("WaitForAnimation");
        }
    }

}
