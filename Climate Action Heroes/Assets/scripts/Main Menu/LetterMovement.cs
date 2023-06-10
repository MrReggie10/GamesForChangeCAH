using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterMovement : MonoBehaviour
{
    [SerializeField] private List<GameObject> letters;
    [SerializeField] private float funcConst;

    private float yCenter;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject letter in letters)
        {
            yCenter = letter.GetComponent<RectTransform>().position.y - 50;
            letter.SetActive(false);
        }
            StartCoroutine(StartOscillation());
    }

    IEnumerator StartOscillation()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject letter in letters)
        {
            letter.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(GameObject letter in letters)
        {
            letter.GetComponent<Rigidbody2D>().gravityScale = Mathf.Abs(letter.transform.position.y-yCenter) * (letter.transform.position.y - yCenter) * funcConst;
        }
    }
}
