using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementFPS : MonoBehaviour
{
    [SerializeField] CharacterController controller ;

    [SerializeField] float jumpHeight=5f;
    [SerializeField] [Range(0.0f, 1f)] float silentWalktoWalkRatio = 0.5f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] [Range(1f, 5f)] float speedToWalkRatio = 2f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] [Range(0.0f, .5f)] float moveSmoothTime = 0.3f;


    float velocityY = 0.0f;

    float runSpeed ;
    float silentWalkSpeed ;
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    void Start()
    {
        runSpeed = speedToWalkRatio * walkSpeed;
        silentWalkSpeed = silentWalktoWalkRatio * walkSpeed;
    }

    void Update()
    {
        UpdateMovement();
    }

    #region
    //Comments

    //Default is running
    //can shift walk
    //can jump


    //TODO
    #endregion

    #region playerMovementFunctions

    Vector2 GetInputDir()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }
    bool GetJumpInpt()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    bool GetRunInpt()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
    bool GetWalkInpt()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
    bool GetSilentWalkInpt()
    {
        return Input.GetKey(KeyCode.LeftControl);
    }


    Vector3 GetUpdatedVelocity()
    {
        if (GetJumpInpt() && controller.isGrounded)
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else
        {
            if (controller.isGrounded)
                /*velocityY -= 1f;*/
                velocityY = 0f;
        }
        

        velocityY += gravity * Time.deltaTime;

        //run->slow walk
        float currSpeed = GetWalkInpt() ? walkSpeed : runSpeed;


        //slow walk -> walk -> run
        /* float currSpeed = GetRunInpt() ? runSpeed :(GetSilentWalkInpt()?silentWalkSpeed:walkSpeed);*/
        return (transform.forward * currentDir.y + transform.right * currentDir.x) * currSpeed + Vector3.up * velocityY;
    }

    void UpdateMovement()
    {
        Vector2 targetDir = GetInputDir();



        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        Vector3 velocity = GetUpdatedVelocity();

        controller.Move(velocity * Time.deltaTime);

    }
    #endregion 
}


/*if (Input.GetButtonDown("Jump") && _isGrounded)
{
    _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
}*/