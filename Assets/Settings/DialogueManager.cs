using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Dialogue dialogue;
    public Transform spawnDialogue;
    private Queue<string> sentences;
    public Transform HunterExpress;
    public Transform BarKeeperExpress;
    public Animator animator;
    int count = 0;
    float check;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sentences = new Queue<string>();

        foreach (string sentence in dialogue.sentence) {
            sentences.Enqueue(sentence);
	    }

        LoadNextSentence();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextSentence() {

        if (sentences.Count == 0) {
            spawnDialogue.gameObject.SetActive(false);
            return;
	    }

        count += 1;

        check = count % 2;

        /**if (count == 4)
        {
            spawnDialogue.gameObject.SetActive(false);
            return;
        }**/

        BarKeeperExpress.gameObject.SetActive(false);
        HunterExpress.gameObject.SetActive(false);

        Debug.Log(check);

        if (check == 0)
        {
            Debug.Log("Bar");
            BarKeeperExpress.gameObject.SetActive(true);
            

        }
        else
        {
            Debug.Log("Hunt");
            //BarKeeperExpress.gameObject.SetActive(false);
            HunterExpress.gameObject.SetActive(true);
            animator.SetBool("smilingEyesClosed", true);
        }
        
        StartCoroutine(TypingWords());


    }

    IEnumerator TypingWords() {
        string line = sentences.Dequeue();
        dialogueText.text = " ";

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
}
