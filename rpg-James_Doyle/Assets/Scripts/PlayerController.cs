using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //vars
    public Rigidbody2D theRB;
    public Animator playerAnim;
    public float moveSpeed;

    public static PlayerController instance;

    //stored var to compare against with which exit the player took
    public string areaTransitionName;

    //to restrict the player to the game area
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    public bool canMove = true;


    // Start is called before the first frame update
    void Start()
    {
        //checks that this is the only player in scene
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //check to see if the other player controller object is not the player then destroy
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        //don't destroy player on changing scenes
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        }
        else{theRB.velocity = Vector2.zero;}

        //anims
        playerAnim.SetFloat("moveX", theRB.velocity.x);
        playerAnim.SetFloat("moveY", theRB.velocity.y);
        
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if (canMove)
            {
                playerAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                playerAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }

        //keep player inside map boundary
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
            Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);

    }

    public void SetBounds(Vector3 bottomLeft, Vector3 topRight)
    {
        //set player boundary from camera tile map
        bottomLeftLimit = bottomLeft + new Vector3(.5f, 1f, 0f);
        topRightLimit = topRight + new Vector3(-.5f,-1f,0f);
    }
}
