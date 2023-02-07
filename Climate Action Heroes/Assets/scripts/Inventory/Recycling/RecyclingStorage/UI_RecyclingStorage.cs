using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_RecyclingStorage : MonoBehaviour
{
    private RecyclingSystem storage;
    private Transform storage_BG;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    //[SerializeField] private UI_WeightCounter weightCounter;
    //[SerializeField] private UIShopSell uiShopSell;

    private IShopCustomer shopCustomer;

    public void Awake()
    {
        storage_BG = transform.Find("RecyclingStorage_BG");
        itemSlotContainer = storage_BG.Find("RecyclingStorage_Container");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
    }

    private void Start()
    {
        Hide();
    }

    public void SetStorage(RecyclingSystem storage)
    {
        this.storage = storage;

        storage.OnStorageListChanged += Storage_OnStorageListChanged;

        RefreshInventoryItems();
    }

    private void Storage_OnStorageListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
        Debug.Log("event works");
    }

    private void RefreshInventoryItems()
    {
        //inventory interface
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        for (int refreshCounter = 0; refreshCounter < storage.getStorageList().Count; refreshCounter++)
        {
            Transform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer);
            itemSlotRectTransform.gameObject.SetActive(true);

            Image image = itemSlotRectTransform.Find("Slot_Image").GetComponent<Image>();
            image.sprite = storage.getStorageList()[refreshCounter].getSprite();
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("text").GetComponent<TextMeshProUGUI>();
            if (storage.getStorageList()[refreshCounter].amount > 1)
            {
                uiText.SetText(storage.getStorageList()[refreshCounter].amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }

            //button checks for click
            MarkButton(itemSlotRectTransform, refreshCounter);
        }

    }

    private void MarkButton(Transform itemSlot, int index)
    {
        itemSlot.GetComponent<Button>().onClick.RemoveAllListeners();

        itemSlot.GetComponent<Button>().onClick.AddListener(delegate { TryFit(index); });
    }

    public void TryFit(int i)
    {
        Debug.Log("not an issue with the button");
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(shopCustomer.TryFitWeight(storage.getStorageList()[i].getWeight() * storage.getStorageList()[i].amount))
            {
                MoveItems(i);
            }
        }
        else
        {
            if(shopCustomer.TryFitWeight(storage.getStorageList()[i].getWeight()))
            {
                MoveItem(i);
            }
        }
    }

    void MoveItems(int i)
    {
        shopCustomer.AddAllItemPlayer(storage.getStorageList()[i]);
        storage.RemoveAll(i);
    }

    void MoveItem(int i)
    {
        Item tempItem = new Item(storage.getStorageList()[i].itemType, 1);
        shopCustomer.Add1ItemPlayer(tempItem);
        storage.RemoveOneItem(i);
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
