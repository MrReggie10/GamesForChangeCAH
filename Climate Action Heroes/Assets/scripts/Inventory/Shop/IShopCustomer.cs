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
}
