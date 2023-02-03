using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingShopTriggerCollider : MonoBehaviour
{
    [SerializeField] private UI_RecyclingStorageShop uiStorage;
    [SerializeField] private UI_RecyclingTruckShop uiTruck;
    //[SerializeField] private RecyclingInfo info;
    private bool shopOpen = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        IShopCustomer shopCustomer = collision.GetComponentInParent<IShopCustomer>();
        if (shopCustomer != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                uiStorage.Show(shopCustomer);
                uiTruck.Show(shopCustomer);
                shopOpen = true;
            }
        }
    }

    private void Update()
    {
        if (!shopOpen) { return; }

        if (Input.GetKeyDown("escape"))
        {
            uiStorage.Hide();
            uiTruck.Hide();
        }
    }

    public bool getShopOpen()
    {
        return shopOpen;
    }
}
