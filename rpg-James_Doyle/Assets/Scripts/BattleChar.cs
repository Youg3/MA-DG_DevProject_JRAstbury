using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BattleChar : GenCharStats
{
    public bool isPlayer;

    public string[] movesAvailable;
    
    public bool hasDied;
    
    public SpriteRenderer theSprite;
    public Sprite deadSprite;
    public Sprite aliveSprite;

    private bool shouldFade;
    public float fadeSpeed = 1f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFade)
        {
            theSprite.color = new Color(Mathf.MoveTowards(theSprite.color.r, 1, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(
                theSprite.color.g, 0, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theSprite.color.b, 0, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(
                theSprite.color.a, 0, fadeSpeed * Time.deltaTime));

            if (theSprite.color.a == 0)
            {
                gameObject.SetActive(false);//deactivate gameobject
            }
        }
    }

    public void EnemyFade()
    {
        shouldFade = true;
    }
}
