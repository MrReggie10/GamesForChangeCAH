using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeQuest : QuestType
{
    [SerializeField] private int xp;

    private GameObject npc;
    private GameObject uiPanel;
    private IShopCustomer shopCustomer;

    public override string GetQuestName()
    {
        return "Turn off all the lights";
    }

    public override void QuestStart(GameObject npc, GameObject uiPanel, IShopCustomer shopCustomer)
    {
        this.npc = npc;

        this.uiPanel = uiPanel;
        uiPanel.GetComponent<QuestUIprefab>().SetName("Give the bike shop owner 2 rubber");

        this.shopCustomer = shopCustomer;

        started = true;
    }

    void Update()
    {
        if (!started) { return; }

        List<Item> tempItemList = shopCustomer.GetInventorySystem().getItemList();

        foreach (Item item in tempItemList)
        {
            if (item.itemType == Item.ItemType.rubber)
            {
                if (npc.GetComponent<QuestNPC>().GetState() < 3)
                {
                    if (item.amount >= 2)
                    {
                        npc.GetComponent<QuestNPC>().SetState(2);
                    }
                    else
                    {
                        npc.GetComponent<QuestNPC>().SetState(1);
                    }
                }
            }
        }

        if (npc.GetComponent<QuestNPC>().GetState() >= 3)
        {
            EndQuest();
        }
    }



    public override void UpdateProgress()
    {
        //QuestManager.questManager.SetQuestProgress(uiPanel, lightsOff, lights.Count);
    }

    public override void EndQuest()
    {
        started = false;

        for (int i = 0; i < shopCustomer.GetInventorySystem().getItemList().Count; i++)
        {
            if (shopCustomer.GetInventorySystem().getItemList()[i].itemType == Item.ItemType.rubber)
            {
                for (int j = 0; j < 2; j++)
                {
                    shopCustomer.GetInventorySystem().RemoveOneItem(i);
                }
            }
        }

        QuestManager.questManager.EndQuest(xp, this.gameObject, uiPanel, npc);
    }
}
