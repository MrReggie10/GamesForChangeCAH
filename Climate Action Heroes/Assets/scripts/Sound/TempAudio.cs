using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAudio : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayClick()
    {
        FindObjectOfType<AudioManager>().PlaySound("buttonPress");
    }
}
