using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeTrigger : MonoBehaviour
{
    [SerializeField] private UI_BikeShop uiBike;
    private bool shopOpen = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        IShopCustomer shopCustomer = collision.GetComponentInParent<IShopCustomer>();
        if (shopCustomer != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                uiBike.Show(shopCustomer);
                shopOpen = true;
            }
        }
    }

    private void Update()
    {
        if (!shopOpen) { return; }

        if (Input.GetKeyDown("escape"))
        {
            uiBike.Hide();
        }
    }

    public bool getShopOpen()
    {
        return shopOpen;
    }
}
