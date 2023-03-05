using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IShopCustomer
{
    [SerializeField] private int cash;

    [SerializeField] private float moveSpeed;
    private float bikeSpeed = 5;
    private Vector2 input;
    private Rigidbody2D rb;
    private bool canMove = true;

    private Bikes.BikeType currentBike;

    private InventorySystem inventory;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private TrashSpawner trashSpawner;
    [SerializeField] private UI_CashAmount uiCashAmount;
    [SerializeField] private UI_WeightCounter uiWeightCounter;
    [SerializeField] private float maxWeight;

    public event EventHandler OnCashAmountChanged;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        inventory = new InventorySystem();
        inventory.setMaxWeight(maxWeight);
        Invoke("delayedAwake", 0.01f);

        uiCashAmount.setCashText(cash);
    }

    void delayedAwake()
    {
        uiInventory.SetInventory(inventory);
    }

    void Update()
    {
        if(canMove)
        {
            ProcessInputs();
        }
    }

    void FixedUpdate()
    {
        if(canMove)
        {
            Move();
        }
    }


    //pickup
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            if (inventory.getCurrentWeight() + itemWorld.getItem().getWeight() <= inventory.getMaxWeight() + 0.01)
            {
                inventory.addItem(itemWorld.getItem());
                itemWorld.DestroySelf();
                trashSpawner.changeCurrentBeachTrash();
            }
        }
    }


    //movement
    private void ProcessInputs()
    {
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveY = Input.GetAxisRaw("Vertical");

        input = new Vector2(MoveX, MoveY);
        input.Normalize();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(moveSpeed == 5)
            {
                moveSpeed = bikeSpeed;
            }
            else
            {
                moveSpeed = 5;
            }
        }
    }

    private void Move()
    {
        rb.velocity = input * moveSpeed;
    }

    private void UpdateSprite()
    {

    }

    //buying items
    public void BoughtItem(Item.ItemType itemType)
    {
        Item item = new Item();
        item.itemType = itemType;
        item.amount = 1;

        inventory.addItem(item);
        uiCashAmount.setCashText(cash);
    }

    bool IShopCustomer.TrySpendCashAmount(int spendCashAmount)
    {
        if(cash >= spendCashAmount)
        {
            cash -= spendCashAmount;
            OnCashAmountChanged?.Invoke(this, EventArgs.Empty);
            uiCashAmount.setCashText(cash);
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

    InventorySystem IShopCustomer.GetInventorySystem()
    {
        return inventory;
    }

    void IShopCustomer.Remove1ItemPlayer(int index)
    {
        cash += inventory.getItemList()[index].getSell();
        uiCashAmount.setCashText(cash);

        inventory.RemoveOneItem(index);
    }

    void IShopCustomer.RemoveAllItemPlayer(int index)
    {
        cash += inventory.getItemList()[index].getSell() * inventory.getItemList()[index].amount;
        uiCashAmount.setCashText(cash);

        inventory.RemoveAll(index);
    }

    int IShopCustomer.GetCash()
    {
        return cash;
    }

    bool IShopCustomer.TryUseItems(Item.ItemType itemType)
    {
        List<Item> recipe = Item.getRecipe(itemType);
        List<Item> inv = inventory.getItemList();
        bool itemFound = false;

        //search the array
        foreach(Item item1 in recipe)
        {
            foreach(Item item2 in inv)
            {
                if(item1.itemType == item2.itemType)
                {
                    if(item2.amount >= item1.amount)
                    {
                        itemFound = true;
                        break;
                    }
                }
            }
            if(!itemFound)
            {
                return false;
            }
            itemFound = false;
        }

        //take away the items
        foreach (Item item1 in recipe)
        {
            for(int j = 0; j < inv.Count; j++)
            {
                if (item1.itemType == inv[j].itemType)
                {
                    for(int i = 0; i < item1.amount; i++)
                    {
                        inventory.RemoveOneItem(j);
                    }
                }
            }
        }

        return true;
    }

    void IShopCustomer.CraftItem(Item.ItemType itemType)
    {
        Item item = new Item();
        item.itemType = itemType;
        item.amount = 1;

        inventory.addItem(item);
    }

    void IShopCustomer.EquipBike(Bikes.BikeType bikeType)
    {
        bikeSpeed = Bikes.GetMoveSpeed(bikeType);
        maxWeight = Bikes.GetStorage(bikeType);

        inventory.setMaxWeight(maxWeight);

        uiCashAmount.setCashText(cash);
        uiWeightCounter.Refresh(inventory);

        UpdateSprite();
    }

    void IShopCustomer.RemoveItem(int itemIndex)
    {
        inventory.RemoveOneItem(itemIndex);
    }

    void IShopCustomer.Add1ItemPlayer(Item item)
    {
        inventory.addItem(item);
    }

    void IShopCustomer.AddAllItemPlayer(Item item)
    {
        Item tempItem = new Item(item.itemType, 1);

        for(int i = 0; i < item.amount; i++)
        {
            inventory.addItem(tempItem);
        }
    }

    void IShopCustomer.DisableMovement()
    {
        canMove = false;
    }

    void IShopCustomer.EnableMovement()
    {
        canMove = true;
    }
}
