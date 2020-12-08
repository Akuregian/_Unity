using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Player;

    public float speed = 10f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        Player.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        Player.Move(velocity * Time.deltaTime);
    
    }

}
