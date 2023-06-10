using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugText : MonoBehaviour
{
    public static DebugText debugText;

    private void Awake()
    {
        debugText = this;
    }

    public void AddText(string text)
    {
        string prevText = gameObject.GetComponent<TextMeshProUGUI>().GetParsedText();

        gameObject.GetComponent<TextMeshProUGUI>().SetText(text + "<br>");
    }
}
