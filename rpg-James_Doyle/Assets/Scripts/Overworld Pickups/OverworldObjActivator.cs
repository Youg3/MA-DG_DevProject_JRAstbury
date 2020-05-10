using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldObjActivator : MonoBehaviour
{
    public GameObject objectToActivate;
    public string pickupToCheck;
    public bool activeIfComplete;

    private bool initialCheckDone;

    void Start()
    {

    }

    void Update()
    {
        if (!initialCheckDone)
        {
            initialCheckDone = true;
            CheckCompletion();
        }
    }

    public void CheckCompletion()
    {
        //Debug.Log("Pickup Check");
        //Debug.Log(pickupToCheck);

        bool test = OverworldPickupManager.instance.CheckIfComplete(pickupToCheck);
        //Debug.Log(test);
        if (test)
        {
            objectToActivate.SetActive(activeIfComplete);
            //Destroy(this);
        }
    }
}

