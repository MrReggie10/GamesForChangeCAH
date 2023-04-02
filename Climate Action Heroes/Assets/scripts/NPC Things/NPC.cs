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
                    dialogueText.text = "";
                    dialoguePanel.SetActive(true);

                    if (StateManager.stateManager.GetState() == 0)
                    {
                        npcNameText.SetText(dialogueState0[index].getCharacterName());
                        playerNameText.SetText(dialogueState0[index].getCharacterName());
                        npcImage.sprite = dialogueState0[index].getCharacterSprite();
                        playerImage.sprite = dialogueState0[index].getCharacterSprite();

                        if (dialogueState0[index].characterType == DialogType.CharacterType.player)
                        {
                            playerName.SetActive(true);
                            playerImg.SetActive(true);
                        }
                        else if (dialogueState0[index].characterType == DialogType.CharacterType.npc)
                        {
                            npcName.SetActive(true);
                            npcImg.SetActive(true);
                        }
                    }
                    else if (StateManager.stateManager.GetState() == 1)
                    {
                        npcNameText.SetText(dialogueState1[index].getCharacterName());
                        playerNameText.SetText(dialogueState1[index].getCharacterName());
                        npcImage.sprite = dialogueState1[index].getCharacterSprite();
                        playerImage.sprite = dialogueState1[index].getCharacterSprite();

                        if (dialogueState1[index].characterType == DialogType.CharacterType.player)
                        {
                            playerName.SetActive(true);
                            playerImg.SetActive(true);
                        }
                        else if (dialogueState1[index].characterType == DialogType.CharacterType.npc)
                        {
                            npcName.SetActive(true);
                            npcImg.SetActive(true);
                        }
                    }
                    else if (StateManager.stateManager.GetState() == 2)
                    {
                        npcNameText.SetText(dialogueState2[index].getCharacterName());
                        playerNameText.SetText(dialogueState2[index].getCharacterName());
                        npcImage.sprite = dialogueState2[index].getCharacterSprite();
                        playerImage.sprite = dialogueState2[index].getCharacterSprite();

                        if (dialogueState2[index].characterType == DialogType.CharacterType.player)
                        {
                            playerName.SetActive(true);
                            playerImg.SetActive(true);
                        }
                        else if (dialogueState2[index].characterType == DialogType.CharacterType.npc)
                        {
                            npcName.SetActive(true);
                            npcImg.SetActive(true);
                        }
                    }
                    else if (StateManager.stateManager.GetState() == 3 && talkedAboutMayor)
                    {
                        npcNameText.SetText(talkedAboutMayorDialog[index].getCharacterName());
                        playerNameText.SetText(talkedAboutMayorDialog[index].getCharacterName());
                        npcImage.sprite = talkedAboutMayorDialog[index].getCharacterSprite();
                        playerImage.sprite = talkedAboutMayorDialog[index].getCharacterSprite();

                        if (talkedAboutMayorDialog[index].characterType == DialogType.CharacterType.player)
                        {
                            playerName.SetActive(true);
                            playerImg.SetActive(true);
                        }
                        else if (talkedAboutMayorDialog[index].characterType == DialogType.CharacterType.npc)
                        {
                            npcName.SetActive(true);
                            npcImg.SetActive(true);
                        }
                    }
                    else if (StateManager.stateManager.GetState() == 3)
                    {
                        npcNameText.SetText(dialogueState3[index].getCharacterName());
                        playerNameText.SetText(dialogueState3[index].getCharacterName());
                        npcImage.sprite = dialogueState3[index].getCharacterSprite();
                        playerImage.sprite = dialogueState3[index].getCharacterSprite();

                        if (dialogueState3[index].characterType == DialogType.CharacterType.player)
                        {
                            playerName.SetActive(true);
                            playerImg.SetActive(true);
                        }
                        else if (dialogueState3[index].characterType == DialogType.CharacterType.npc)
                        {
                            npcName.SetActive(true);
                            npcImg.SetActive(true);
                        }
                    }
                    else if (StateManager.stateManager.GetState() == 4)
                    {
                        npcNameText.SetText(dialogueState4[index].getCharacterName());
                        playerNameText.SetText(dialogueState4[index].getCharacterName());
                        npcImage.sprite = dialogueState4[index].getCharacterSprite();
                        playerImage.sprite = dialogueState4[index].getCharacterSprite();

                        if (dialogueState4[index].characterType == DialogType.CharacterType.player)
                        {
                            playerName.SetActive(true);
                            playerImg.SetActive(true);
                        }
                        else if (dialogueState4[index].characterType == DialogType.CharacterType.npc)
                        {
                            npcName.SetActive(true);
                            npcImg.SetActive(true);
                        }
                    }
                    else if (StateManager.stateManager.GetState() == 5)
                    {
                        npcNameText.SetText(dialogueState5[index].getCharacterName());
                        playerNameText.SetText(dialogueState5[index].getCharacterName());
                        npcImage.sprite = dialogueState5[index].getCharacterSprite();
                        playerImage.sprite = dialogueState5[index].getCharacterSprite();

                        if (dialogueState5[index].characterType == DialogType.CharacterType.player)
                        {
                            playerName.SetActive(true);
                            playerImg.SetActive(true);
                        }
                        else if (dialogueState5[index].characterType == DialogType.CharacterType.npc)
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


        if (StateManager.stateManager.GetState() == 0)
        {
            if (dialogueText.text == dialogueState0[index].getText())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    NextLine();
                }
            }
        }
        else if (StateManager.stateManager.GetState() == 1)
        {
            if (dialogueText.text == dialogueState1[index].getText())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    NextLine();
                }
            }
        }
        else if (StateManager.stateManager.GetState() == 2)
        {
            if (dialogueText.text == dialogueState2[index].getText())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    NextLine();
                }
            }
        }
        else if (StateManager.stateManager.GetState() == 3 && talkedAboutMayor)
        {
            if (dialogueText.text == talkedAboutMayorDialog[index].getText())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    NextLine();
                }
            }
        }
        else if (StateManager.stateManager.GetState() == 3)
        {
            if (dialogueText.text == dialogueState3[index].getText())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    NextLine();
                }
            }
        }
        else if (StateManager.stateManager.GetState() == 4)
        {
            if (dialogueText.text == dialogueState4[index].getText())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    NextLine();
                }
            }
        }
        else if (StateManager.stateManager.GetState() == 5)
        {
            if (dialogueText.text == dialogueState5[index].getText())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    NextLine();
                }
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

        if(StateManager.stateManager.GetState() == 3)
        {
            talkedAboutMayor = true;
            MayorQuest.mayorQuest.AddVote();
            MayorQuest.mayorQuest.UpdateProgress();
        }
    }

    IEnumerator Typing()
    {
        if (StateManager.stateManager.GetState() == 0)
        {
            foreach (char letter in dialogueState0[index].getText().ToCharArray())
            {
                if (!playerIsClose)
                {
                    yield break;
                }
                dialogueText.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
        }
        else if (StateManager.stateManager.GetState() == 1)
        {
            foreach (char letter in dialogueState1[index].getText().ToCharArray())
            {
                if (!playerIsClose)
                {
                    yield break;
                }
                dialogueText.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
        }
        else if (StateManager.stateManager.GetState() == 2)
        {
            foreach (char letter in dialogueState2[index].getText().ToCharArray())
            {
                if (!playerIsClose)
                {
                    yield break;
                }
                dialogueText.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
        }
        else if (StateManager.stateManager.GetState() == 3 && talkedAboutMayor)
        {
            foreach (char letter in talkedAboutMayorDialog[index].getText().ToCharArray())
            {
                if (!playerIsClose)
                {
                    yield break;
                }
                dialogueText.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
        }
        else if (StateManager.stateManager.GetState() == 3)
        {
            foreach (char letter in dialogueState3[index].getText().ToCharArray())
            {
                if (!playerIsClose)
                {
                    yield break;
                }
                dialogueText.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
        }
        else if (StateManager.stateManager.GetState() == 4)
        {
            foreach (char letter in dialogueState4[index].getText().ToCharArray())
            {
                if (!playerIsClose)
                {
                    yield break;
                }
                dialogueText.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
        }
        else if (StateManager.stateManager.GetState() == 5)
        {
            foreach (char letter in dialogueState5[index].getText().ToCharArray())
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
        if (StateManager.stateManager.GetState() == 0)
        {
            if (index < dialogueState0.Count - 1 && !endLineEarly)
            {
                index++;
                dialogueText.text = "";

                playerName.SetActive(false);
                playerImg.SetActive(false);
                npcName.SetActive(false);
                npcImg.SetActive(false);

                npcNameText.SetText(dialogueState0[index].getCharacterName());
                playerNameText.SetText(dialogueState0[index].getCharacterName());
                npcImage.sprite = dialogueState0[index].getCharacterSprite();
                playerImage.sprite = dialogueState0[index].getCharacterSprite();

                if (dialogueState0[index].characterType == DialogType.CharacterType.player)
                {
                    playerName.SetActive(true);
                    playerImg.SetActive(true);
                }
                else if ((dialogueState0[index].characterType == DialogType.CharacterType.npc))
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
        else if (StateManager.stateManager.GetState() == 1)
        {
            if (index < dialogueState1.Count - 1 && !endLineEarly)
            {
                index++;
                dialogueText.text = "";

                playerName.SetActive(false);
                playerImg.SetActive(false);
                npcName.SetActive(false);
                npcImg.SetActive(false);

                npcNameText.SetText(dialogueState1[index].getCharacterName());
                playerNameText.SetText(dialogueState1[index].getCharacterName());
                npcImage.sprite = dialogueState1[index].getCharacterSprite();
                playerImage.sprite = dialogueState1[index].getCharacterSprite();

                if (dialogueState1[index].characterType == DialogType.CharacterType.player)
                {
                    playerName.SetActive(true);
                    playerImg.SetActive(true);
                }
                else if ((dialogueState1[index].characterType == DialogType.CharacterType.npc))
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
        else if (StateManager.stateManager.GetState() == 2)
        {
            if (index < dialogueState2.Count - 1 && !endLineEarly)
            {
                index++;
                dialogueText.text = "";

                playerName.SetActive(false);
                playerImg.SetActive(false);
                npcName.SetActive(false);
                npcImg.SetActive(false);

                npcNameText.SetText(dialogueState2[index].getCharacterName());
                playerNameText.SetText(dialogueState2[index].getCharacterName());
                npcImage.sprite = dialogueState2[index].getCharacterSprite();
                playerImage.sprite = dialogueState2[index].getCharacterSprite();

                if (dialogueState2[index].characterType == DialogType.CharacterType.player)
                {
                    playerName.SetActive(true);
                    playerImg.SetActive(true);
                }
                else if ((dialogueState2[index].characterType == DialogType.CharacterType.npc))
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
        else if (StateManager.stateManager.GetState() == 3 && talkedAboutMayor)
        {
            if (index < talkedAboutMayorDialog.Count - 1 && !endLineEarly)
            {
                index++;
                dialogueText.text = "";

                playerName.SetActive(false);
                playerImg.SetActive(false);
                npcName.SetActive(false);
                npcImg.SetActive(false);

                npcNameText.SetText(talkedAboutMayorDialog[index].getCharacterName());
                playerNameText.SetText(talkedAboutMayorDialog[index].getCharacterName());
                npcImage.sprite = talkedAboutMayorDialog[index].getCharacterSprite();
                playerImage.sprite = talkedAboutMayorDialog[index].getCharacterSprite();

                if (talkedAboutMayorDialog[index].characterType == DialogType.CharacterType.player)
                {
                    playerName.SetActive(true);
                    playerImg.SetActive(true);
                }
                else if ((talkedAboutMayorDialog[index].characterType == DialogType.CharacterType.npc))
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
        else if (StateManager.stateManager.GetState() == 3)
        {
            if (index < dialogueState3.Count - 1 && !endLineEarly)
            {
                index++;
                dialogueText.text = "";

                playerName.SetActive(false);
                playerImg.SetActive(false);
                npcName.SetActive(false);
                npcImg.SetActive(false);

                npcNameText.SetText(dialogueState3[index].getCharacterName());
                playerNameText.SetText(dialogueState3[index].getCharacterName());
                npcImage.sprite = dialogueState3[index].getCharacterSprite();
                playerImage.sprite = dialogueState3[index].getCharacterSprite();

                if (dialogueState3[index].characterType == DialogType.CharacterType.player)
                {
                    playerName.SetActive(true);
                    playerImg.SetActive(true);
                }
                else if ((dialogueState3[index].characterType == DialogType.CharacterType.npc))
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
        else if (StateManager.stateManager.GetState() == 4)
        {
            if (index < dialogueState4.Count - 1 && !endLineEarly)
            {
                index++;
                dialogueText.text = "";

                playerName.SetActive(false);
                playerImg.SetActive(false);
                npcName.SetActive(false);
                npcImg.SetActive(false);

                npcNameText.SetText(dialogueState4[index].getCharacterName());
                playerNameText.SetText(dialogueState4[index].getCharacterName());
                npcImage.sprite = dialogueState4[index].getCharacterSprite();
                playerImage.sprite = dialogueState4[index].getCharacterSprite();

                if (dialogueState4[index].characterType == DialogType.CharacterType.player)
                {
                    playerName.SetActive(true);
                    playerImg.SetActive(true);
                }
                else if ((dialogueState4[index].characterType == DialogType.CharacterType.npc))
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
        else if (StateManager.stateManager.GetState() == 5)
        {
            if (index < dialogueState5.Count - 1 && !endLineEarly)
            {
                index++;
                dialogueText.text = "";

                playerName.SetActive(false);
                playerImg.SetActive(false);
                npcName.SetActive(false);
                npcImg.SetActive(false);

                npcNameText.SetText(dialogueState5[index].getCharacterName());
                playerNameText.SetText(dialogueState5[index].getCharacterName());
                npcImage.sprite = dialogueState5[index].getCharacterSprite();
                playerImage.sprite = dialogueState5[index].getCharacterSprite();

                if (dialogueState5[index].characterType == DialogType.CharacterType.player)
                {
                    playerName.SetActive(true);
                    playerImg.SetActive(true);
                }
                else if((dialogueState5[index].characterType == DialogType.CharacterType.npc))
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
            zeroText();
        }
    }
}
