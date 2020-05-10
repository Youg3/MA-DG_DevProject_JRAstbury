using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPickupMarker : MonoBehaviour
{
    public string questToMark;
    public bool markComplete;
    public bool markOnEnter;
    private bool canMark;

    public bool deactivateOnMark;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canMark && Input.GetButtonDown("Fire1"))
        {
            canMark = false;
            MarkPickup();
        }
    }

    public void MarkPickup()
    {
        if (markComplete)
        {
            OverworldPickupManager.instance.MarkPickupComplete(questToMark);
        }
        else
        {
            OverworldPickupManager.instance.MarkPickupIncomplete(questToMark);
        }

        gameObject.SetActive(!deactivateOnMark);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (markOnEnter)
            {
                MarkPickup();
            }
            else
            {
                canMark = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canMark = false;
        }
    }
}
