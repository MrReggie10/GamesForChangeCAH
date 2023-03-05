using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager progressionManager { get; private set; }

    public event EventHandler OnPhaseChange;

    [SerializeField] private Slider cleanMeter;
    [SerializeField] private TextMeshProUGUI xpText;

    [SerializeField] private int totalXP;
    [SerializeField] private int neededXP;

    [SerializeField] private GameObject smog2;
    [SerializeField] private GameObject smog3;

    private int phase = 1;

    // Start is called before the first frame update
    void Start()
    {
        progressionManager = this;
    }

    public void AddXP(int xp)
    {
        totalXP += xp;

        cleanMeter.value = (float)totalXP / (float)neededXP;
        xpText.SetText(totalXP + "/" + neededXP);

        if(totalXP >= neededXP)
        {
            ChangePhase();
        }
    }

    public void ChangePhase()
    {
        phase++;
        if(phase == 2)
        {
            Destroy(smog2);
            neededXP = 125;
        }
        else if(phase == 3)
        {
            Destroy(smog3);
            neededXP = 450;
        }

        xpText.SetText(totalXP + "/" + neededXP);
        OnPhaseChange?.Invoke(this, EventArgs.Empty);
    }

    public int GetPhase()
    {
        return phase;
    }
}
