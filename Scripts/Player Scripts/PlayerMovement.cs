using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   private CharacterController controller;
   public Vector3 fallingVelocity;
   public bool isGrounded;// public for testing
   [SerializeField] private float moveSpeedMod;
   [SerializeField] private float jumpHeight;
   private float Gravity = -10f;

   private Vector3 moveDirection;
   
    
   private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MoveDirection();
        CheckIsGrounded();
        PlayerMove();
        PlayerJump();
        PlayerRotation();
    
    }

    private void MoveDirection()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
    private void PlayerRotation()
    {
        float targetAngle = Mathf.Atan2(moveDirection.x,moveDirection.z) * Mathf.Rad2Deg;
        if(moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(0, targetAngle,0); 
        }
    }
    
    private void CheckIsGrounded()
    {
        isGrounded = controller.isGrounded;
    }
    private void PlayerMove()
    {
        if(isGrounded && fallingVelocity.y < 0f)
        {
            fallingVelocity.y = 0f;
        }
        controller.Move(moveDirection.normalized * Time.deltaTime * moveSpeedMod);
    }
    private void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            fallingVelocity.y += Mathf.Sqrt(jumpHeight * -3f * Gravity);
        }
        fallingVelocity.y += Gravity * Time.deltaTime;
        controller.Move(fallingVelocity * Time.deltaTime);

    }

   
}
