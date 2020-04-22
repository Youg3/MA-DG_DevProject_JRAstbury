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
            //destroy new player object
            Destroy(gameObject);
        }

        //don't destroy player on changing scenes
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"))*moveSpeed;

        playerAnim.SetFloat("moveX", theRB.velocity.x);
        playerAnim.SetFloat("moveY", theRB.velocity.y);


        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            playerAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            playerAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }

    }
}
