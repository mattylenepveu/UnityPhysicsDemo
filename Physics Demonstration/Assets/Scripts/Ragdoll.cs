using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------
// Obtained from: https://aie.instructure.com/courses/41/pages/resources#ragdoll
//--------------------------------------------------------------------------------

// GameObject of which the script is attached to requires an animator
[RequireComponent(typeof(Animator))]

// Creates a class for the Ragdoll script
public class Ragdoll : MonoBehaviour 
{
	// Creates an animator variable for script to use and intilises it to false
    private Animator animator = null;

	// Creates a list of rigidbodies that the script can use
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();

	//--------------------------------------------------------------------------------
	// Function used to switch the Ragdoll animator on (and has a getter and setter)
	//
	// Return:
	// 		Returns the opposite of if the animator is enabled
	//--------------------------------------------------------------------------------
    public bool RagdollOn 
	{
		// Getter returns the opposite of if the animator is enabled
        get { return !animator.enabled; }

		// Sets all ragdoll joints kinematics and animator to not equal a value
        set
        {
            animator.enabled = !value;

            foreach (Rigidbody r in rigidbodies)
                r.isKinematic = !value;
        }
    }

	//--------------------------------------------------------------------------------
	// Function is called just before the update function.
	//--------------------------------------------------------------------------------
	void Start () 
	{
		// Gets the animator component of the Ragdoll
        animator = GetComponent<Animator>();

		// Sets all joints of the ragdoll to be true
        foreach (Rigidbody r in rigidbodies)
            r.isKinematic = true;
	}
	
	//--------------------------------------------------------------------------------
	// Function is called once every frame (Not being used).
	//--------------------------------------------------------------------------------
	void Update () {}   
}
