using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer
{
    void BoughtItem(Item.ItemType itemType);
    bool TrySpendCashAmount(int cashAmount);
    bool TryFitWeight(float weight);
    InventorySystem GetInventorySystem();
    void Remove1ItemPlayer(int index);
    void RemoveAllItemPlayer(int index);
    int GetCash();
    bool TryUseItems(Item.ItemType itemType);
    void CraftItem(Item.ItemType itemType);
    void EquipBike(Bikes.BikeType bikeType);
    void RemoveItem(int itemIndex);

    void Add1ItemPlayer(Item item);
    void AddAllItemPlayer(Item item);
    void DisableMovement();
    void EnableMovement();
}
