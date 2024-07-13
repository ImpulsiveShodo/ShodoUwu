using UnityEngine;

public class CatCharacterController : MonoBehaviour
{

    // set the current gameobject to the player
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // get the current gameobject
        player = this.gameObject;
        isGrounded = true;

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
        player.GetComponent<Rigidbody>().linearVelocity = motion * 1000;
        Debug.Log(player.GetComponent<Rigidbody>().linearVelocity);

    }

    public bool isGrounded { get; set; }

}
