using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    [SerializeField] private int moneyToOpen;
    [SerializeField] private GameObject gate;

    private IShopCustomer shopCustomer;
    private bool playerIsClose;

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
        if(playerIsClose)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (shopCustomer.TrySpendCashAmount(moneyToOpen))
                {
                    gate.SetActive(false);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
