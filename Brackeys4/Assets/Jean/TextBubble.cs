using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class TextBubble : MonoBehaviour
{

    public GameObject FloatingTextPrefab;
    

    Text dialogueText;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void ShowTextBubble(Dialogue dialogue)
    {
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);

        }
       
        DisplayNextSentence();

    }


    public void DisplayNextSentence()
    {

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        if (GameObject.Find("FloatingText(Clone)") != null)
        {

            Destroy(GameObject.Find("FloatingText(Clone)"));

        }
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);

        foreach (char letter in sentence.ToCharArray())
        {
            go.GetComponent<TextMesh>().text += letter;
            yield return null;
        }
    }

}
