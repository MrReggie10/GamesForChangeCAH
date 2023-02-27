using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestType : MonoBehaviour
{
    [HideInInspector] public bool started;
    public abstract void QuestStart(GameObject npc, GameObject UIinstance);

    public abstract String GetQuestName();

    public abstract void UpdateProgress();

    public abstract void EndQuest();
}
