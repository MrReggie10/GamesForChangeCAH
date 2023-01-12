using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer
{
    void BoughtItem(Item.ItemType itemType);
    bool TrySpendCashAmount(int cashAmount);
    bool TryFitWeight(float weight);
}
