using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    [SerializeField] private AudioSource buttonPress;
    [SerializeField] private AudioSource trashPickup;
    [SerializeField] private AudioSource buy;
    [SerializeField] private AudioSource error;
    [SerializeField] private AudioSource craft;
    [SerializeField] private AudioSource win;
    [SerializeField] private AudioSource music;

    private void Awake()
    {
        /*
        if(audioManager == null)
        {
            audioManager = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        */

        audioManager = this;

        //DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(string name)
    {
        if(name == "buttonPress")
        {
            buttonPress.Play();
        }
        else if(name == "trashPickup")
        {
            trashPickup.Play();
        }
        else if(name == "buy")
        {
            buy.Play();
        }
        else if (name == "error")
        {
            error.Play();
        }
        else if (name == "craft")
        {
            craft.Play();
        }
        else if (name == "win")
        {
            win.Play();
        }
    }
}
