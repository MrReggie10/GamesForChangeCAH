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
    private bool isDriving = true;
    private bool inventoryActive = false;

    private Bikes.BikeType currentBike;

    private InventorySystem inventory;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private TrashSpawner trashSpawner;
    [SerializeField] private UI_CashAmount uiCashAmount;
    [SerializeField] private UI_WeightCounter uiWeightCounter;
    [SerializeField] private float maxWeight;
    [SerializeField] private InventoryTrigger uiTrigger;

    [SerializeField] private Animator playerAnim;
    [SerializeField] private Animator blueBike;
    [SerializeField] private Animator redBike;
    [SerializeField] private Animator electricBike;
    [SerializeField] private Animator rainbowBike;

    [SerializeField] private GameObject taxi;
    [SerializeField] private GameObject visual;
    [SerializeField] private CircleCollider2D scientist;

    public event EventHandler OnCashAmountChanged;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        inventory = new InventorySystem();
        inventory.setMaxWeight(maxWeight);

        Invoke("delayedAwake", 0.01f);
    }

    void delayedAwake()
    {
        uiInventory.SetInventory(inventory);

        uiCashAmount.setCashText(cash);
    }

    void Start()
    {
        DisableMovement();
        rb.velocity = new Vector2(-4, 0);

        StartCoroutine(DriveTaxi());
    }

    IEnumerator DriveTaxi()
    {
        yield return new WaitForSeconds(9);

        rb.velocity = new Vector2(0, 0);
        taxi.GetComponent<SpriteRenderer>().sortingOrder = 6;

        yield return new WaitForSeconds(1);

        visual.GetComponent<SpriteRenderer>().enabled = true;

        yield return new WaitForSeconds(1);

        taxi.transform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(DriveAway());

        yield return new WaitForSeconds(2);

        scientist.enabled = true;

        yield return new WaitForSeconds(3);

        taxi.GetComponent<SpriteRenderer>().enabled = false;

        isDriving = false;
    }

    IEnumerator DriveAway()
    {
        for(int i = 0; i < 600; i++)
        {
            taxi.transform.localPosition = new Vector3(taxi.transform.localPosition.x + 0.06f, taxi.transform.localPosition.y, 0);
            yield return new WaitForSeconds(0.016f);
        }
    }

    void Update()
    {
        ProcessInputs();
        if (canMove)
        {
            UpdateSprite();
        }
        else if(isDriving == false)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    void FixedUpdate()
    {
        if(canMove)
        {
            Move();
        }
        else if (isDriving == false)
        {
            rb.velocity = new Vector2(0, 0);
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
                FindObjectOfType<AudioManager>().PlaySound("trashPickup");

                inventory.addItem(itemWorld.getItem());
                itemWorld.DestroySelf();
                trashSpawner.changeCurrentBeachTrash();
            }
        }
    }


    //movement
    private void ProcessInputs()
    {
        float MoveX;
        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            MoveX = 0;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            MoveX = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MoveX = 1;
        }
        else
        {
            MoveX = 0;
        }

        float MoveY;
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W))
        {
            MoveY = 0;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MoveY = -1;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            MoveY = 1;
        }
        else
        {
            MoveY = 0;
        }

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

        if(Input.GetKeyDown(KeyCode.Q) && (moveSpeed != 0 || inventoryActive))
        {
            if(inventoryActive)
            {
                inventoryActive = false;
            }
            else
            {
                inventoryActive = true;
            }

            uiTrigger.OpenShop(this);
        }
    }

    private void Move()
    {
        rb.velocity = input * moveSpeed;
    }

    private void UpdateSprite()
    {
        if(moveSpeed == 7)
        {
            playerAnim.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            blueBike.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            redBike.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            electricBike.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            rainbowBike.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            blueBike.SetFloat("speedX", Mathf.Abs(rb.velocity.x));
            blueBike.SetFloat("speedY", rb.velocity.y);

            Debug.Log("blue bike");
        }
        else if(moveSpeed == 10)
        {
            playerAnim.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            blueBike.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            redBike.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            electricBike.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            rainbowBike.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            redBike.SetFloat("speedX", Mathf.Abs(rb.velocity.x));
            redBike.SetFloat("speedY", rb.velocity.y);

            Debug.Log("red bike");
        }
        else if(moveSpeed == 13)
        {
            playerAnim.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            blueBike.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            redBike.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            electricBike.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            rainbowBike.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            electricBike.SetFloat("speedX", Mathf.Abs(rb.velocity.x));
            electricBike.SetFloat("speedY", rb.velocity.y);

            Debug.Log("electric bike");
        }
        else if(moveSpeed == 16)
        {
            playerAnim.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            blueBike.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            redBike.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            electricBike.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            rainbowBike.gameObject.GetComponent<SpriteRenderer>().enabled = true;

            rainbowBike.SetFloat("speedX", Mathf.Abs(rb.velocity.x));
            rainbowBike.SetFloat("speedY", rb.velocity.y);

            Debug.Log("rainbow bike");
        }
        else
        {
            playerAnim.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            blueBike.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            redBike.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            electricBike.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            rainbowBike.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            playerAnim.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
            playerAnim.SetFloat("SpeedY", rb.velocity.y);

            Debug.Log("player bike");
        }

        if (rb.velocity.x > 0)
        {
            playerAnim.gameObject.transform.localScale = new Vector3(1, 1, 1);
            blueBike.gameObject.transform.localScale = new Vector3(1, 1, 1);
            redBike.gameObject.transform.localScale = new Vector3(1, 1, 1);
            electricBike.gameObject.transform.localScale = new Vector3(1, 1, 1);
            rainbowBike.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if(rb.velocity.x < 0)
        {
            playerAnim.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            blueBike.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            redBike.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            electricBike.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            rainbowBike.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        
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
        moveSpeed = 0;
    }

    void IShopCustomer.EnableMovement()
    {
        canMove = true;
        moveSpeed = 5;
    }

    public void DisableMovement()
    {
        canMove = false;
        moveSpeed = 0;
    }

    public void EnableMovement()
    {
        canMove = true;
        moveSpeed = 5;
    }
}
