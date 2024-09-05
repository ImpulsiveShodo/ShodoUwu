using UnityEngine;
using UnityEngine.InputSystem;
public class CatController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 1.0f;
    [SerializeField]
    private float maxSpeed = 5.0f;
    [SerializeField]
    private float jumpHeight = 2.0f;
    [SerializeField]
    private int index;
    [SerializeField]
    private float rotationSpeed = 0.02f;
    [SerializeField]
    private float WakeUpSpeed = 0.01f;
    [SerializeField]
    private float fiction = 0.5f;
    [SerializeField]
    private float fov = 200.0f;
    [SerializeField]
    private float viewDistance = 1000.0f;

    // on the fiction change reset the material's friction
    private void OnValidate()
    {
        var pm = gameObject.GetComponent<MeshCollider>().material;
        pm.dynamicFriction = fiction;
        pm.staticFriction = fiction * 1.2f;
        var controller = gameObject.GetComponent<CatCharacterController>();
        gameObject.GetComponent<CatCharacterController>().maxSpeed = maxSpeed;
        gameObject.GetComponent<CatCharacterController>().rotationSpeed = rotationSpeed;
        gameObject.GetComponent<CatCharacterController>().WakeUpSpeed = WakeUpSpeed;
        gameObject.GetComponent<CatCharacterController>().fov = fov;
        gameObject.GetComponent<CatCharacterController>().viewDistance = viewDistance;

    }

    private CatCharacterController controller;
    private PlayerInput playerInput;
    private Vector3 movement;

    private void Start()
    {
        controller = gameObject.GetComponent<CatCharacterController>();
        controller.rotationSpeed = rotationSpeed;
        controller.WakeUpSpeed = WakeUpSpeed;
        controller.maxSpeed = maxSpeed;
        controller.fov = fov;
        controller.viewDistance = viewDistance;
        // set the current meterial's friction
        var pm = gameObject.GetComponent<MeshCollider>().material;
        pm.dynamicFriction = fiction;
        pm.staticFriction = fiction * 1.2f;
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
        if (controller.isGrounded)
        {
            controller.Move(new Vector3(0, jumpHeight, 0));
        }
    }

    void Update()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
        if (playerInput.playerIndex != index)
        {
            return;
        }
        if (movement != Vector3.zero)
        {
            controller.WakeUp();
        }
        controller.Move(movement * Time.deltaTime * playerSpeed);
        controller.LookAtSomething();
    }
}
