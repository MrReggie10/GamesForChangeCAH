using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUIprefab : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI progress;

    public void DisableSlider()
    {
        slider.gameObject.SetActive(false);
    }

    public void EnableSlider()
    {
        slider.gameObject.SetActive(true);
    }

    public void SetSlider(float value)
    {
        Debug.Log(value);
        slider.value = value;
    }

    public void SetName(string name)
    {
        questName.SetText(name);
    }

    public void DisableProgress()
    {
        progress.gameObject.SetActive(false);
    }

    public void EnableProgress()
    {
        progress.gameObject.SetActive(true);
    }

    public void SetProgress(string progressP)
    {
        Debug.Log(progressP);
        progress.SetText(progressP);
    }
}
