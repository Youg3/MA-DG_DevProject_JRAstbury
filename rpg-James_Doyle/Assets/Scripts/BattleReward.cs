using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleReward : MonoBehaviour
{
    public static BattleReward instance;

    public Text expText, itemText;

    public GameObject battleRewardScreen;

    public string[] rewardItems;
    public int expEarned;

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            OpenRewardScreen(54,new string[]{"Iron Sword", "Iron Armour"});
        }
    }

    public void OpenRewardScreen(int exp, string[] rewards)
    {
        expEarned = exp;
        rewardItems = rewards;

        expText.text = "Everyone earned " + expEarned + " EXP!";
        itemText.text = "";

        foreach (string rewardItem in rewardItems)
        {
            itemText.text += rewardItem + "\n";
        }

        /*for (int i = 0; i < rewardItems.Length; i++)
        {
            itemText.text += rewardItem + "\n";
        }*/

        battleRewardScreen.SetActive(true);

    }

    public void CloseRewardScreen()
    {
        for (int i = 0; i < GameManager.instance.playerStats.Length; i++)
        {
            if (GameManager.instance.playerStats[i].gameObject.activeInHierarchy)
            {
                GameManager.instance.playerStats[i].AddExp(expEarned);
            }
        }

        for (int i = 0; i < rewardItems.Length; i++)
        {
            GameManager.instance.AddItem(rewardItems[i]);
        }

        battleRewardScreen.SetActive(false);
        GameManager.instance.battleActive = false;

    }
}
