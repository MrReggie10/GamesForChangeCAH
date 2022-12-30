using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    private InventorySystem inventory;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private TrashSpawner trashSpawner;
    [SerializeField] private float maxWeight;

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);

        inventory = new InventorySystem();
        inventory.setMaxWeight(maxWeight);
        uiInventory.SetInventory(inventory);
    }

    public void itemCollision(Collider2D collision)
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
}
