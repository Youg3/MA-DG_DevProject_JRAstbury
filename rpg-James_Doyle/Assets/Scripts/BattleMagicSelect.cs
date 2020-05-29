using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMagicSelect : MonoBehaviour
{
    public string spellName;
    public int spellCost;
    public Text nameText;
    public Text costText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        int currentMpAmount = BattleManager.instance.activeBattleChar[BattleManager.instance.currentTurn].currentMp;

        //new var for finding whether we can actually use a move without going below 0 MP
        int mpCheck = currentMpAmount - spellCost;
        
        if (currentMpAmount >= spellCost && mpCheck >= 0)
        {
            BattleManager.instance.magicMenu.SetActive(false);
            BattleManager.instance.OpenTargetMenu(spellName);
            BattleManager.instance.activeBattleChar[BattleManager.instance.currentTurn].currentMp -= spellCost;
        }
        else if(mpCheck < 0 || currentMpAmount == 0)
        {
            //notification when not enough MP
            BattleManager.instance.battleNotice.theText.text = "Not Enough MP";
            BattleManager.instance.battleNotice.Activate();
            //turns magic menu off
            BattleManager.instance.magicMenu.SetActive(false);
        }
    }
}
