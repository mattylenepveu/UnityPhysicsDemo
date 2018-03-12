﻿using System.Collections;
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
	private GameObject pickUp = null;

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
			if (IsGrounded()) 
			{
				rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			}
		}

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Cursor.lockState = CursorLockMode.None;
		}

		/*float scaleY = GetComponent<CameraController>().GetHeight();
		transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);*/

		if (Input.GetMouseButtonDown(0)) 
		{
			if (pickUp == null) 
			{
				int layerMask = 1 << LayerMask.NameToLayer ("Pick Up");
				Vector3 boxSize = new Vector3 (2, 2, 2);
				Collider[] boxPickUp = Physics.OverlapBox (transform.position + transform.forward, 
					                       boxSize * 0.5f, transform.rotation, layerMask);

				if (boxPickUp.Length > 0) 
				{
					pickUp = boxPickUp [0].gameObject;
					pickUp.transform.parent = transform;
					Rigidbody boxRb = pickUp.GetComponent<Rigidbody>();
					boxRb.isKinematic = true;
					pickUp.transform.localPosition = Vector3.forward + Vector3.up;
					pickUp.transform.localRotation = Quaternion.identity;
					BoxCollider bc = pickUp.GetComponent<BoxCollider>();
					bc.enabled = false;
				}
			} 

			else 
			{
				pickUp.transform.parent = null;
				Rigidbody boxRb = pickUp.GetComponent<Rigidbody>();
				boxRb.isKinematic = false;
				BoxCollider bc = pickUp.GetComponent<BoxCollider>();
				bc.enabled = true; 
				pickUp = null;
			}
		}
	}

	private bool IsGrounded()
	{
		return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, 
			col.bounds.min.y, col.bounds.center.z), col.radius * 0.9f, groundLayers);
	}
}
