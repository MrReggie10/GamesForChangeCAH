using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    private bool walkingToBeach;
    private bool walkingToShop;
    private bool walkingToPark;
    private bool walkingToGeneral;

    [SerializeField] private GameObject initialNPC;
    [SerializeField] private GameObject beachNPC;
    [SerializeField] private GameObject shopNPC;
    [SerializeField] private GameObject parkNPC;
    [SerializeField] private GameObject generalNPC;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject player;
    [SerializeField] private float yPos;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(player.transform.position.y > yPos + transform.position.y)
        {
            spriteRenderer.sortingOrder = 6;
        }
        else
        {
            spriteRenderer.sortingOrder = 4;
        }

        animator.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("SpeedY", rb.velocity.y);

        if(rb.velocity.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (rb.velocity.x > 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        if (walkingToBeach)
        {
            if(gameObject.transform.position.x > -21)
            {
                rb.velocity = new Vector2(-5, 0);
            }
            else if(gameObject.transform.position.x > -30 && gameObject.transform.position.y > 1.75)
            {
                Vector2 tempVector = new Vector2(-5, -5);
                tempVector.Normalize();
                tempVector *= 5;
                rb.velocity = tempVector;
            }
            else if(gameObject.transform.position.x > -30 && gameObject.transform.position.y <= 1.75)
            {
                rb.velocity = new Vector2(-5, 0);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
                gameObject.transform.position = new Vector3(-30, 1.75f, 0);
                beachNPC.SetActive(true);
                walkingToBeach = false;
            }
        }

        if (walkingToShop)
        {
            if (gameObject.transform.position.x < -15.5 && gameObject.transform.position.y < 5)
            {
                rb.velocity = new Vector2(5, 0);
            }
            else if (gameObject.transform.position.x > -15.5 && gameObject.transform.position.y < 8.75)
            {
                rb.velocity = new Vector2(0, 5);
            }
            else if (gameObject.transform.position.x > -17 && gameObject.transform.position.y >= 8.75)
            {
                rb.velocity = new Vector2(-5, 0);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
                gameObject.transform.position = new Vector3(-17, 8.75f, 0);
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                shopNPC.SetActive(true);
                walkingToShop = false;
            }
        }

        if (walkingToPark)
        {
            if (gameObject.transform.position.x < -15.5 && gameObject.transform.position.y > 5)
            {
                rb.velocity = new Vector2(5, 0);
            }
            else if (gameObject.transform.position.x < 8 && gameObject.transform.position.x > -15.5 && gameObject.transform.position.y > 4.75)
            {
                rb.velocity = new Vector2(0, -5);
            }
            else if (gameObject.transform.position.x < 8 && gameObject.transform.position.y <= 4.75)
            {
                rb.velocity = new Vector2(5, 0);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
                gameObject.transform.position = new Vector3(8, 4.75f, 0);
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                parkNPC.SetActive(true);
                walkingToPark = false;
            }
        }

        if (walkingToGeneral)
        {
            if (gameObject.transform.position.x > -17)
            {
                rb.velocity = new Vector2(-5, 0);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
                gameObject.transform.position = new Vector3(-17, 4.75f, 0);
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                generalNPC.SetActive(true);
                walkingToGeneral = false;
            }
        }
    }

    public void WalkToBeach()
    {
        Destroy(initialNPC);
        walkingToBeach = true;
    }

    public void WalkToShop()
    {
        Destroy(beachNPC);
        walkingToShop = true;
    }

    public void WalkToPark()
    {
        Destroy(shopNPC);
        walkingToPark = true;
    }

    public void WalkToGeneral()
    {
        Destroy(parkNPC);
        walkingToGeneral = true;
    }
}
