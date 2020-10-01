using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ModButtons : MonoBehaviour
{
    private GameObject[] bossDragon;

    public Image bossImage;
    // Start is called before the first frame update
    void Start()
    {
        if (bossDragon == null)
        {
            bossDragon = GameObject.FindGameObjectsWithTag("Boss");
        }
        //bossDragon = GameObject.FindGameObjectWithTag("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayButtonSound()
    {
        AudioManager.instance.PlaySFX(4);
    }

    public void CharEnable()
    {
        //activate/deactivate char
        foreach (var boss in bossDragon)
        {
            boss.SetActive(true);

        }

        bossImage.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        //bossDragon.SetActive(true);
    }
    public void CharDisable()
    {
        foreach (var boss in bossDragon)
        {
            boss.SetActive(false);
        }

        bossImage.GetComponent<Image>().color = new Color32(121, 121, 123,100);
    }

    public void SaveCharStats()
    {
        //calls func from mod script
        CharMod.instance.SaveInput();
    }
}
