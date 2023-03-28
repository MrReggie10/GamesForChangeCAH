using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkQuest : QuestType
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
        uiPanel.GetComponent<QuestUIprefab>().SetName("Place a Lvl 1 Windmill on a plot of land");

        this.shopCustomer = shopCustomer;

        started = true;
    }

    void Update()
    {
        if (!started) { return; }

        if(npc.GetComponent<ParkQuestNPC>().GetState() < 3)
        {
            if (ProgressionManager.progressionManager.GetTotalXP() > 3)
            {
                npc.GetComponent<ParkQuestNPC>().SetState(2);
            }
        }
        else if (npc.GetComponent<ParkQuestNPC>().GetState() >= 3)
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

        QuestManager.questManager.EndQuest(xp, this.gameObject, uiPanel, npc);
    }
}
