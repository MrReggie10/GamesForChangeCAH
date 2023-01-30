using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    [SerializeField] private int moneyToOpen;
    [SerializeField] private GameObject gate;
    private IShopCustomer shopCustomer;

    private void OnTriggerStay2D(Collider2D collision)
    {
        IShopCustomer shopCustomer = collision.GetComponentInParent<IShopCustomer>();
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(shopCustomer.TrySpendCashAmount(moneyToOpen))
            {
                gate.SetActive(false);
            }
        }
    }
}
