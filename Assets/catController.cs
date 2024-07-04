using UnityEngine;
using UnityEngine.InputSystem;
public class catController : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private Vector3 movement;
    [SerializeField]
    private int index;
    private void Start()
    {
    }

    private void Awake()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
        index = playerInput.playerIndex;
        controller = gameObject.GetComponent<CharacterController>();
        if (controller == null)
        {
            controller = gameObject.GetComponent<CharacterController>();
            Debug.Log("Character Controller added to the game object");

        }
    }
    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            move.y = -0.5f;
        }
        controller.Move(move * Time.deltaTime * playerSpeed);
        move.y = 0;
        if (move != Vector3.zero)
        {
            var currentRotation = gameObject.transform.rotation;
            gameObject.transform.forward =
                Vector3.RotateTowards(gameObject.transform.forward, move, 0.1f, 0.0f);
        }
        movement = new Vector3(input.x, 0, input.y);

    }
    public void OnJump()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -0.5f;
        }
        if (groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }

    void Update()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
        Debug.Log(playerInput.playerIndex + " " + index);
        if (playerInput.playerIndex != index)
        {
            return;
        }


        controller.Move(movement * Time.deltaTime * playerSpeed);

        if (movement != Vector3.zero)
        {
            gameObject.transform.forward =
                           Vector3.RotateTowards(gameObject.transform.forward, movement, 0.1f, 0.0f);

        }

        // Changes the height position of the player..


        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -0.5f;
        }
    }
}
