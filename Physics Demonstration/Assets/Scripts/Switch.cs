using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a class for the Switch script
public class Switch : MonoBehaviour
{
	// Public float inidicates the extra force to apply to boxes when switch is hit
	public float extraForce = 300.0f;

	// GameObject array used to store boxes' which force will be applied to
    private GameObject[] farBoxes;

	//--------------------------------------------------------------------------------
	// Function is called when the script is run first (Not being used).
	//--------------------------------------------------------------------------------
	void Awake() {}
	
	//--------------------------------------------------------------------------------
	// Function is called once every frame (Not being used).
	//--------------------------------------------------------------------------------
	void Update() {}

	//--------------------------------------------------------------------------------
	// Function runs when something enters a trigger
	//
	// Param:
	// 		other: Refers to the collider that has entered the trigger
	//--------------------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
		// Checks if the collider had a tag of "Player" and belonged to the player
        if (other.gameObject.tag == "Player")
        {
			// Checks if the farBoxes array is empty
            if (farBoxes == null)
            {
				// Finds objects with a "FarBox" tag and stores in array
                farBoxes = GameObject.FindGameObjectsWithTag("FarBox");
            }

			// Cycles through each box in the farBoxes array
            foreach (GameObject boxes in farBoxes)
            {
				// Adds a random upForce with x and z values between -10 and 10
                Vector3 upForce = new Vector3(Random.Range(-10.0f, 10.0f), 10, Random.Range(-10.0f, 10.0f));

				// Gets each boxes' rigidbody
                Rigidbody boxRb = boxes.GetComponent<Rigidbody>();

				// Applies the upForce to each box multiplied by extra force
				boxRb.AddForce(upForce * extraForce);
            }
        }
    }
}
