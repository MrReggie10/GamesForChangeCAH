using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_RecyclingStorageShop : MonoBehaviour
{
    private Transform shop_BG;
    private Transform shop_container;
    private Transform count_text;
    private Transform purchase_button;
    private Transform cost_text;

    [SerializeField] private RecyclingManager manager;

    [SerializeField] private int[] cost;
    [SerializeField] private int[] storageUpgrades;
    private int counter = 0;

    private bool animationPlaying = false;
    [SerializeField] private Animator BGAnimator;

    private IShopCustomer shopCustomer;

    private void Awake()
    {
        shop_BG = transform.Find("RecyclingStorageShop_BG");
        shop_container = shop_BG.Find("RecyclingStorageShop_Container");
        count_text = shop_container.Find("Count_text");
        purchase_button = shop_container.Find("Upgrade_button");
        cost_text = purchase_button.Find("Cost_text");

        manager.SetMaxWeight(storageUpgrades[counter]);
        cost_text.GetComponent<TextMeshProUGUI>().SetText(cost[counter].ToString());
        count_text.GetComponent<TextMeshProUGUI>().SetText(storageUpgrades[counter].ToString());

        purchase_button.GetComponent<Button>().onClick.AddListener(delegate { TryUpdateStorage(); });
    }

    private void Start()
    {
        Invoke("DelayedStart", 0.01f);
    }

    private void DelayedStart()
    {
        this.gameObject.SetActive(false);
    }

    private void TryUpdateStorage()
    {
        if(shopCustomer.TrySpendCashAmount(cost[counter]))
        {
            counter++;

            manager.SetMaxWeight(storageUpgrades[counter]);
            count_text.GetComponent<TextMeshProUGUI>().SetText(storageUpgrades[counter].ToString());

            if(counter >= cost.Length-1)
            {
                cost_text.GetComponent<TextMeshProUGUI>().SetText("Max");
                purchase_button.GetComponent<Button>().enabled = false;
            }
            else
            {
                cost_text.GetComponent<TextMeshProUGUI>().SetText(cost[counter].ToString());
            }
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
