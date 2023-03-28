using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopSNPC : MonoBehaviour
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
    private int index;

    [SerializeField] private float wordSpeed;
    [SerializeField] private bool playerIsClose;
    [SerializeField] private bool endLineEarly = false;

    private IShopCustomer shopCustomer;
    [SerializeField] private WalkController walkController;

    // Update is called once per frame
    void Update()
    {
        if (playerIsClose)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                shopCustomer.DisableMovement();

                if (!dialoguePanel.activeInHierarchy)
                {
                    index = 0;
                    dialogueText.text = "";
                    dialoguePanel.SetActive(true);

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

                    StartCoroutine(Typing());
                }
            }
        }
        if (dialogueText.text == dialogueState0[index].getText())
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

        shopCustomer.EnableMovement();
        walkController.WalkToPark();
        playerIsClose = false;
    }

    IEnumerator Typing()
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
    public void NextLine()
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
            else if (dialogueState0[index].characterType == DialogType.CharacterType.npc)
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
