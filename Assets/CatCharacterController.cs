using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;

public class CatCharacterController : MonoBehaviour
{
    public float rotationSpeed { get; set; }

    public float WakeUpSpeed { get; set; }

    public float maxSpeed { get; set; }

    public float fov { get; set; }

    public float viewDistance { get; set; }

    private GameObject target;

    private AimConstraint aimConstraint;

    private Coroutine currentTransition;
    // set the current gameobject to the player

    private GameObject player;

    private ConstraintSource defaultSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // get the current gameobject
        player = this.gameObject;
        aimConstraint = player.GetComponentsInChildren<AimConstraint>()[0];
        Transform headLight = null;
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag("📷"))
            {
                headLight = child;
                break;
            }
        }
        var cons = new ConstraintSource();
        cons.sourceTransform = headLight;
        cons.weight = 1.0f;
        defaultSource = cons;
        aimConstraint.AddSource(cons);
        var temp = new ConstraintSource();
        temp.sourceTransform = headLight;
        temp.weight = 0.0f;
        aimConstraint.AddSource(temp);
    }

    // Update is called once per frame
    void Update()
    {
        // get the time between frames
        var deltaTime = Time.deltaTime;
        // if the player is not grounded,  add gravity
        if (!isGrounded)
        {
            player.GetComponent<Rigidbody>().linearVelocity += new Vector3(0, -9.8f, 0) * deltaTime;
        }

    }
    GameObject[] FindNearestObjects(Vector3 position, string tag, Vector3 sight, float fov)
    {
        // Find all objects with the given tag
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag(tag);
        var normSight = Vector3.Normalize(sight);
        fov *= Mathf.Deg2Rad;
        // Sort objects by distance to the given position
        var sortedObjects = allObjects
            .Where(obj => Vector3.Distance(obj.transform.position, position) < viewDistance &&
                 Mathf.Acos(Vector3.Dot(Vector3.Normalize(obj.transform.position - position), normSight)) < fov / 2)
            .OrderBy(obj => Vector3.Distance(position, obj.transform.position))
            .ToArray();  // Convert to array

        return sortedObjects;
    }

    public void LookAtSomething()
    {
        Transform head = null, headLight = null;
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag("📷"))
            {
                headLight = child;
                break;
            }
            if (child.CompareTag("💀"))
            {
                head = child;
            }
        }

        var zFov = fov * 0.75f;

        var nearest = FindNearestObjects(head.position, "🍩", headLight.position - head.position, fov).FirstOrDefault();

        if (target != null && nearest == target)
        {
            return;
        }
        else if (target != nearest)
        {
            if (nearest == null)
            {
                target = null;
                ChangeAimTarget(headLight.transform);
            }
            else
            {
                target = nearest;
                ChangeAimTarget(target.transform);
            }
        }

        return;

    }

    IEnumerator SmoothChangeSource(Transform newSource, float duration)
    {
        var sources = new List<ConstraintSource>();
        aimConstraint.GetSources(sources);
        var index = 0;
        var newIndex = 1;
        // Ensure the index is valid
        if (index < 0 || index >= sources.Count)
        {
            yield break; // Exit if the index is out of range
        }

        // Get the current source and prepare the new source
        ConstraintSource oldSource = sources[index];
        ConstraintSource newConstraintSource = new ConstraintSource
        {
            sourceTransform = newSource,
            weight = 0f // Start with weight 0 for the new source
        };

        sources[newIndex] = newConstraintSource;

        // Apply the new source with weight 0 initially
        aimConstraint.SetSources(sources);

        float elapsedTime = 0f;

        // Smoothly interpolate the weights over time
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Gradually decrease the old source's weight and increase the new source's weight
            oldSource.weight = Mathf.Lerp(1.0f, 0.0f, t); // Old source weight decreases
            newConstraintSource.weight = Mathf.Lerp(0.0f, 1.0f, t); // New source weight increases

            // Update the sources with the new weights
            sources[index] = oldSource;
            sources[newIndex] = newConstraintSource;
            aimConstraint.SetSources(sources);

            yield return null; // Wait for the next frame
        }

        // Ensure final weights are exactly 0 and 1 at the end of the transition
        oldSource.weight = 0f;
        newConstraintSource.weight = 1f;
        sources[index] = newConstraintSource;
        sources[newIndex] = oldSource;
        aimConstraint.SetSources(sources);

        // Clear the current coroutine reference after completion
        currentTransition = null;
    }

    void ChangeAimTarget(Transform newTarget)
    {
        if (aimConstraint != null && newTarget != null)
        {
            // If there is a running transition, stop it
            if (currentTransition != null)
            {
                return;
                StopCoroutine(currentTransition);
            }

            // Start the new transition and keep track of the coroutine
            currentTransition = StartCoroutine(SmoothChangeSource(newTarget, 0.5f));
        }

    }


    // Move
    public void Move(Vector3 motion)
    {
        if (!isGrounded)
        {
            motion *= 0.3f;
        }
        // move the player
        // use add force to move the player
        //player.GetComponent<Rigidbody>().AddForce(motion * 500);
        var rigid = player.GetComponent<Rigidbody>();
        var x = rigid.linearVelocity.x;
        var z = rigid.linearVelocity.z;
        var y = rigid.linearVelocity.y;
        var speed = math.sqrt(x * x + z * z);
        if (speed > maxSpeed)
        {
            rigid.linearVelocity = new Vector3(x / speed * maxSpeed, y, z / speed * maxSpeed);
        }
        else
        {
            rigid.linearVelocity += motion * 10;
        }

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
