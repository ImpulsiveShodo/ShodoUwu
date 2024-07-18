using UnityEngine;

public class CatCharacterController : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 100;
    // set the current gameobject to the player

    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // get the current gameobject
        player = this.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
    }

    // Move
    public void Move(Vector3 motion)
    {
        // move the player
        //Debug.Log(motion);
        // use add force to move the player
        //player.GetComponent<Rigidbody>().AddForce(motion * 500);
        player.GetComponent<Rigidbody>().linearVelocity += motion * 10;
        // rotate y axis a little bit
        player.transform.Rotate(0, motion.x * rotationSpeed, 0);


        // rotate x and z axis toward 0
        var currRotation = player.transform.rotation;
        // change to euler angles
        var currEuler = currRotation.eulerAngles;
        currEuler.x /= 1.2f;
        currEuler.z /= 1.2f;
        // set the euler angles back to the rotation
        currRotation.eulerAngles = currEuler;

        // set the rotation to the current rotation
        player.transform.rotation = currRotation;


    }

    // get children's collider collision event
    public void OnCollisionEnter(Collision collision)
    {
        bool flag = false;
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.thisCollider.CompareTag("🐾"))
            {
                flag = true;
            }
        }
        isGrounded = flag;
    }


    public bool isGrounded { get; set; }

}
