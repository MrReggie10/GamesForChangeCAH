using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector2 input;
    private Rigidbody2D rb;

    private InventorySystem inventory;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private TrashSpawner trashSpawner;
    [SerializeField] private float maxWeight;

    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        inventory = new InventorySystem();
        inventory.setMaxWeight(maxWeight);
        uiInventory.SetInventory(inventory);

        

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
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if(itemWorld != null)
        {
            if(inventory.getCurrentWeight() + itemWorld.getItem().getWeight() <= inventory.getMaxWeight())
            {
                inventory.addItem(itemWorld.getItem());
                itemWorld.DestroySelf();
                trashSpawner.changeCurrentBeachTrash();
            }
        }
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
