using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingTrigger : MonoBehaviour
{
    [SerializeField] private BuildingStates.States buildingType;
    private Item heldBuilding;

    private bool playerIsClose;
    private IShopCustomer shopCustomer;

    [SerializeField] private GameObject lvl1;
    [SerializeField] private GameObject lvl2;
    [SerializeField] private GameObject lvl3;

    [SerializeField] private float yPos;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject speechGrid;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        shopCustomer = collision.GetComponentInParent<IShopCustomer>();
        if (collision.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }

    private void Update()
    {
        if (player.transform.position.y > yPos + transform.position.y)
        {
            lvl1.GetComponent<TilemapRenderer>().sortingOrder = 6;
            lvl2.GetComponent<TilemapRenderer>().sortingOrder = 6;
            lvl3.GetComponent<TilemapRenderer>().sortingOrder = 6;
        }
        else
        {
            lvl1.GetComponent<TilemapRenderer>().sortingOrder = 4;
            lvl2.GetComponent<TilemapRenderer>().sortingOrder = 4;
            lvl3.GetComponent<TilemapRenderer>().sortingOrder = 4;
        }

        if (playerIsClose)
        {
            if(heldBuilding != null)
            {
                speechGrid.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    switch (buildingType)
                    {
                        default:
                        case BuildingStates.States.windmill_plot:
                            buildingType = BuildingStates.States.windmill_lvl1;
                            lvl1.SetActive(true);
                            ProgressionManager.progressionManager.AddXP(2);
                            break;
                        case BuildingStates.States.windmill_lvl1:
                            buildingType = BuildingStates.States.windmill_lvl2;
                            lvl2.SetActive(true);
                            lvl1.SetActive(false);
                            ProgressionManager.progressionManager.AddXP(3);
                            break;
                        case BuildingStates.States.windmill_lvl2:
                            buildingType = BuildingStates.States.windmill_lvl3;
                            lvl3.SetActive(true);
                            lvl2.SetActive(false);
                            ProgressionManager.progressionManager.AddXP(5);
                            break;
                        case BuildingStates.States.windmill_lvl3:
                            break;

                        case BuildingStates.States.hydrogenerator_plot:
                            buildingType = BuildingStates.States.hydrogenerator_lvl1;
                            lvl1.SetActive(true);
                            ProgressionManager.progressionManager.AddXP(4);
                            break;
                        case BuildingStates.States.hydrogenerator_lvl1:
                            buildingType = BuildingStates.States.hydrogenerator_lvl2;
                            lvl2.SetActive(true);
                            lvl1.SetActive(false);
                            ProgressionManager.progressionManager.AddXP(6);
                            break;
                        case BuildingStates.States.hydrogenerator_lvl2:
                            buildingType = BuildingStates.States.hydrogenerator_lvl3;
                            lvl3.SetActive(true);
                            lvl2.SetActive(false);
                            ProgressionManager.progressionManager.AddXP(10);
                            break;
                        case BuildingStates.States.hydrogenerator_lvl3:
                            break;

                        case BuildingStates.States.solar_plot:
                            buildingType = BuildingStates.States.solar_lvl1;
                            lvl1.SetActive(true);
                            ProgressionManager.progressionManager.AddXP(3);
                            break;
                        case BuildingStates.States.solar_lvl1:
                            buildingType = BuildingStates.States.solar_lvl2;
                            lvl2.SetActive(true);
                            lvl1.SetActive(false);
                            ProgressionManager.progressionManager.AddXP(4);
                            break;
                        case BuildingStates.States.solar_lvl2:
                            buildingType = BuildingStates.States.solar_lvl3;
                            lvl3.SetActive(true);
                            lvl2.SetActive(false);
                            ProgressionManager.progressionManager.AddXP(7);
                            break;
                        case BuildingStates.States.solar_lvl3:
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
                speechGrid.SetActive(false);
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
        else
        {
            speechGrid.SetActive(false);
        }
    }
}
