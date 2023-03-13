using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterMovement : MonoBehaviour
{
    [SerializeField] private List<GameObject> letters;
    [SerializeField] private float funcConst;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject letter in letters)
        {
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
            letter.GetComponent<Rigidbody2D>().gravityScale = Mathf.Abs(letter.transform.position.y-640) * (letter.transform.position.y - 640) * funcConst;
        }
    }
}
