using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTrigger : MonoBehaviour
{
    [SerializeField] private BuildingStates.States buildingType;
    private Item heldBuilding;

    private void OnTriggerStay2D(Collider2D collision)
    {
        IShopCustomer shopCustomer = collision.GetComponentInParent<IShopCustomer>();
        if (shopCustomer != null)
        {
            if(heldBuilding != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("key pressed");
                    switch (buildingType)
                    {
                        default:
                        case BuildingStates.States.windmill_plot:
                            buildingType = BuildingStates.States.windmill_lvl1;
                            break;
                        case BuildingStates.States.windmill_lvl1:
                            buildingType = BuildingStates.States.windmill_lvl2;
                            break;
                        case BuildingStates.States.windmill_lvl2:
                            buildingType = BuildingStates.States.windmill_lvl3;
                            break;
                    }
                    for(int i = 0; i < shopCustomer.GetInventorySystem().getItemList().Count; i++)
                    {
                        if(shopCustomer.GetInventorySystem().getItemList()[i] == heldBuilding)
                        {
                            shopCustomer.RemoveItem(i);
                            break;
                        }
                    }
                    heldBuilding = null;
                }
            }
            else
            {
                foreach (Item item in shopCustomer.GetInventorySystem().getItemList())
                {
                    if (item.getName().Equals(BuildingStates.GetNextUpgradeName(buildingType)))
                    {
                        heldBuilding = item;
                        break;
                    }
                }
            }
        }
    }
}
