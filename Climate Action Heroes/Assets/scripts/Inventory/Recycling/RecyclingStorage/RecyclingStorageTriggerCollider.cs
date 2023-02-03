using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingStorageTriggerCollider : MonoBehaviour
{
    [SerializeField] private UI_RecyclingStorage uiRecycling;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private RecyclingInfo info;
    private bool shopOpen = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        IShopCustomer shopCustomer = collision.GetComponentInParent<IShopCustomer>();
        if (shopCustomer != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                uiRecycling.Show(shopCustomer);
                uiInventory.Show(shopCustomer);
                info.Show();
                shopOpen = true;
            }
        }
    }

    private void Update()
    {
        if (!shopOpen) { return; }

        if (Input.GetKeyDown("escape"))
        {
            uiRecycling.Hide();
            uiInventory.Hide();
            info.Hide();
        }
    }

    public bool getShopOpen()
    {
        return shopOpen;
    }
}
