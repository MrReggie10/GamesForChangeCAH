using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_RecyclingStorage : MonoBehaviour
{
    [Header("Container Fields")]

    [SerializeField] private Transform storage_BG;
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlotTemplate;
    private RecyclingSystem storage;

    [Header("Other Fields")]

    [SerializeField] private Animator BGAnimator;
    private bool animationPlaying = false;

    private IShopCustomer shopCustomer;

    public void Awake()
    {

    }

    private void Start()
    {
        Invoke("DelayedStart", 0.01f);
    }

    private void DelayedStart()
    {
        this.gameObject.SetActive(false);
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
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(shopCustomer.TryFitWeight(storage.getStorageList()[i].getWeight() * storage.getStorageList()[i].amount))
            {
                FindObjectOfType<AudioManager>().PlaySound("buttonPress");

                MoveItems(i);
            }
            else
            {
                FindObjectOfType<AudioManager>().PlaySound("error");
            }
        }
        else
        {
            if(shopCustomer.TryFitWeight(storage.getStorageList()[i].getWeight()))
            {
                FindObjectOfType<AudioManager>().PlaySound("buttonPress");

                MoveItem(i);
            }
            else
            {
                FindObjectOfType<AudioManager>().PlaySound("error");
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
