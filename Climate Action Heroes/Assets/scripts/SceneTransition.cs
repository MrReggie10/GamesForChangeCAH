using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition sceneTransition;

    public Animator transition;

    private void Awake()
    {
        sceneTransition = this;
    }

    public void LoadScene(int scene)
    {
        StartCoroutine(LoadNextScene(scene));
    }

    IEnumerator LoadNextScene(int scene)
    {
        transition.SetTrigger("PauseEnabled");

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(scene);
    }
}
