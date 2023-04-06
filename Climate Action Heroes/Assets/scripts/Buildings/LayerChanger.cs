using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChanger : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> spriteRenderers;
    [SerializeField] private GameObject player;
    private float yPos;

    private void Awake()
    {
        yPos = transform.position.y;
    }

    void Update()
    {
        if (player.transform.position.y > yPos + transform.position.y)
        {
            foreach (SpriteRenderer ren in spriteRenderers)
            {
                ren.sortingOrder = 6;
            }
        }
        else
        {
            foreach (SpriteRenderer ren in spriteRenderers)
            {
                ren.sortingOrder = 4;
            }
        }
    }
}
