using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    //ref to game elements
    public Text dialogueText;
    public Text nameText;
    public GameObject dialogueBox;
    public GameObject nameBox;


    public string[] dialogueLines;
    public int currentLine;
    private bool justStarted;

    public static DialogManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //dialogueText.text = dialogueLines[currentLine];
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                if (!justStarted)
                {
                    currentLine++;
                    if (currentLine >= dialogueLines.Length)
                    {
                        //deactivates dialogue box when out of lines
                        dialogueBox.SetActive(false);
                    }
                    else
                    {
                        //update dialogue text box 
                        dialogueText.text = dialogueLines[currentLine];
                    }
                }
                else
                {
                    justStarted = false;
                }
            }
        }
    }

    public void ShowDialog(string[] newLines)
    {
        dialogueLines = newLines;

        currentLine = 0;

        dialogueText.text = dialogueLines[0];
        dialogueBox.SetActive(true);

        justStarted = true;
    }
}
