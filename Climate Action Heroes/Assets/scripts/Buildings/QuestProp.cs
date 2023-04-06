using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestProp : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> onSprites;
    [SerializeField] private List<SpriteRenderer> offSprites;

    private bool playerIsClose;
    private bool questActive = false;

    private void Awake()
    {
        foreach(SpriteRenderer sprite in onSprites)
        {
            sprite.enabled = true;
        }
        foreach(SpriteRenderer sprie in offSprites)
        {
            sprie.enabled = false;
        }
    }

    private void Update()
    {
        if(playerIsClose && questActive && Input.GetKeyDown(KeyCode.E))
        {
            foreach (SpriteRenderer sprite in onSprites)
            {
                sprite.enabled = false;
            }
            foreach (SpriteRenderer sprie in offSprites)
            {
                sprie.enabled = true;
            }
        }
    }

    public void SetPropActive()
    {
        questActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}
