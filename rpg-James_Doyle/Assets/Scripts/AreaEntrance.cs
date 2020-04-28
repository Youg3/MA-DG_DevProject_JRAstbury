using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{

    public string transitionName;

    // Start is called before the first frame update
    void Start()
    {
        //check to see if the player has a transition name that equals this string
        if (transitionName == PlayerController.instance.areaTransitionName)
        {
            //if it does, move it to this position and begin level
            PlayerController.instance.transform.position = transform.position;

            UIFade.instance.FadeFromBlack();
            GameManager.instance.fadingBetweenAreas = false;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
