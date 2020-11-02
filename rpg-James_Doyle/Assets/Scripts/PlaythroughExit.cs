using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaythroughExit : MonoBehaviour
{

    //controls scene change
    public float waitToLoad = 1f;
    private bool shouldLoadAfterFade;

    public GameObject modMenu;

    public void Start()
    {
        modMenu = GameObject.FindWithTag("ModMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLoadAfterFade)
        {
            waitToLoad -= Time.deltaTime;
            if (waitToLoad <= 0)
            {
                shouldLoadAfterFade = false;
                ModMenu.instance.OpenMenu();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Contact");

            //trigger, fade to black, block movement.
            shouldLoadAfterFade = true;
            GameManager.instance.fadingBetweenAreas = true;
            UIFade.instance.FadeToBlack();
        }
    }
}
