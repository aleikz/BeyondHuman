using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private float direction = 0f;
    public float jumpForce = 8f;
    private Rigidbody2D player;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;
    private bool isGrounded;
    public bool isDashing;
    void Start(){
        player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        direction = Input.GetAxis("Horizontal");

        if (!isDashing)
        {
            if (direction > 0f)
            {
                player.linearVelocity = new Vector2(direction * speed, player.linearVelocity.y);
            }
            else if (direction < 0f)
            {
                player.linearVelocity = new Vector2(direction * speed, player.linearVelocity.y);
            }
            else
            {
                player.linearVelocity = new Vector2(0f, player.linearVelocity.y);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            player.linearVelocity = new Vector2(player.linearVelocity.x, jumpForce);
        }
    }
}
