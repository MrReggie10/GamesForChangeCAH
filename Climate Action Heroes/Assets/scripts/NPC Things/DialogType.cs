using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogType
{
    public enum CharacterType
    {
        player,
        npc,
    }

    public CharacterType characterType;
    public string text;
    public string characterName;
    public Sprite characterSprite;

    public DialogType(string text, string characterName, Sprite characterSprite)
    {
        this.text = text;
        this.characterName = characterName;
        this.characterSprite = characterSprite;
    }

    public string getText()
    {
        return text;
    }

    public string getCharacterName()
    {
        return characterName;
    }

    public Sprite getCharacterSprite()
    {
        return characterSprite;
    }
}
