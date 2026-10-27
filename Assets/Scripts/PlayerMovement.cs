using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Horizontal Movement")]
    [SerializeField] float moveSpeed = 8f;

    [Header("Jump Settings")]
    [SerializeField] float jumpForce = 12f;
    [SerializeField] float gravity = 30f;

    [Header("Player Collider")]
    [SerializeField] AABBCollider playerCollider;

    [Header("Colliders")]
    [SerializeField] AABBCollider[] groundColliders;
    [SerializeField] AABBCollider[] obstacles;
    [SerializeField] AABBCollider[] fallCollider;

    private Vector2 velocity;
    private bool isGrounded;

    void Update()
    {
 
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        velocity.x = horizontalInput * moveSpeed;

        
        if (isColliding(groundColliders))
        {
            isGrounded = true;

            if (velocity.y < 0)
            {
                velocity.y = 0;
            }
        }
        else
        {
            isGrounded = false;
        }

       
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = jumpForce;
            isGrounded = false;
        }

      
        if (!isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }

       
        transform.Translate(velocity * Time.deltaTime);

        if(isColliding(fallCollider) ||  isColliding(obstacles))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    bool Overlaps(AABBCollider a, AABBCollider b)
    {
        return a.min.x <= b.max.x && b.min.x <= a.max.x &&
               a.min.y <= b.max.y && b.min.y <= a.max.y;
    }

    bool isColliding(AABBCollider[] Colliders)
    {
      
        foreach(AABBCollider colliders in Colliders)
        {
            if (Overlaps(playerCollider, colliders))
            {
                return true;
            }
        }

        return false;
    }


}
