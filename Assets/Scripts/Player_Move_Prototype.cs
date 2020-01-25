using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_Prototype : MonoBehaviour{

    public int playerSpeed = 10;
    private bool facingRight = false;
    public int playerJumpPower = 1250;
    private float moveX;
    public bool isGrounded;


    // Start is called before the first frame update
    void Start(){
        
    }

    void FixedUpdate(){
        PlayerRaycast();
        PlayerMove();
        

    }

    void PlayerMove(){
        //CONTROLS
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButton("Jump") && isGrounded == true){
            Jump();
        }
        //ANIMATION

        //PLAYER DIRECTION
        if (moveX < 0.0f && facingRight == false){
            FlipPlayer();
        }else if (moveX > 0.0f && facingRight == true){
            FlipPlayer();
        }
        //PHYSICS
        gameObject.GetComponent<Rigidbody2D>().velocity
            = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump(){
        //JUMPING CODE
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        isGrounded = false;
    }

    void FlipPlayer(){
        //flips player when you turn the opposite direction
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void PlayerRaycast() {
        //TODO FIX THIS CODE
        //Ray Up
        RaycastHit2D rayUp = Physics2D.Raycast(transform.position, Vector2.up);
        if (rayUp != false && rayUp.collider != false && rayUp.distance < 0.9f && rayUp.collider.name == "Box_2") {
            Destroy(rayUp.collider.gameObject);
        }

        //Ray Down
        //if the player jumps on an enemy, the player will bounce upwards and enemy will fall
        RaycastHit2D rayDown = Physics2D.Raycast(transform.position, Vector2.down);
        if(rayDown != false && rayDown.collider != false && rayDown.distance < 0.9f && rayDown.collider.tag == "enemy") {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000);
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 200);
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 8;
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            rayDown.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            rayDown.collider.gameObject.GetComponent<Enemy_Move>().enabled = false;
            //Destroy(hit.collider.gameObject);
        }

        //allows player to jump off of anything that's not an enemy
        if (rayDown != false && rayDown.collider != false && rayDown.distance < 0.9f && rayDown.collider.tag != "enemy") {
            isGrounded = true;
        }
    }
}
