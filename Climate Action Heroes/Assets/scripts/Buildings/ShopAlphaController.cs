using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShopAlphaController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float xLeft;
    [SerializeField] private float xRight;
    [SerializeField] private float yLow;

    [SerializeField] private Tilemap top;
    [SerializeField] private Tilemap low;

    private bool inShop;

    void Update()
    {
        if(player.transform.position.x > xLeft && player.transform.position.x < xRight && player.transform.position.y < yLow + 2)
        {
            if(player.transform.position.y > yLow && player.transform.position.y < yLow+1)
            {
                Color tempColor = top.GetComponent<Tilemap>().color;
                tempColor.a = 1 - (player.transform.position.y - yLow);
                top.GetComponent<Tilemap>().color = tempColor;
                low.GetComponent<Tilemap>().color = tempColor;
            }
            else if (player.transform.position.y > yLow + 1)
            {
                Color tempColor = top.GetComponent<Tilemap>().color;
                tempColor.a = 0;
                top.GetComponent<Tilemap>().color = tempColor;
                low.GetComponent<Tilemap>().color = tempColor;
                inShop = true;
            }
            else
            {
                Color tempColor = top.GetComponent<Tilemap>().color;
                tempColor.a = 1;
                top.GetComponent<Tilemap>().color = tempColor;
                low.GetComponent<Tilemap>().color = tempColor;
                inShop = false;
            }
        }
        else if (player.transform.position.y > yLow + 1 && inShop)
        {
            Color tempColor = top.GetComponent<Tilemap>().color;
            tempColor.a = 0;
            top.GetComponent<Tilemap>().color = tempColor;
            low.GetComponent<Tilemap>().color = tempColor;
        }
        else
        {
            Color tempColor = top.GetComponent<Tilemap>().color;
            tempColor.a = 1;
            top.GetComponent<Tilemap>().color = tempColor;
            low.GetComponent<Tilemap>().color = tempColor;
        }
    }
}
