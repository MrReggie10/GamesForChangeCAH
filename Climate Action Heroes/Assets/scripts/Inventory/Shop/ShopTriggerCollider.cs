using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTriggerCollider : MonoBehaviour
{
    [SerializeField] private UIShop uiShop;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private UIShopSell uiSell;
    [SerializeField] private Animator fadeAnimator;

    [SerializeField] private GameObject speechGrid;

    private IShopCustomer shopCustomer;
    private bool playerIsClose;

    private bool shopOpen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        shopCustomer = collision.GetComponentInParent<IShopCustomer>();
        if (collision.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }

    private void Update()
    {
        if (!shopOpen)
        {
            if (playerIsClose)
            {
                speechGrid.SetActive(true);

                if(Input.GetKeyDown(KeyCode.E))
                {
                    uiShop.Show(shopCustomer);
                    uiInventory.Show(shopCustomer);
                    uiSell.Show(shopCustomer);
                    fadeAnimator.SetBool("PauseEnabled", true);

                    shopOpen = true;
                    shopCustomer.DisableMovement();
                }
            }
            else
            {
                speechGrid.SetActive(false);
            }
        }
        else
        {
            speechGrid.SetActive(false);

            if (Input.GetKeyDown(KeyCode.E))
            {
                uiShop.Hide();
                uiInventory.Hide();
                uiSell.Hide();
                fadeAnimator.SetBool("PauseEnabled", false);

                shopOpen = false;
                shopCustomer.EnableMovement();
            }
        }
    }

    public bool getShopOpen()
    {
        return shopOpen;
    }
}
