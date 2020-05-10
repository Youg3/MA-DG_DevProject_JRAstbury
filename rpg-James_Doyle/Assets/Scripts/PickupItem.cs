using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string[] lines; //any text lines for picking up item

    private bool canPickup;

    public bool hasText = false; //to activate dialogue box if item has text

    /*[Header("Overworld Pickup Settings")]
    public bool shouldActivatePickup;
    public string pickupToMark;
    public bool markComplete;
    public bool markIncomplete;*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickup && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove)
        {
            GameManager.instance.AddItem(GetComponent<Item>().itemName);
            Destroy(gameObject); //remove object from the world once picked up

            if (hasText)
            {
                DialogManager.instance.ShowDialog(lines, hasText); //unsure about this...
                //DialogManager.instance.ShouldActivateQuestAtEnd(pickupToMark, markComplete, markIncomplete);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canPickup = false;
        }
    }
}
