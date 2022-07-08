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

    // Update is called once per frames
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if(Input.GetButtonDown("Jump")){
            jump = true;
        }
    }

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
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
