using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------
// Obtained from: https://aie.instructure.com/courses/41/pages/resources#ragdoll
//--------------------------------------------------------------------------------

// Creates a class for the RagdollTrigger script
public class RagdollTrigger : MonoBehaviour 
{
	//--------------------------------------------------------------------------------
	// Function is called just before the update function (Not being used).
	//--------------------------------------------------------------------------------
	void Start () {}
	
	//--------------------------------------------------------------------------------
	// Function is called once every frame (Not being used).
	//--------------------------------------------------------------------------------
	void Update () {}

	//--------------------------------------------------------------------------------
	// Function runs when something enters a trigger
	//
	// Param:
	// 		other: Refers to the collider that has entered the trigger
	//--------------------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
		// Gets the ragdoll component of the colliding object
        Ragdoll ragdoll = other.gameObject.GetComponentInParent<Ragdoll>();

		// Sets Ragdoll to be on if the ragdoll component is not null
        if (ragdoll != null)
            ragdoll.RagdollOn = true;
    }
}
