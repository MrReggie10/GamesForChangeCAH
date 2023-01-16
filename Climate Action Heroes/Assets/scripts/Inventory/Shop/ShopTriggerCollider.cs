using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTriggerCollider : MonoBehaviour
{
    [SerializeField] private UIShop uiShop;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private UIShopSell uiSell;
    private bool shopOpen = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        IShopCustomer shopCustomer = collision.GetComponentInParent<IShopCustomer>();
        if(shopCustomer != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                uiShop.Show(shopCustomer);
                uiInventory.Show(shopCustomer);
                uiSell.Show(shopCustomer);
                shopOpen = true;
            }
        }
    }

    private void Update()
    {
        if(!shopOpen) { return; }

        if(Input.GetKeyDown("escape"))
        {
            uiShop.Hide();
            uiInventory.Hide();
            uiSell.Hide();
        }
    }

    public bool getShopOpen()
    {
        return shopOpen;
    }
}
