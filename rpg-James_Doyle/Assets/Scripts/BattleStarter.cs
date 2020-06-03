using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStarter : MonoBehaviour
{

    public BattleType[] potentialBattles;

    public bool activateOnEnter, activateOnStay, activateOnExit;

    public float pauseBetweenBattles;
    private float battleCounterTime;

    private bool inBattleZone;

    public bool deactivateAfterStart;
    public bool cannotFlee;

    public bool shouldCompleteQuest;
    public string questToComplete;

    // Start is called before the first frame update
    void Start()
    {
        //sets the battle counter time
        battleCounterTime = Random.Range(pauseBetweenBattles * .5f, pauseBetweenBattles * 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //check to see if player is in area and can move
        if (inBattleZone && PlayerController.instance.canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                //start a timer if player is moving around
                battleCounterTime -= Time.deltaTime;
            }

            if (battleCounterTime <= 0)
            {
                //reset counter time when at 0
                battleCounterTime = Random.Range(pauseBetweenBattles * .5f, pauseBetweenBattles * 1.5f);
                //start a battle
                StartCoroutine(StartBattleCo());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (activateOnEnter)
            {
                StartCoroutine(StartBattleCo());
            }
            else
            {
                inBattleZone = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (activateOnExit)
            {
                StartCoroutine(StartBattleCo());
            }
            else
            {
                inBattleZone = false;
            }
        }
    }

    public IEnumerator StartBattleCo()
    {
        UIFade.instance.FadeToBlack();
        GameManager.instance.battleActive = true;

        //select a random battle from list of potential battles
        int selectedBattle = Random.Range(0, potentialBattles.Length);
        //battle rewards as set on the battle
        BattleManager.instance.rewardItems = potentialBattles[selectedBattle].rewardItems;
        BattleManager.instance.rewardExp= potentialBattles[selectedBattle].rewardExp;

        yield return new WaitForSeconds(1.5f);
        //set the enmies from the potential battles
        BattleManager.instance.BattleStart(potentialBattles[selectedBattle].enemies, cannotFlee);
        //fade to bg screen
        UIFade.instance.FadeFromBlack();

        if (deactivateAfterStart) //use this for one off battles
        {
            gameObject.SetActive(false);
        }

        //pas the info on quests over to the reward screen
        BattleReward.instance.markQuestComplete = shouldCompleteQuest;
        BattleReward.instance.questToMark = questToComplete;
    }
}
