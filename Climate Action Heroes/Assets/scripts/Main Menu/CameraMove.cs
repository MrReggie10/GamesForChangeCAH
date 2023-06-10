using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector2 targetPos;
    private Vector2 vec;
    [SerializeField] private float speed;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        PickLocation();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = vec * speed;

        if(Mathf.Abs(transform.position.x - targetPos.x) < 1 || Mathf.Abs(transform.position.y - targetPos.y) < 1)
        {
            PickLocation();
        }
    }

    private void PickLocation()
    {
        float randX = Random.Range(-34f, 27f);
        float randY = Random.Range(-15f, 20f);

        targetPos = new Vector2(randX, randY);

        vec = new Vector2(targetPos.x - transform.position.x, targetPos.y - transform.position.y);
        vec.Normalize();
    }
}
