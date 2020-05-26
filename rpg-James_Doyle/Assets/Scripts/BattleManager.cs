using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    private bool activeBattle;

    public GameObject battleScene;
    public Transform[] playerPos;
    public Transform[] enemyPos;


    public BattleChar[] playerPrefabs;
    public BattleChar[] enemyPrefabs;

    public List<BattleChar> activeBattleChar = new List<BattleChar>(); //to hold all active battle char

    //turn vars
    public int currentTurn;
    public bool turnWaiting; //used waiting for turn to end

    public GameObject uiButtonsHolder;

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            BattleStart(new string[]{"Eyeball","Skeleton", "Goblin Raider", "Eyeball", "Skeleton", "Goblin Raider" });
        }

        //handle what happens for turn
        if (activeBattle)
        {
            if (turnWaiting)
            {
                //waiting for player? display player input buttons
                if (activeBattleChar[currentTurn].isPlayer)
                {
                    uiButtonsHolder.SetActive(true);
                }
                else
                {
                    uiButtonsHolder.SetActive(false);
                    //enemy turn...
                    StartCoroutine(EnemyMoveCo());//this will delay the enemy attacks so they don't activate in less than a second
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            NextTurn();
        }

    }

    public void BattleStart(string[] enemiesToSpawn)
    {
        if (!activeBattle)
        {
            activeBattle = true;

            GameManager.instance.battleActive = true;
            GameMenu.instance.battleActive = true;

            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
            battleScene.SetActive(true);

            AudioManager.instance.PlayBGM(0);

            //player appearing
            for (int i = 0; i < playerPos.Length; i++)
            {
                if (GameManager.instance.playerStats[i].gameObject.activeInHierarchy) //check to see if the player char selected is active (Tim at first position is active)
                {
                    for (int j = 0; j < playerPrefabs.Length; j++)
                    {
                        if (playerPrefabs[j].charName == GameManager.instance.playerStats[i].charName)
                        {
                            BattleChar newPlayer = Instantiate(playerPrefabs[j], playerPos[i].position,
                                playerPos[i].rotation);

                            newPlayer.transform.parent = playerPos[i]; //creates newplayer as a child of the player positions array which can be used to move the char around during battle

                            //check which chars are active in scene to monitor stats by adding to list
                            activeBattleChar.Add(newPlayer);

                            //set player stats on active players
                            CharStats thePlayer = GameManager.instance.playerStats[i]; //quick store stat info
                            activeBattleChar[i].currentHp = thePlayer.currentHP;
                            activeBattleChar[i].maxHp = thePlayer.maxHP;
                            activeBattleChar[i].currentMp = thePlayer.currentMP;
                            activeBattleChar[i].maxMp = thePlayer.maxMP;
                            activeBattleChar[i].strength = thePlayer.strength;
                            activeBattleChar[i].defence = thePlayer.defense;
                            activeBattleChar[i].wpnPower = thePlayer.weaponPower;
                            activeBattleChar[i].armPower = thePlayer.armourPower;

                        }
                    }
                }
            }

            for (int i = 0; i < enemiesToSpawn.Length; i++)
            {
                if (enemiesToSpawn[i] != "")
                {
                    //find enemy in prefab list
                    for (int j = 0; j < enemyPrefabs.Length; j++)
                    {
                        if (enemyPrefabs[j].charName == enemiesToSpawn[i])
                        {
                            BattleChar newEnemy = Instantiate(enemyPrefabs[j], enemyPos[i].position,
                                enemyPos[i].rotation);

                            newEnemy.transform.parent = enemyPos[i];
                            activeBattleChar.Add(newEnemy);
                        }
                    }
                }
            }

            turnWaiting = true;
            currentTurn = Random.Range(0,activeBattleChar.Count); //starting a new battle randomizes who goes first.
        }
    }

    public void NextTurn()
    {
        currentTurn++;

        if (currentTurn >= activeBattleChar.Count)
        {
            currentTurn = 0;
        }

        turnWaiting = true;

        UpdateBattle();
    }

    public void UpdateBattle()
    {
        //update all info in battle
        //check enemies/players dead?
        bool allEnemiesDead = true;
        bool allPlayersDead = true; //these start true

        for (int i = 0; i < activeBattleChar.Count; i++)
        {
            if (activeBattleChar[i].currentHp < 0)
            {
                activeBattleChar[i].currentHp = 0;  //makes sure health can't show below 0
            }

            if (activeBattleChar[i].currentHp == 0)
            {
                //is the battler dead?
            }
            else
            {
                if (activeBattleChar[i].isPlayer)
                {
                    allPlayersDead = false;
                }
                else
                {
                    allEnemiesDead = false;
                }
            }
        }

        if(allEnemiesDead || allPlayersDead)
        {
            //we are at end of the battle
            if (allEnemiesDead)
            {
                //victory
            }
            else
            { 
                //lose
            }

            //set everything to false
            battleScene.SetActive(false);
            GameManager.instance.battleActive = false;
            activeBattle = false;
        }

    }

    public IEnumerator EnemyMoveCo()
    {
        //uses a co routine (something that can happen outside of the normal running order) so the rest of the game can continue running
        turnWaiting = false;
        yield return new WaitForSeconds(1f);//wait for this amount of time
        EnemyAttack();
        yield return new WaitForSeconds(1f);
        NextTurn();
    }
    public void EnemyAttack()
    {
        //deals with all AI decision
        List<int> players = new List<int>();
        for (int i = 0; i < activeBattleChar.Count; i++)
        {
            if (activeBattleChar[i].isPlayer && activeBattleChar[i].currentHp > 0)
            {
                players.Add(i); //finds the player characters that can be attacked.
            }
        }

        int selectedTarget = players[Random.Range(0, players.Count)];  //selects a random target

        activeBattleChar[selectedTarget].currentHp -= 30;
    }
}
