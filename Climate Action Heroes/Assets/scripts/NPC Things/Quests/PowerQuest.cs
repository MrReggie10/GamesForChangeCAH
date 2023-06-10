using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerQuest : QuestType
{
    [SerializeField] private List<GameObject> breaks;
    [SerializeField] private int xp;

    private GameObject npc;
    private GameObject uiPanel;

    private int lightsOff = 0;

    public override string GetQuestName()
    {
        return "Turn off all the lights";
    }

    public override void QuestStart(GameObject npc, GameObject uiPanel, IShopCustomer shopCustomer)
    {
        this.npc = npc;

        this.uiPanel = uiPanel;
        uiPanel.GetComponent<QuestUIprefab>().SetName("Fix the broken power lines");

        foreach (GameObject light in breaks)
        {
            light.SetActive(true);
        }

        PowerlinePropController.powerlinePropController.SetQuestActive();

        started = true;
    }

    void Update()
    {
        if (!started) { return; }

        CheckCollisions();
        if (npc.GetComponent<QuestNPC>().GetState() >= 3)
        {
            EndQuest();
        }
    }

    private void CheckCollisions()
    {
        foreach (GameObject light in breaks)
        {
            if (light == null) { continue; }
            if (light.GetComponent<Power>().GetContact() && light.GetComponent<Power>().on)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    lightsOff += 1;
                    UpdateProgress();
                    light.GetComponent<Power>().TurnOff();
                }
            }
        }
    }

    public override void UpdateProgress()
    {
        QuestManager.questManager.SetQuestProgress(uiPanel, lightsOff, breaks.Count);
        if (lightsOff == breaks.Count)
        {
            npc.GetComponent<QuestNPC>().SetState(2);
        }
    }

    public override void EndQuest()
    {
        started = false;
        QuestManager.questManager.EndQuest(xp, this.gameObject, uiPanel, npc);
    }
}
