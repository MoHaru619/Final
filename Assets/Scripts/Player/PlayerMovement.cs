using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    public TextMeshProUGUI uiText;
    int totalCoins;
    int coinsCollected;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
   
    private float horizontalInput;
     private bool grounded;

    private void Awake()    //everytime a script is loaded
    {
        //grabs refernece for rigidbody and animator object
        body = GetComponent<Rigidbody2D>();    //script is attached to the player and getcomponent is checking if its attached
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        coinsCollected = 0;
        totalCoins = GameObject.FindGameObjectsWithTag("coin").Length;

    }

    private void Update()    //runs in every frame
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        



        //flips player sprite when moving left/right
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(5,5,5);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-5, 5, 5);

        anim.SetBool("run", horizontalInput != 0);    //changes the animation to run
        anim.SetBool("grounded", isGrounded());


        if (wallJumpCooldown > 0.2f)
        {

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {

                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 7;

            if (Input.GetKey(KeyCode.Space))  //if player has pressed space and is on the ground
            {

                Jump();
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
                {
                    SoundManager.instance.PlaySound(jumpSound);
                }
            }
        }
        else
            wallJumpCooldown += Time.deltaTime;



        string uiString = "x " + coinsCollected + "/" + totalCoins;
        uiText.text = uiString;
    }
    private void Jump()    //method for jumping
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
            

        }
        else if (onWall() && !isGrounded())
        {

            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            }
            else 
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCooldown = 0;
        }
    }
    void OnTriggerEnter2D(Collider2D coll)     //added coin code from class 
    {
        if (coll.gameObject.tag == "coin")
        {
            coinsCollected++;
            Destroy(coll.gameObject);
        }
    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;

    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()

    { 
    
    return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
