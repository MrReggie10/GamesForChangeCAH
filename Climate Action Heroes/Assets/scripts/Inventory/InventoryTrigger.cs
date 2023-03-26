using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTrigger : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private Animator fadeAnimator;

    private bool shopOpen = false;

    public void OpenShop(IShopCustomer shopCustomer)
    {
        if (!shopOpen)
        {
            uiInventory.Show(shopCustomer);
            fadeAnimator.SetBool("PauseEnabled", true);

            shopOpen = true;
            shopCustomer.DisableMovement();
        }
        else
        {
            uiInventory.Hide();
            fadeAnimator.SetBool("PauseEnabled", false);

            shopOpen = false;
            shopCustomer.EnableMovement();
        }
    }

    public bool getShopOpen()
    {
        return shopOpen;
    }
}
