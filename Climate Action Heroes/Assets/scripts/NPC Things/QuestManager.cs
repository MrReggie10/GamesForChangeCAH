using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager questManager { get; private set; }

    [SerializeField] private List<GameObject> currentQuests;
    [SerializeField] private List<GameObject> questUIs;

    [SerializeField] private GameObject questUIprefab;
    [SerializeField] private Transform questUIcontainer;
    [SerializeField] private Transform questContainer;

    private void Start()
    {
        questManager = this;
    }

    public void StartQuest(GameObject type, GameObject npc, IShopCustomer shopCustomer)
    {
        GameObject tempQuestInstance = Instantiate(type, questContainer);
        currentQuests.Add(tempQuestInstance);


        GameObject tempUIprefab = questUIprefab;
        tempUIprefab.GetComponent<QuestUIprefab>().DisableSlider();
        tempUIprefab.GetComponent<QuestUIprefab>().DisableProgress();
        GameObject tempUIinstasnce = Instantiate(tempUIprefab, questUIcontainer);
        questUIs.Add(tempUIinstasnce);

        tempQuestInstance.GetComponent<QuestType>().QuestStart(npc, tempUIinstasnce, shopCustomer);
    }

    public void EndQuest(int xp, GameObject type, GameObject currentUI, GameObject npc)
    {
        foreach(GameObject quest in currentQuests)
        {
            if(quest == type)
            {
                Destroy(quest);
            }
        }
        foreach (GameObject questUI in questUIs)
        {
            if (questUI == currentUI)
            {
                Destroy(questUI);
            }
        }

        ProgressionManager.progressionManager.AddXP(xp);
    }

    public void SetQuestProgress(GameObject currentUI, int completed, int total)
    {
        foreach (GameObject questUI in questUIs)
        {
            if (questUI == currentUI)
            {
                questUI.GetComponent<QuestUIprefab>().EnableSlider();
                questUI.GetComponent<QuestUIprefab>().SetSlider((float) completed / (float) total);

                questUI.GetComponent<QuestUIprefab>().EnableProgress();
                questUI.GetComponent<QuestUIprefab>().SetProgress(completed + "/" + total);
            }
        }
    }
}
