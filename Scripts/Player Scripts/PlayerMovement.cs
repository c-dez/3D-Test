using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   private CharacterController controller;
   private Vector3 playerVelocity;
   private bool isGrounded;
   [SerializeField] private float playerSpeed;
   [SerializeField] private float jumpHeight;
   private float Gravity = -10f;
   public bool isFalling;
   
    
   private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMove();
        PlayerJump();
       
       
       if(isGrounded )
       {
            isFalling = false;
       }
       if(!isGrounded && playerVelocity.y < -1)
       {
            isFalling = true;
       }

    }

   
    
    private void PlayerMove()
    {
        isGrounded = controller.isGrounded;
        if(isGrounded && playerVelocity.y < 0f)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        
        controller.Move(move.normalized * Time.deltaTime * playerSpeed);

    }

    private void PlayerJump()
    {
         if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3f * Gravity);
        }

        playerVelocity.y += Gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
    }

    

   

}
