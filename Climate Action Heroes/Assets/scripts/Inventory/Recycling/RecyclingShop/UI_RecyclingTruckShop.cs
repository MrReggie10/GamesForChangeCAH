using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_RecyclingTruckShop : MonoBehaviour
{
    [Header("Container Fields")]

    [SerializeField] private Transform shop_BG;
    [SerializeField] private Transform shop_container;
    [SerializeField] private Transform count_text;
    [SerializeField] private Transform purchase_button;
    [SerializeField] private Transform cost_text;

    [Header("Other Fields")]

    [SerializeField] private RecyclingManager manager;

    [SerializeField] private int[] cost;
    [SerializeField] private int[] truckUpgrades;
    private int counter = 0;

    private bool animationPlaying = false;
    [SerializeField] private Animator BGAnimator;

    private IShopCustomer shopCustomer;

    private void Awake()
    {
        shop_BG = transform.Find("RecyclingTruckShop_BG");
        shop_container = shop_BG.Find("RecyclingTruckShop_Container");
        count_text = shop_container.Find("Count_text");
        purchase_button = shop_container.Find("Upgrade_button");
        cost_text = purchase_button.Find("Cost_text");

        cost_text.GetComponent<TextMeshProUGUI>().SetText(cost[counter].ToString());
        count_text.GetComponent<TextMeshProUGUI>().SetText(truckUpgrades[counter].ToString());

        purchase_button.GetComponent<Button>().onClick.AddListener(delegate { TryUpdateTrucks(); });
    }

    private void Start()
    {
        Invoke("DelayedStart", 0.01f);
    }

    private void DelayedStart()
    {
        this.gameObject.SetActive(false);
    }

    private void TryUpdateTrucks()
    {
        if (shopCustomer.TrySpendCashAmount(cost[counter]))
        {
            FindObjectOfType<AudioManager>().PlaySound("buy");

            counter++;

            manager.SetTrucks(truckUpgrades[counter]);
            count_text.GetComponent<TextMeshProUGUI>().SetText(truckUpgrades[counter].ToString());

            if (counter >= cost.Length - 1)
            {
                cost_text.GetComponent<TextMeshProUGUI>().SetText("Max");
                purchase_button.GetComponent<Button>().enabled = false;
            }
            else
            {
                cost_text.GetComponent<TextMeshProUGUI>().SetText(cost[counter].ToString());
            }
        }
        else
        {
            FindObjectOfType<AudioManager>().PlaySound("error");
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

