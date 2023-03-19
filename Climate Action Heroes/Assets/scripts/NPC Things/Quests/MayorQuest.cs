using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayorQuest : QuestType
{
    public static MayorQuest mayorQuest;
    
    [SerializeField] private int xp;

    private GameObject npc;
    private GameObject uiPanel;

    private int votes;
    [SerializeField] private int totalVotesNeeded;

    private void Awake()
    {
        mayorQuest = this;
    }

    public override void EndQuest()
    {
        started = false;
        StateManager.stateManager.SetState(4);
        QuestManager.questManager.EndQuest(xp, this.gameObject, uiPanel, npc);
    }

    public override string GetQuestName()
    {
        return "Turn off all the lights";
    }

    public override void QuestStart(GameObject npc, GameObject uiPanel, IShopCustomer shopCustomer)
    {
        this.npc = npc;

        this.uiPanel = uiPanel;
        StateManager.stateManager.SetState(3);

        uiPanel.GetComponent<QuestUIprefab>().SetName("Get 10 people to vote for the public transportation bill");

        started = true;
    }

    public override void UpdateProgress()
    {
        QuestManager.questManager.SetQuestProgress(uiPanel, votes, totalVotesNeeded);
        if (votes == totalVotesNeeded)
        {
            npc.GetComponent<QuestNPC>().SetState(2);
        }
    }

    public void AddVote()
    {
        votes++;
    }
}
