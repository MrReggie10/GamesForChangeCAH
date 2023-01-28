using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BikeShop : MonoBehaviour
{
    [SerializeField] private List<Bikes> bikeArray;

    private Transform bg;
    private Transform container;
    private Transform button;
    private IShopCustomer shopCustomer;

    private void Awake()
    {
        bg = transform.Find("BikeShop_BG");
        container = bg.Find("BikeShop_Container");
        button = container.transform.Find("BikeShop_Button");
        button.gameObject.SetActive(false);
    }

    private void Start()
    {
        foreach (Bikes bike in bikeArray)
        {
            CreateBikeButton(bike.bikeType, bike.GetSprite(), bike.GetName(), Bikes.GetCost(bike.bikeType));
        }

        Hide();
    }

    private void CreateBikeButton(Bikes.BikeType type, Sprite bikeSprite, string bikeName, int bikeCost)
    {
        Transform buttonTransform = Instantiate(button, container);
        buttonTransform.gameObject.SetActive(true);

        buttonTransform.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(bikeName);
        buttonTransform.Find("ItemCost").GetComponent<TextMeshProUGUI>().SetText(bikeCost.ToString());

        button.transform.Find("ItemIcon").GetComponent<Image>().sprite = bikeSprite;

        //button checks for click
        buttonTransform.GetComponent<Button>().onClick.AddListener(delegate { TryBuyBike(type); });
    }

    public void TryBuyBike(Bikes.BikeType bikeType)
    {
        if (shopCustomer.TrySpendCashAmount(Bikes.GetCost(bikeType)))
        {
            shopCustomer.EquipBike(bikeType);
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
