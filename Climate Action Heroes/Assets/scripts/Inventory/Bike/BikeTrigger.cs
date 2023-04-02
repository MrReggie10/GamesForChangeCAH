using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeTrigger : MonoBehaviour
{
    [SerializeField] private UI_BikeShop uiBike;
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

                if (Input.GetKeyDown(KeyCode.E))
                {
                    uiBike.Show(shopCustomer);
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
                uiBike.Hide();
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
