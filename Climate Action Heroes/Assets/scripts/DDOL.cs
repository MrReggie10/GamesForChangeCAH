using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class DDOL : MonoBehaviour
{
    [SerializeField] private GameObject optionMenu;
    public AudioMixer audioMixer;
    [SerializeField] private Animator BGAnimator;
    [SerializeField] private Animator fadeAnimator;

    private bool animationPlaying = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        optionMenu.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex == 1)
        {
            OpenMenu();
        }
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void OpenMenu()
    {
        if(!animationPlaying)
        {
            optionMenu.SetActive(true);
            BGAnimator.SetBool("PauseEnabled", true);
            fadeAnimator.SetBool("PauseEnabled", true);

            StartCoroutine("WaitForAnimation");
        }
    }

    IEnumerator WaitForAnimation()
    {
        animationPlaying = true;

        yield return new WaitForSeconds(1);

        animationPlaying = false;
        if(!BGAnimator.GetBool("PauseEnabled"))
        {
            optionMenu.SetActive(false);
        }
    }

    public void CloseMenu()
    {
        if (!animationPlaying)
        {
            BGAnimator.SetBool("PauseEnabled", false);
            fadeAnimator.SetBool("PauseEnabled", false);

            StartCoroutine("WaitForAnimation");
        }
    }
}
