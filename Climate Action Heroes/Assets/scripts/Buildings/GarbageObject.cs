using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer three;
    [SerializeField] private SpriteRenderer two;
    [SerializeField] private SpriteRenderer one;

    private int level = 3;

    public void Show()
    {
        level--;
        if (level == 3)
        {
            three.enabled = true;
            two.enabled = false;
            one.enabled = false;
        }
        else if (level == 2)
        {
            three.enabled = false;
            two.enabled = true;
            one.enabled = false;
        }
        else if (level == 1)
        {
            three.enabled = false;
            two.enabled = false;
            one.enabled = true;
        }
        else
        {
            three.enabled = false;
            two.enabled = false;
            one.enabled = false;
        }
    }

    public int GetLevel()
    {
        return level;
    }
}
