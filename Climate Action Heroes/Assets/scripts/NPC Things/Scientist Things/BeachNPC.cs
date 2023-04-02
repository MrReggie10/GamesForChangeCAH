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

                    if (state == 0)
                    {
                        npcNameText.SetText(preDialogue[index].getCharacterName());
                        playerNameText.SetText(preDialogue[index].getCharacterName());
                        npcImage.sprite = preDialogue[index].getCharacterSprite();
                        playerImage.sprite = preDialogue[index].getCharacterSprite();

                        if (preDialogue[index].characterType == DialogType.CharacterType.player)
                        {
                            playerName.SetActive(true);
                            playerImg.SetActive(true);
                        }
                        else
                        {
                            npcName.SetActive(true);
                            npcImg.SetActive(true);
                        }
                    }
                    else if (state == 1)
                    {
                        npcNameText.SetText(afterPreDialogue[index].getCharacterName());
                        playerNameText.SetText(afterPreDialogue[index].getCharacterName());
                        npcImage.sprite = afterPreDialogue[index].getCharacterSprite();
                        playerImage.sprite = afterPreDialogue[index].getCharacterSprite();

                        if (afterPreDialogue[index].characterType == DialogType.CharacterType.player)
                        {
                            playerName.SetActive(true);
                            playerImg.SetActive(true);
                        }
                        else if (afterPreDialogue[index].characterType == DialogType.CharacterType.npc)
                        {
                            npcName.SetActive(true);
                            npcImg.SetActive(true);
                        }
                    }
                    else if (state == 2)
                    {
                        npcNameText.SetText(postDialogue[index].getCharacterName());
                        playerNameText.SetText(postDialogue[index].getCharacterName());
                        npcImage.sprite = postDialogue[index].getCharacterSprite();
                        playerImage.sprite = postDialogue[index].getCharacterSprite();

                        if (postDialogue[index].characterType == DialogType.CharacterType.player)
                        {
                            playerName.SetActive(true);
                            playerImg.SetActive(true);
                        }
                        else if (postDialogue[index].characterType == DialogType.CharacterType.npc)
                        {
                            npcName.SetActive(true);
                            npcImg.SetActive(true);
                        }
                    }

                    StartCoroutine(Typing());
                }
            }
        }
        else
        {
            speechGrid.SetActive(false);
        }

        if (state == 0)
        {
            if (dialogueText.text == preDialogue[index].getText())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    NextLine();
                }
            }
        }
        else if (state == 1)
        {
            if (dialogueText.text == afterPreDialogue[index].getText())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    NextLine();
                }
            }
        }
        else if (state == 2)
        {
            if (dialogueText.text == postDialogue[index].getText())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    NextLine();
                }
            }
        }
        else
        {
            if (blockingObject != null)
            {
                Destroy(blockingObject);
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
            //delete the ui
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
        if (state == 0)
        {
            foreach (char letter in preDialogue[index].getText().ToCharArray())
            {
                if (!playerIsClose)
                {
                    yield break;
                }
                dialogueText.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
        }
        else if (state == 1)
        {
            foreach (char letter in afterPreDialogue[index].getText().ToCharArray())
            {
                if (!playerIsClose)
                {
                    yield break;
                }
                dialogueText.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
        }
        else if (state == 2)
        {
            foreach (char letter in postDialogue[index].getText().ToCharArray())
            {
                if (!playerIsClose)
                {
                    yield break;
                }
                dialogueText.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
        }

    }
    public void NextLine()
    {
        if (state == 0)
        {
            if(index == 4)
            {
                weightCounter.GetComponentInChildren<Canvas>().enabled = true;
            }
            else if(index == 10)
            {
                questMenu.GetComponentInChildren<Canvas>().enabled = true;
            }
            else if(index == 13)
            {
                progressionMenu.GetComponentInChildren<Canvas>().enabled = true;
            }

            if (index < preDialogue.Count - 1 && !endLineEarly)
            {
                index++;
                dialogueText.text = "";

                playerName.SetActive(false);
                playerImg.SetActive(false);
                npcName.SetActive(false);
                npcImg.SetActive(false);

                npcNameText.SetText(preDialogue[index].getCharacterName());
                playerNameText.SetText(preDialogue[index].getCharacterName());
                npcImage.sprite = preDialogue[index].getCharacterSprite();
                playerImage.sprite = preDialogue[index].getCharacterSprite();

                if (preDialogue[index].characterType == DialogType.CharacterType.player)
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
        else if (state == 1)
        {
            if (index < afterPreDialogue.Count - 1 && !endLineEarly)
            {
                index++;
                dialogueText.text = "";

                playerName.SetActive(false);
                playerImg.SetActive(false);
                npcName.SetActive(false);
                npcImg.SetActive(false);

                npcNameText.SetText(afterPreDialogue[index].getCharacterName());
                playerNameText.SetText(afterPreDialogue[index].getCharacterName());
                npcImage.sprite = afterPreDialogue[index].getCharacterSprite();
                playerImage.sprite = afterPreDialogue[index].getCharacterSprite();

                if (afterPreDialogue[index].characterType == DialogType.CharacterType.player)
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
            else
            {
                zeroText();
            }
        }
        else if (state == 2)
        {
            if (index < postDialogue.Count - 1 && !endLineEarly)
            {
                index++;
                dialogueText.text = "";

                playerName.SetActive(false);
                playerImg.SetActive(false);
                npcName.SetActive(false);
                npcImg.SetActive(false);

                npcNameText.SetText(postDialogue[index].getCharacterName());
                playerNameText.SetText(postDialogue[index].getCharacterName());
                npcImage.sprite = postDialogue[index].getCharacterSprite();
                playerImage.sprite = postDialogue[index].getCharacterSprite();

                if (postDialogue[index].characterType == DialogType.CharacterType.player)
                {
                    playerName.SetActive(true);
                    playerImg.SetActive(true);
                }
                else if (postDialogue[index].characterType == DialogType.CharacterType.npc)
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
