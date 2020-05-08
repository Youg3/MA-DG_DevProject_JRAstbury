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

    private string questToMark;
    private bool markQuestComplete;
    private bool shouldMarkQuest;

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
                        GameManager.instance.dialogActive = false;
                        //check to see if we should mark a quest
                        if (shouldMarkQuest)
                        {
                            shouldMarkQuest = false;
                            if (markQuestComplete)
                            {
                                QuestManager.instance.MarkQuestComplete(questToMark);
                            }
                            else
                            {
                                QuestManager.instance.MarkQuestIncomplete(questToMark);
                            }
                        }

                    }
                    else
                    {
                        //check to see if it's a name
                        CheckForName();
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

    public void ShowDialog(string[] newLines, bool isNPC)
    {
        dialogueLines = newLines;

        currentLine = 0;

        CheckForName();

        dialogueText.text = dialogueLines[currentLine];
        dialogueBox.SetActive(true);

        justStarted = true;

        //activate namebox is npc
        nameBox.SetActive(isNPC);

        GameManager.instance.dialogActive = true;
    }

    public void CheckForName()
    {
        if (dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-","");
            currentLine++;
        }
    }
    public void ShouldActivateQuestAtEnd(string questName, bool markComplete)
    {
        questToMark = questName;
        markQuestComplete = markComplete;

        shouldMarkQuest = true;
    }
}
