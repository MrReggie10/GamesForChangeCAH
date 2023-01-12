using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTriggerCollider : MonoBehaviour
{
    [SerializeField] private UIShop uiShop;
    private bool shopOpen = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        IShopCustomer shopCustomer = collision.GetComponentInParent<IShopCustomer>();
        if(shopCustomer != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("trigger working");

                uiShop.Show(shopCustomer);
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
        }
    }

    public bool getShopOpen()
    {
        return shopOpen;
    }
}
