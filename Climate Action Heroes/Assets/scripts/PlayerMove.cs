using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IShopCustomer
{
    [SerializeField] private int cash;

    [SerializeField] private float moveSpeed;
    private Vector2 input;
    private Rigidbody2D rb;

    private InventorySystem inventory;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private TrashSpawner trashSpawner;
    [SerializeField] private float maxWeight;

    public event EventHandler OnCashAmountChanged;

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
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            if (inventory.getCurrentWeight() + itemWorld.getItem().getWeight() <= inventory.getMaxWeight())
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


    //buying items
    public void BoughtItem(Item.ItemType itemType)
    {
        Item item = new Item();
        item.itemType = itemType;
        item.amount = 1;

        inventory.addItem(item);
    }

    bool IShopCustomer.TrySpendCashAmount(int spendCashAmount)
    {
        if(cash >= spendCashAmount)
        {
            cash -= spendCashAmount;
            OnCashAmountChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IShopCustomer.TryFitWeight(float weight)
    {
        if(inventory.getCurrentWeight() + weight <= maxWeight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
