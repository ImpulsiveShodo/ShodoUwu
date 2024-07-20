using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CatCharacterController : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 0.02f;

    [SerializeField]
    private float WakeUpSpeed = 0.01f;
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
        // get the time between frames
        var deltaTime = Time.deltaTime;
        // if the player is not grounded, add gravity
        Debug.Log("isGrounded: " + isGrounded + " velocity: " + player.GetComponent<Rigidbody>().linearVelocity + " deltaTime: " + deltaTime);
        if (!isGrounded)
        {
            player.GetComponent<Rigidbody>().linearVelocity += new Vector3(0, -9.8f, 0) * deltaTime;
        }
        //Debug.Log(player.GetComponent<Rigidbody>().linearVelocity);
    }

    // Move
    public void Move(Vector3 motion)
    {
        if (!isGrounded)
        {
            motion *= 0.3f;
        }
        // move the player
        //Debug.Log(motion);
        // use add force to move the player
        //player.GetComponent<Rigidbody>().AddForce(motion * 500);
        var rigid = player.GetComponent<Rigidbody>();
        //Debug.Log(rigid.linearVelocity);
        rigid.linearVelocity += motion * 10;
        // rotate y axis a little bit


        if (motion == Vector3.zero)
        {
            return;
        }
        // rotate toward the direction of the motion
        var targetRotation = Quaternion.LookRotation(motion);
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, rotationSpeed);

    }

    public void WakeUp()
    {
        // rotate x and z axis toward 0
        var currRotation = player.transform.rotation;
        // change to euler angles
        var currEuler = currRotation.eulerAngles;
        if (currEuler.x > 180)
        {
            currEuler.x -= 360;
        }

        if (currEuler.z > 180)
        {
            currEuler.z -= 360;
        }
        var abs_x = math.abs(currEuler.x);
        var abs_z = math.abs(currEuler.z);

        if (abs_x > 0.1f)
        {
            currEuler.x = currEuler.x / (1 + (180 - abs_x) * WakeUpSpeed);
        }

        if (abs_z > 0.1f)
        {
            currEuler.z = currEuler.z / (1 + (180 - abs_z) * WakeUpSpeed);
        }

        // set the euler angles back to the rotation
        currRotation.eulerAngles = currEuler;

        // set the rotation to the current rotation
        player.transform.rotation = currRotation;
    }

    // get children's collider collision event
    public void OnCollisionEnter(Collision collision)
    {
        //bool flag = false;
        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    if (contact.thisCollider.CompareTag("🐾"))
        //    {
        //        Debug.Log("🐾 enter");
        //        flag = true;
        //    }

        //}
        //isGrounded |= flag;
    }

    public void OnCollisionExit(Collision collision)
    {
        //ContactPoint[] contacts = new ContactPoint[collision.contactCount];
        //collision.GetContacts(contacts);
        //Debug.Log(contacts.Length);
        //foreach (ContactPoint contact in contacts)
        //{
        //    Debug.Log(contact.thisCollider.tag + " exit");
        //    if (contact.thisCollider.CompareTag("🐾"))
        //    {
        //        isGrounded = false;
        //    }
        //}
    }




    public bool isGrounded
    {
        get
        {
            // find all children with tag "🐾"
            List<Transform> children = new();
            foreach (Transform child in transform.GetComponentsInChildren<Transform>())
            {
                if (child.CompareTag("🐾"))
                {
                    children.Add(child);
                }
            }
            return children.Exists(child => Physics.Raycast(child.position, -Vector3.up, 0.3f));
        }
        set
        {
            isGrounded = value;
        }
    }
}
