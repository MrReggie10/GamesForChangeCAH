using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector2 input;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

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
