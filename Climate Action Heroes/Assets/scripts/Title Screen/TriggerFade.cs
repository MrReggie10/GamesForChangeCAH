using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFade : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine("WaitForFade");
    }

    IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(3);

        SceneTransition.sceneTransition.LoadScene(1);
    }
}
