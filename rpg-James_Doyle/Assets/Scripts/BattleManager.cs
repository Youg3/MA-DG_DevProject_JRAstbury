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
            BattleStart(new string[]{"Eyeball","Skeleton"});
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
        }
    }
}
