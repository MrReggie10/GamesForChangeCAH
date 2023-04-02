using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager progressionManager { get; private set; }

    public event EventHandler OnPhaseChange;

    [SerializeField] private Slider cleanMeter;
    [SerializeField] private TextMeshProUGUI xpText;

    [SerializeField] private int totalXP;
    [SerializeField] private int neededXP;

    [SerializeField] private GameObject factorySmoke;
    [SerializeField] private GameObject smog2;
    [SerializeField] private GameObject smog3;
    [SerializeField] private Camera playerCam;

    [SerializeField] private Canvas invCanvas;
    [SerializeField] private Canvas questCanvas;
    [SerializeField] private Canvas progCanvas;

    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private UI_PhaseNotif phaseNotif;
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private Transform endMarker;

    [SerializeField] private GameObject player;

    private int phase = 1;
    private bool camMoving = false;

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

    private void Update()
    {
        if(camMoving)
        {
            playerCam.transform.position = Vector3.Lerp(playerCam.transform.position, endMarker.position, Time.deltaTime);
            playerCam.orthographicSize += (endMarker.transform.localScale.x - playerCam.orthographicSize) * 0.90f * Time.deltaTime;
            if (MathF.Abs(playerCam.transform.position.x - endMarker.position.x) < 0.2)
            {
                camMoving = false;
            }
        }
    }

    public void ChangePhase()
    {
        phase++;
        if(phase == 2)
        {
            StartCoroutine("goToPhase2");
            neededXP = 125;
            StateManager.stateManager.SetState(1);
        }
        else if(phase == 3)
        {
            StartCoroutine("goToPhase3");
            neededXP = 450;
            StateManager.stateManager.SetState(2);
        }
        else
        {
            StateManager.stateManager.SetState(5);
        }

        xpText.SetText(totalXP + "/" + neededXP);
        OnPhaseChange?.Invoke(this, EventArgs.Empty);
    }

    IEnumerator goToPhase2()
    {
        invCanvas.enabled = false;
        questCanvas.enabled = false;
        progCanvas.enabled = false;

        player.GetComponent<PlayerMove>().DisableMovement();

        yield return new WaitForSeconds(1);

        endMarker.transform.position = new Vector3(57, 15, -10);
        endMarker.transform.localScale = new Vector3(12, 1, 1);
        camMoving = true;

        while(camMoving)
        {
            yield return new WaitForSeconds(0.1f);
        }

        Color tempColor = factorySmoke.GetComponent<Tilemap>().color;
        for(int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 10; i++)
            {
                tempColor.a = 1 - ((float)i * 0.05f);
                factorySmoke.GetComponent<Tilemap>().color = tempColor;
                yield return new WaitForSeconds(0.03f);
            }
            for (int i = 0; i < 10; i++)
            {
                tempColor.a = 0.5f + ((float)i * 0.05f);
                factorySmoke.GetComponent<Tilemap>().color = tempColor;
                yield return new WaitForSeconds(0.03f);
            }
        }
        

        endMarker.transform.position = new Vector3(0, 15, -10);
        endMarker.transform.localScale = new Vector3(15, 1, 1);
        camMoving = true;

        while (camMoving)
        {
            yield return new WaitForSeconds(0.1f);
        }

        tempColor = smog2.GetComponentInChildren<Tilemap>().color;
        for (int i = 0; i < 20; i++)
        {
            tempColor.a = 1 - ((float)i * 0.05f);
            smog2.GetComponentInChildren<Tilemap>().color = tempColor;
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(smog2);

        yield return new WaitForSeconds(1);

        endMarker.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        endMarker.transform.localScale = new Vector3(7, 1, 1);
        camMoving = true;

        while (camMoving)
        {
            yield return new WaitForSeconds(0.1f);
        }

        playerCam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        playerCam.orthographicSize = 7;

        text.SetText("Phase 2 Unlocked!");
        fadeAnimator.SetBool("PauseEnabled", true);
        phaseNotif.Show();

        yield return new WaitForSeconds(3);

        fadeAnimator.SetBool("PauseEnabled", false);
        phaseNotif.Hide();

        yield return new WaitForSeconds(1);

        player.GetComponent<PlayerMove>().EnableMovement();

        invCanvas.enabled = true;
        questCanvas.enabled = true;
        progCanvas.enabled = true;
    }

    IEnumerator goToPhase3()
    {
        invCanvas.enabled = false;
        questCanvas.enabled = false;
        progCanvas.enabled = false;

        player.GetComponent<PlayerMove>().DisableMovement();

        yield return new WaitForSeconds(1);

        endMarker.transform.position = new Vector3(57, 15, -10);
        endMarker.transform.localScale = new Vector3(12, 1, 1);
        camMoving = true;

        while (camMoving)
        {
            yield return new WaitForSeconds(0.1f);
        }

        Color tempColor = factorySmoke.GetComponent<Tilemap>().color;
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 10; i++)
            {
                tempColor.a = 1 - ((float)i * 0.05f);
                factorySmoke.GetComponent<Tilemap>().color = tempColor;
                yield return new WaitForSeconds(0.02f);
            }
            for (int i = 0; i < 10; i++)
            {
                tempColor.a = 0.5f + ((float)i * 0.05f);
                factorySmoke.GetComponent<Tilemap>().color = tempColor;
                yield return new WaitForSeconds(0.02f);
            }
        }
        for (int i = 0; i < 10; i++)
        {
            tempColor.a = 1 - ((float)i * 0.05f);
            factorySmoke.GetComponent<Tilemap>().color = tempColor;
            yield return new WaitForSeconds(0.02f);
        }


        endMarker.transform.position = new Vector3(0, -10, -10);
        endMarker.transform.localScale = new Vector3(15, 1, 1);
        camMoving = true;

        while (camMoving)
        {
            yield return new WaitForSeconds(0.1f);
        }

        tempColor = smog3.GetComponentInChildren<Tilemap>().color;
        for (int i = 0; i < 20; i++)
        {
            tempColor.a = 1 - ((float)i * 0.05f);
            smog3.GetComponentInChildren<Tilemap>().color = tempColor;
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(smog3);

        yield return new WaitForSeconds(1);

        endMarker.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        endMarker.transform.localScale = new Vector3(7, 1, 1);
        camMoving = true;

        while (camMoving)
        {
            yield return new WaitForSeconds(0.1f);
        }

        playerCam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        playerCam.orthographicSize = 7;

        text.SetText("Phase 3 Unlocked!");
        fadeAnimator.SetBool("PauseEnabled", true);
        phaseNotif.Show();

        yield return new WaitForSeconds(3);

        fadeAnimator.SetBool("PauseEnabled", false);
        phaseNotif.Hide();

        yield return new WaitForSeconds(1);

        player.GetComponent<PlayerMove>().EnableMovement();

        invCanvas.enabled = true;
        questCanvas.enabled = true;
        progCanvas.enabled = true;
    }

    public int GetPhase()
    {
        return phase;
    }
    public int GetTotalXP()
    {
        return totalXP;
    }
}
