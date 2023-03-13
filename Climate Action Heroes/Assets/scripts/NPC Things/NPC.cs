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

    [SerializeField] private List<DialogType> dialogue;
    private int index;
    
    [SerializeField] private float wordSpeed;
    [SerializeField] private bool playerIsClose;
    [SerializeField] private bool endLineEarly = false;

    private IShopCustomer shopCustomer;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            shopCustomer.DisableMovement();

            if (!dialoguePanel.activeInHierarchy)
            {
                index = 0;
                dialogueText.text = "";
                dialoguePanel.SetActive(true);

                npcNameText.SetText(dialogue[index].getCharacterName());
                playerNameText.SetText(dialogue[index].getCharacterName());
                npcImage.sprite = dialogue[index].getCharacterSprite();
                playerImage.sprite = dialogue[index].getCharacterSprite();

                if (dialogue[index].characterType == DialogType.CharacterType.player)
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
        if(dialogueText.text == dialogue[index].getText())
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
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
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].getText().ToCharArray())
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
        if(index < dialogue.Count - 1 && !endLineEarly)
        {
            index++;
            dialogueText.text = "";

            playerName.SetActive(false);
            playerImg.SetActive(false);
            npcName.SetActive(false);
            npcImg.SetActive(false);

            npcNameText.SetText(dialogue[index].getCharacterName());
            playerNameText.SetText(dialogue[index].getCharacterName());
            npcImage.sprite = dialogue[index].getCharacterSprite();
            playerImage.sprite = dialogue[index].getCharacterSprite();

            if (dialogue[index].characterType == DialogType.CharacterType.player)
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
