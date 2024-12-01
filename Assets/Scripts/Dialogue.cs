using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    //Fields
    //Window
    public GameObject window;
    //Indicator
    //Text component
    public TMP_Text dialogueText;
    //Dialogues list
    public List<string> dialogues;
    //writing speed
    public float writingSpeed;
    //Index on dialogue
    private int index;
    //Character index
    private int charIndex;
    //Started boolean
    private bool started;
    //Wait for next Boolean
    private bool waitForNext;
    //public DialogueActivator activator;

    void Start()
    {
    }

    public void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }

    //Start dialogue
    public void StartDialogue()
    {
        if (started)
            return;

        //Boolean to indicate that we have started
        started = true;
        //Show the window
        ToggleWindow(true);
        //Start with first dialogue
        FirstDialogue(0);
    }

    private void FirstDialogue(int i)
    {
        //start index at zero
        index = i;
        //Reset the character ind
        charIndex = 0;
        //Clear the dialogue component text
        dialogueText.text = string.Empty;
        //Stat writing
        StartCoroutine(Writing());
    }

    //End dialogue
    public void EndDialogue()
    {
        //Hide the Window
        ToggleWindow(false);
    }

    //Writing Logic
    IEnumerator Writing()
    {
        string currentDialogue = dialogues[index];
        //Write the Character
        dialogueText.text += currentDialogue[charIndex];
        //increase the character Index
        charIndex++;

        //Check of end of sentence
        if(charIndex < currentDialogue.Length)
        {
            //HELLO (= 5 char) H=0 E=1...O=4 ?=5
            //Wait X seconds
            yield return new WaitForSeconds(writingSpeed);
            //Restart the same process
            StartCoroutine(Writing());
        }
        else
        {
            //End this sentence and wait for next one
            waitForNext = true;
        }
    }

    private void Update()
    {
        if (!started)
            return;

        if(waitForNext && Input.GetKeyDown(KeyCode.E))
        {
            waitForNext = false;
            index++;

            if(index < dialogues.Count)
            {
                FirstDialogue(index);
            }
            else
            {
                //End Dialogue
                EndDialogue();
            }
        }
    }
}
