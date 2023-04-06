using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPropController : MonoBehaviour
{
    public static QuestPropController questPropController;

    [SerializeField] private List<QuestProp> props;

    private void Awake()
    {
        questPropController = this;
    }

    public void SetQuestActive()
    {
        foreach(QuestProp prop in props)
        {
            prop.SetPropActive();
        }
    }
}
