using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsQuest : QuestType
{
    [SerializeField] private List<GameObject> lights;
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
        uiPanel.GetComponent<QuestUIprefab>().SetName("Turn off all the appliances in the man's house");

        foreach (GameObject light in lights)
        {
            light.SetActive(true);
        }

        started = true;
    }

    void Update()
    {
        if(!started) { return; }

        CheckCollisions();
        if(npc.GetComponent<QuestNPC>().GetState() >= 3)
        {
            EndQuest();
        }
    }

    private void CheckCollisions()
    {
        foreach(GameObject light in lights)
        {
            if(light == null) { continue; }
            if(light.GetComponent<Lights>().GetContact() && light.GetComponent<Lights>().on)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    lightsOff += 1;
                    UpdateProgress();
                    light.GetComponent<Lights>().TurnOff();
                }
            }
        }
    }

    public override void UpdateProgress()
    {
        QuestManager.questManager.SetQuestProgress(uiPanel, lightsOff, lights.Count);
        if(lightsOff == lights.Count)
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
