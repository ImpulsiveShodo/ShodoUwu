using UnityEngine;
using UnityEngine.InputSystem;
public class CatController : MonoBehaviour
{
    [SerializeField]
    private CatCharacterController controller;
    [SerializeField]
    private float playerSpeed = 1.0f;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
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
    }
    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        controller.Move(move * Time.deltaTime * playerSpeed);
        movement = new Vector3(input.x, 0, input.y);
    }
    public void OnJump()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            controller.Move(new Vector3(0, Mathf.Sqrt(jumpHeight * -3.0f * gravityValue) * 0.1f, 0));
        }
        controller.isGrounded = false;
    }

    void Update()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
        if (playerInput.playerIndex != index)
        {
            return;
        }
        controller.Move(movement * Time.deltaTime * playerSpeed);
        //var currRotation = this.gameObject.transform.rotation;
        //// change to euler angles
        //var currEuler = currRotation.eulerAngles;
        //// set the x and z to 0
        //currEuler.x = 0;
        //currEuler.z = 0;
        //// set the euler angles back to the rotation
        //currRotation.eulerAngles = currEuler;
        //// set the rotation to the current rotation
        //this.gameObject.transform.rotation = currRotation;

    }
}
