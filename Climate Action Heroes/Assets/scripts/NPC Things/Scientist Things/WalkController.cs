using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    private bool walkingToBeach;

    [SerializeField] private GameObject initialNPC;
    [SerializeField] private GameObject beachNPC;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
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
    }

    public void WalkToBeach()
    {
        Destroy(initialNPC);
        walkingToBeach = true;
    }
}
