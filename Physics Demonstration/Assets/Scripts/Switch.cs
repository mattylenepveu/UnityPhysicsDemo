using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private GameObject[] farBoxes;

	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {	
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (farBoxes == null)
            {
                farBoxes = GameObject.FindGameObjectsWithTag("FarBox");
            }

            foreach (GameObject boxes in farBoxes)
            {
                Vector3 upForce = new Vector3(Random.Range(-10.0f, 10.0f), 10, Random.Range(-10.0f, 10.0f));
                Rigidbody boxRb = boxes.GetComponent<Rigidbody>();
                boxRb.AddForce(upForce * 500.0f);
            }
        }
    }
}
