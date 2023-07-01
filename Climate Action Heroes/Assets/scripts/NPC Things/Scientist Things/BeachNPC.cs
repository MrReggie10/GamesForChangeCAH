using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BeachNPC : MonoBehaviour
{
    [SerializeField] private GameObject quest;
    [SerializeField] private GameObject blockingObject;

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

    [SerializeField] private List<DialogType> preDialogue;
    [SerializeField] private List<DialogType> afterPreDialogue;
    [SerializeField] private List<DialogType> postDialogue;
    private int state = 0;
    private int index;

    [SerializeField] private GameObject speechGrid;
    private bool talking;

    [SerializeField] private float wordSpeed;
    [SerializeField] private bool playerIsClose;
    [SerializeField] private bool endLineEarly = false;

    [SerializeField] private GameObject weightCounter;
    [SerializeField] private GameObject questMenu;
    [SerializeField] private GameObject progressionMenu;

    private IShopCustomer shopCustomer;
    [SerializeField] private WalkController walkController;

    // Update is called once per frame
    void Update()
    {
        if (playerIsClose)
        {
            if (!talking)
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
                    dialogueText.text = "";
                    dialoguePanel.SetActive(true);

                    npcNameText.SetText(GetDialogList()[index].getCharacterName());
                    playerNameText.SetText(GetDialogList()[index].getCharacterName());
                    npcImage.sprite = GetDialogList()[index].getCharacterSprite();
                    playerImage.sprite = GetDialogList()[index].getCharacterSprite();

                    if (GetDialogList()[index].characterType == DialogType.CharacterType.player)
                    {
                        playerName.SetActive(true);
                        playerImg.SetActive(true);
                    }
                    else
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

        if (state == 0)
        {
            QuestManager.questManager.StartQuest(quest, this.gameObject, shopCustomer);
        }

        if (state == 2)
        {
            FindObjectOfType<AudioManager>().PlaySound("win");
        }

        if (state == 0 || state == 2)
        {
            state++;
        }

        if(state == 3)
        {
            walkController.WalkToShop();
            playerIsClose = false;
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
        if (state == 0)
        {
            if (index == 4)
            {
                weightCounter.GetComponentInChildren<Canvas>().enabled = true;
            }
            else if (index == 10)
            {
                questMenu.GetComponentInChildren<Canvas>().enabled = true;
            }
            else if (index == 15)
            {
                progressionMenu.GetComponentInChildren<Canvas>().enabled = true;
            }
        }

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
            else if (preDialogue[index].characterType == DialogType.CharacterType.npc)
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
        switch (state)
        {
            case 0: return preDialogue;
            case 1: return afterPreDialogue;
            default: return postDialogue;
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

    public void SetState(int state)
    {
        this.state = state;
    }

    public int GetState()
    {
        return state;
    }
}
