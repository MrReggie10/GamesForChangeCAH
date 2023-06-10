using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopSell : MonoBehaviour
{

    private IShopCustomer shopCustomer;

    private Transform sellBG;
    private Button sell1;
    private Button sellAll;

    private bool animationPlaying = false;
    [SerializeField] private Animator BGAnimator;

    private int itemIndex;

    private void Awake()
    {
        sellBG = transform.Find("Sell_BG");
        sell1 = sellBG.Find("Sell1_Button").GetComponent<Button>();
        sellAll = sellBG.Find("Sell2_Button").GetComponent<Button>();

        sell1.interactable = false;
        sellAll.interactable = false;
    }

    private void Start()
    {
        Invoke("DelayedStart", 0.01f);
    }

    private void DelayedStart()
    {
        this.gameObject.SetActive(false);
    }

    public void setItemForSale(int index)
    {
        itemIndex = index;
        sell1.interactable = true;
        sellAll.interactable = true;

        sell1.onClick.RemoveAllListeners();
        sellAll.onClick.RemoveAllListeners();

        sell1.onClick.AddListener(delegate { removeOneItem(itemIndex); });
        sellAll.onClick.AddListener(delegate { removeAllItem(itemIndex); });
    }

    private void removeOneItem(int itemIndex)
    {
        FindObjectOfType<AudioManager>().PlaySound("buttonPress");
        shopCustomer.Remove1ItemPlayer(itemIndex);
    }

    private void removeAllItem(int itemIndex)
    {
        FindObjectOfType<AudioManager>().PlaySound("buttonPress");
        shopCustomer.RemoveAllItemPlayer(itemIndex);
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
