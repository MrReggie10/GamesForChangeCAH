using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachQuest : QuestType
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
        uiPanel.GetComponent<QuestUIprefab>().SetName("Bring at least 5 pieces of trash to Professor Samuel");

        this.shopCustomer = shopCustomer;

        started = true;
    }

    void Update()
    {
        if (!started) { return; }

        List<Item> tempItemList = shopCustomer.GetInventorySystem().getItemList();

        int tempTrashTotal = 0;

        foreach (Item item in tempItemList)
        {
            tempTrashTotal += item.amount;
        }

        if (npc.GetComponent<BeachNPC>().GetState() < 3)
        {
            if (tempTrashTotal >= 5)
            {
                npc.GetComponent<BeachNPC>().SetState(2);
            }
            else
            {
                npc.GetComponent<BeachNPC>().SetState(1);
            }
        }
        else
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
            shopCustomer.GetInventorySystem().RemoveAll(i);
        }

        QuestManager.questManager.EndQuest(xp, this.gameObject, uiPanel, npc);
    }
}
