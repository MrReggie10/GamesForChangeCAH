using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector2 input;
    private Rigidbody2D rb;

    [SerializeField] private MainManager mM;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }


    //pickup
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Touching");
        mM.itemCollision(collision);
    }


    //movement
    void ProcessInputs()
    {
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveY = Input.GetAxisRaw("Vertical");

        input = new Vector2(MoveX, MoveY);
        input.Normalize();
    }
    void Move()
    {
        rb.velocity = input * moveSpeed;
    }
}
