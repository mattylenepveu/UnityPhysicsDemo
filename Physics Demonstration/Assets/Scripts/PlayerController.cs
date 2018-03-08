using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public float speed = 1.0f;
	public float jumpForce = 5.0f;
	//public float height = 1.0f;
	public CapsuleCollider col;
	public LayerMask groundLayers;

	private Rigidbody rb;

	// Use this for initialization
	void Awake() 
	{
		Cursor.lockState = CursorLockMode.Locked;
		rb = GetComponent<Rigidbody>();
		col = GetComponent<CapsuleCollider>();

		/*float scaleY = GetComponent<CameraController>().GetHeight();
		transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);*/
	}
	
	// Update is called once per frame
	void FixedUpdate() 
	{
		float moveX = Input.GetAxis("Horizontal") * speed;
		float moveZ = Input.GetAxis("Vertical") * speed;
		moveX *= Time.deltaTime;
		moveZ *= Time.deltaTime;

		transform.Translate(moveX, 0, moveZ);

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Cursor.lockState = CursorLockMode.None;
		}

		/*float scaleY = GetComponent<CameraController>().GetHeight();
		transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);*/
	}

	private bool IsGrounded()
	{
		return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, 
			col.bounds.min.y, col.bounds.center.z), col.radius * 0.9f, groundLayers);
	}
}
