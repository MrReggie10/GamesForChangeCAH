using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_BikeShop : MonoBehaviour
{
    [SerializeField] private List<Bikes> bikeArray;
    private List<GameObject> buttonArray = new List<GameObject>();

    private Transform bg;
    private Transform container;
    private Transform button;

    private bool animationPlaying = false;
    [SerializeField] private Animator BGAnimator;

    private IShopCustomer shopCustomer;

    private void Awake()
    {
        bg = transform.Find("BikeShop_BG");
        container = bg.Find("BikeShop_Container");
        button = container.transform.Find("BikeShop_Button");
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
            if (bikeArray[i].GetPhase() > ProgressionManager.progressionManager.GetPhase())
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
        foreach (Bikes bike in bikeArray)
        {
            CreateBikeButton(bike.bikeType, bike.GetSprite(), bike.GetName(), Bikes.GetCost(bike.bikeType));
        }

        this.gameObject.SetActive(false);
    }

    private void CreateBikeButton(Bikes.BikeType type, Sprite bikeSprite, string bikeName, int bikeCost)
    {
        GameObject buttonTransform = Instantiate(button, container).gameObject;
        buttonArray.Add(buttonTransform);
        buttonTransform.gameObject.SetActive(true);

        buttonTransform.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(bikeName);
        buttonTransform.transform.Find("ItemCost").GetComponent<TextMeshProUGUI>().SetText(bikeCost.ToString());

        buttonTransform.transform.Find("ItemIcon").GetComponent<Image>().sprite = bikeSprite;

        if (Bikes.GetPhase(type) > ProgressionManager.progressionManager.GetPhase())
        {
            buttonTransform.transform.Find("Locked_BG").gameObject.SetActive(true);
            GameObject tempBG = buttonTransform.transform.Find("Locked_BG").gameObject;
            tempBG.transform.Find("Locked_text").GetComponent<TextMeshProUGUI>().SetText("Locked until Phase " + Bikes.GetPhase(type));
            buttonTransform.transform.GetComponent<Button>().enabled = false;
        }
        else
        {
            buttonTransform.transform.Find("Locked_BG").gameObject.SetActive(false);
            buttonTransform.transform.GetComponent<Button>().enabled = true;
        }

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
