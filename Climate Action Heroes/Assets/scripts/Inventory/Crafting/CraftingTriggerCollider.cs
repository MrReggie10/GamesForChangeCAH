using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTriggerCollider : MonoBehaviour
{
    [SerializeField] private UI_Crafting uiCraft;
    [SerializeField] private UI_Inventory uiInventory;
    private bool shopOpen = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        IShopCustomer shopCustomer = collision.GetComponentInParent<IShopCustomer>();
        if (shopCustomer != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                uiCraft.Show(shopCustomer);
                uiInventory.Show(shopCustomer);
                shopOpen = true;
            }
        }
    }

    private void Update()
    {
        if (!shopOpen) { return; }

        if (Input.GetKeyDown("escape"))
        {
            uiCraft.Hide();
            uiInventory.Hide();
        }
    }

    public bool getShopOpen()
    {
        return shopOpen;
    }
}
