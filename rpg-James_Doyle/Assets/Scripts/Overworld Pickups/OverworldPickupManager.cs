using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPickupManager : MonoBehaviour
{

    public string[] pickupMarkerNames;
    public bool[] pickupMarkersComplete;

    public static OverworldPickupManager instance;

    void Start()
    {
        instance = this;
        pickupMarkersComplete = new bool[pickupMarkerNames.Length]; //set to same length of pre named unique items

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(CheckIfComplete("is"));
            MarkPickupComplete("is");
            //MarkPickupIncomplete("Talk");
        }
    }

    public int GetPickupNumber(string pickupToFind)
    {
        for (int i = 0; i < pickupMarkerNames.Length; i++)
        {
            if (pickupMarkerNames[i] == pickupToFind)
            {
                return i;
            }
        }

        Debug.LogError("Pickup: " + pickupToFind + "does not exist");
        return 0; //if no quest found, return element 0 which is null
    }

    public bool CheckIfComplete(string pickupToCheck)
    {
        Debug.Log(pickupToCheck);
        if (GetPickupNumber(pickupToCheck) != 0)
        {
            return pickupMarkersComplete[GetPickupNumber(pickupToCheck)]; //returns value if public int is not null
        }

        return false;
    }

    public void MarkPickupComplete(string pickupToMark)
    {
        pickupMarkersComplete[GetPickupNumber(pickupToMark)] = true;
        UpdateLocalPickupObj();
    }

    public void MarkPickupIncomplete(string pickupToMark)
    {
        pickupMarkersComplete[GetPickupNumber(pickupToMark)] = false;
        UpdateLocalPickupObj();
    }

    public void UpdateLocalPickupObj()
    {
        OverworldObjActivator[] pickupObjects = FindObjectsOfType<OverworldObjActivator>(); //find all objects in scene that have the quest obj activator attached

        foreach (var objActivator in pickupObjects)
        {
            Debug.Log(pickupObjects);
        }

        if (pickupObjects.Length > 0)
        {
            for (int i = 0; i < pickupObjects.Length; i++)
            {
                pickupObjects[i].CheckCompletion(); //check against each quest if it's complete
            }
        }
    }
}