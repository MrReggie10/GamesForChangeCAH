using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private GameObject playerName;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private GameObject npcName;
    [SerializeField] private TextMeshProUGUI npcNameText;

    [SerializeField] private GameObject playerImg;
    [SerializeField] private Image playerImage;
    [SerializeField] private GameObject npcImg;
    [SerializeField] private Image npcImage;

    [SerializeField] private List<DialogType> dialogueState0;
    [SerializeField] private List<DialogType> dialogueState1;
    [SerializeField] private List<DialogType> dialogueState2;
    [SerializeField] private List<DialogType> dialogueState3;
    [SerializeField] private List<DialogType> talkedAboutMayorDialog;
    [SerializeField] private List<DialogType> dialogueState4;
    [SerializeField] private List<DialogType> dialogueState5;
    private int index;
    private bool talkedAboutMayor = false;

    [SerializeField] private GameObject speechGrid;
    private bool talking;
    
    [SerializeField] private float wordSpeed;
    [SerializeField] private bool playerIsClose;
    [SerializeField] private bool endLineEarly = false;

    [SerializeField] private bool isScientist;

    private IShopCustomer shopCustomer;

    // Update is called once per frame
    void Update()
    {
        if (playerIsClose)
        {
            if(!talking)
            {
                speechGrid.SetActive(true);
            }
            else
            {
                speechGrid.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                talking = true;
                shopCustomer.DisableMovement();

                if (!dialoguePanel.activeInHierarchy)
                {
                    index = 0;
                    dialoguePanel.SetActive(true);
                    dialogueText.text = "";

                    playerName.SetActive(false);
                    playerImg.SetActive(false);
                    npcName.SetActive(false);
                    npcImg.SetActive(false);

                    npcNameText.SetText(GetDialogList()[index].getCharacterName());
                    playerNameText.SetText(GetDialogList()[index].getCharacterName());
                    npcImage.sprite = GetDialogList()[index].getCharacterSprite();
                    playerImage.sprite = GetDialogList()[index].getCharacterSprite();

                    if (GetDialogList()[index].characterType == DialogType.CharacterType.player)
                    {
                        playerName.SetActive(true);
                        playerImg.SetActive(true);
                    }
                    else if ((GetDialogList()[index].characterType == DialogType.CharacterType.npc))
                    {
                        npcName.SetActive(true);
                        npcImg.SetActive(true);
                    }

                    StartCoroutine(Typing());
                }
            }
        }
        else
        {
            speechGrid.SetActive(false);
        }

        if (dialogueText.text == GetDialogList()[index].getText())
        {
            if (Input.GetMouseButtonDown(0))
            {
                NextLine();
            }
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);

        playerName.SetActive(false);
        playerImg.SetActive(false);
        npcName.SetActive(false);
        npcImg.SetActive(false);

        talking = false;
        shopCustomer.EnableMovement();

        if(StateManager.stateManager.GetState() == 3 && !talkedAboutMayor && !isScientist)
        {
            talkedAboutMayor = true;
            MayorQuest.mayorQuest.AddVote();
            MayorQuest.mayorQuest.UpdateProgress();
        }
    }

    IEnumerator Typing()
    {
        foreach (char letter in GetDialogList()[index].getText().ToCharArray())
        {
            if (!playerIsClose)
            {
                yield break;
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < GetDialogList().Count - 1 && !endLineEarly)
        {
            index++;
            dialogueText.text = "";

            playerName.SetActive(false);
            playerImg.SetActive(false);
            npcName.SetActive(false);
            npcImg.SetActive(false);

            npcNameText.SetText(GetDialogList()[index].getCharacterName());
            playerNameText.SetText(GetDialogList()[index].getCharacterName());
            npcImage.sprite = GetDialogList()[index].getCharacterSprite();
            playerImage.sprite = GetDialogList()[index].getCharacterSprite();

            if (GetDialogList()[index].characterType == DialogType.CharacterType.player)
            {
                playerName.SetActive(true);
                playerImg.SetActive(true);
            }
            else if ((GetDialogList()[index].characterType == DialogType.CharacterType.npc))
            {
                npcName.SetActive(true);
                npcImg.SetActive(true);
            }

            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private List<DialogType> GetDialogList()
    {
        switch(StateManager.stateManager.GetState())
        {
            case 0: return dialogueState0;
            case 1: return dialogueState1;
            case 2: return dialogueState2;
            case 3:
                if(talkedAboutMayor)
                {
                    return talkedAboutMayorDialog;
                }
                else
                {
                    return dialogueState3;
                }
            case 4: return dialogueState4;
            default: return dialogueState5;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopCustomer = other.GetComponentInParent<IShopCustomer>();
            playerIsClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(Typing());
            playerIsClose = false;
        }
    }
}
