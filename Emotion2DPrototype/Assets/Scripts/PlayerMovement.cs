using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    public bool canMove = true;
    public bool touch = true;

    /*private void Start() {
        if(PlayerPrefs.GetInt("touch") == 0)
        {
            touch = false;
        }
    }*/

    public void OnIdle()
    {
        horizontalMove = 0f;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }
    public void OnLeftButton()
    {
        horizontalMove = -runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }
    public void OnRightButton()
    {
        horizontalMove = runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }
    public void OnJumpButton()
    {
        jump = true;
        animator.SetBool("IsJumping", true);
    }

    public void OnLanding (){
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching){
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate() {
        //Move the character
        if(canMove)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
        /* else if(canMove && !touch)
        {
            if(Input.GetButtonDown("Jump")){
                OnJumpButton();
            }
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }*/
    }
}
