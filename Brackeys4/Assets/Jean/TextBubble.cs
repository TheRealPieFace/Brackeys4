using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class TextBubble : MonoBehaviour
{

    public GameObject FloatingTextPrefab;
    public bool typing;
    public float timer = 0;
    public float textSpeed = .02f;
    public float sentenceLinger = 1f;
    public int stringIndex = 0;
    public int sentenceIndex = 0;
    private TimelineController timeline;
    private bool wasRewinding = false;
    private int timeModifier = 1;

    Text dialogueText;

    private List<string> sentences;
    private GameObject currentBubble;

    void Start()
    {
        sentences = new List<string>();
        timeline = GetComponent<TimelineController>();
    }

    private void Update()
    {
        if(timeline.rewinding && timeModifier == 1)
        {
            timeModifier = -1;
        }
        else if(!timeline.rewinding && timeModifier == -1)
        {
            timeModifier = 1;
            wasRewinding = true;
        }

        if (wasRewinding && stringIndex <= -1)
        {
            stringIndex = 0;
            wasRewinding = false;
        }


        if (typing && !timeline.rewinding && sentences.Count !=0)
        {
            TypeForward();
        }
        if(typing && timeline.rewinding && sentences.Count != 0)
        {
            TypeBackward();
        }

    }

    private void TypeForward()
    {
        timer += Time.deltaTime;
        if(stringIndex == sentences[sentenceIndex].Length)
        {
            if(timer >= sentenceLinger)
            {
                if(sentenceIndex == sentences.Count - 1)
                {
                    
                    timer = 0;
                    sentenceIndex = 0;
                    stringIndex = 0;
                    typing = false;
                    sentences.Clear();
                    Destroy(gameObject.GetComponentInChildren<DestroyFloatingText>().gameObject);
                    
                }
                else
                {
                    StartSentence();
                    sentenceIndex++;
                    timer = 0;
                    stringIndex = 0;
                }
            }
        }
        else
        {
            if(timer >= textSpeed)
            {
                currentBubble.GetComponent<TextMesh>().text += sentences[sentenceIndex][stringIndex];
                stringIndex++;
                timer = 0;
            }
        }
    }



    private void TypeBackward()
    {
        timer -= Time.deltaTime;
        if(stringIndex == -1)
        {
            if (timer <= 0)
            {
                if (sentenceIndex == 0)
                {
                    timer = 0;
                    sentenceIndex = 0;
                    stringIndex = 0;
                    typing = false;
                    sentences.Clear();
                    Destroy(gameObject.GetComponentInChildren<DestroyFloatingText>().gameObject);
                }
                else
                {
                    StartSentence();
                    sentenceIndex--;
                    currentBubble.GetComponent<TextMesh>().text = sentences[sentenceIndex];
                    stringIndex = sentences[sentenceIndex].Length;
                    
                    timer = sentenceLinger;
                }
            }
        }
        else if(stringIndex > -1)
        {
            if(timer <= 0)
            {
                var newString = "";
                for(int i = 0; i < currentBubble.GetComponent<TextMesh>().text.Length - 1; i++)
                {
                    newString += currentBubble.GetComponent<TextMesh>().text[i];
                }
                currentBubble.GetComponent<TextMesh>().text = newString;
                stringIndex--;
                timer = textSpeed;
            }
        }
    }


    private void StartSentence()
    {
        if (gameObject.GetComponentInChildren<DestroyFloatingText>() != null)
        {
            Destroy(gameObject.GetComponentInChildren<DestroyFloatingText>().gameObject);
        }
        currentBubble = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
    }

    public void ShowTextBubble(Dialogue dialogue)
    {
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Add(sentence);
        }
        
        if (timeline.rewinding)
        {
            sentenceIndex = sentences.Count;
            stringIndex = sentences[sentenceIndex - 1].Length - 1;
        }

        StartSentence();
        typing = true;

        //DisplayNextSentence();

    }


    public void DisplayNextSentence()
    {

        //string sentence = sentences.Dequeue();
        StopAllCoroutines();
       //StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        if (gameObject.GetComponentInChildren<DestroyFloatingText>() != null)
        {

            Destroy(gameObject.GetComponentInChildren<DestroyFloatingText>().gameObject);

        }
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);

        foreach (char letter in sentence.ToCharArray())
        {
            go.GetComponent<TextMesh>().text += letter;
            yield return null;
        }
    }

}
