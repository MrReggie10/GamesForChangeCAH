using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingStorageTriggerCollider : MonoBehaviour
{
    [SerializeField] private UI_RecyclingStorage uiRecycling;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private RecyclingInfo info;
    [SerializeField] private Animator fadeAnimator;

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
            if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
            {
                uiRecycling.Show(shopCustomer);
                uiInventory.Show(shopCustomer);
                info.Show();
                fadeAnimator.SetBool("PauseEnabled", true);

                shopOpen = true;
                shopCustomer.DisableMovement();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                uiRecycling.Hide();
                uiInventory.Hide();
                info.Hide();
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
