using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    private bool playerIsClose;
    public bool on = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IShopCustomer shopCustomer = collision.GetComponentInParent<IShopCustomer>();
        if (shopCustomer != null)
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerIsClose = false;
    }

    public bool GetContact()
    {
        return playerIsClose;
    }

    public void TurnOff()
    {
        on = false;
    }
}
