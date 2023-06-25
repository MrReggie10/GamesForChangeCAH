using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopSell : MonoBehaviour
{
    [Header("Container Fields")]

    [SerializeField] private Transform sellBG;
    [SerializeField] private Button sell1;
    [SerializeField] private Button sellAll;

    private IShopCustomer shopCustomer;

    [Header("Other Fields")]

    [SerializeField] private Animator BGAnimator;
    private bool animationPlaying = false;

    private int itemIndex;

    private void Awake()
    {
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
