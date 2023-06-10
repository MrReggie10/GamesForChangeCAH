using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour
{
    [SerializeField] private List<string> lines;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private SpriteRenderer logo;

    private void Awake()
    {
        StartCoroutine("RunText");
    }

    IEnumerator RunText()
    {
        Color tempColor = text.color;
        foreach (string line in lines)
        {
            text.SetText(line);

            tempColor = text.color;
            for (int i = 0; i < 50; i++)
            {
                tempColor.a = (float)i * 0.02f;
                text.color = tempColor;
                yield return new WaitForSeconds(0.015f);
            }

            yield return new WaitForSeconds(4);

            for (int i = 0; i < 50; i++)
            {
                tempColor.a = 1 - (float)i * 0.02f;
                text.color = tempColor;
                yield return new WaitForSeconds(0.015f);
            }

            yield return new WaitForSeconds(0.5f);
        }

        text.rectTransform.anchoredPosition = new Vector2(0, 200);
        text.SetText("Thanks for Playing!");

        for (int i = 0; i < 50; i++)
        {
            tempColor.a = (float)i * 0.02f;
            text.color = tempColor;
            logo.color = tempColor;
            yield return new WaitForSeconds(0.015f);
        }
    }
}
