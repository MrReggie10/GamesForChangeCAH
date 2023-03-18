using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTriggerCollider : MonoBehaviour
{
    [SerializeField] private UI_Crafting uiCraft;
    [SerializeField] private UI_Inventory uiInventory;
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
                uiCraft.Show(shopCustomer);
                uiInventory.Show(shopCustomer);
                fadeAnimator.SetBool("PauseEnabled", true);

                shopOpen = true;
                shopCustomer.DisableMovement();
            }
        }
        else
        {
            if (Input.GetKeyDown("escape"))
            {
                uiCraft.Hide();
                uiInventory.Hide();
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
