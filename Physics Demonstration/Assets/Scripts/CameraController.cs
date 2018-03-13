using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;
	//public float initialHeight = 1.0f;
	//public float crouchHeight = 0.5f;

	private GameObject character;
	private Vector2 mouseLook;
	private Vector2 smoothV;
	//private bool crouching;
	//private float height;
    //private float heightDiff;

	// Use this for initialization
	void Awake() 
	{
		character = transform.parent.gameObject;
		//crouching = false;
		//height = initialHeight;
        //heightDiff = initialHeight - crouchHeight;
    }
	
	// Update is called once per frame
	void FixedUpdate() 
	{
		// Mouse delta
		var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		md = Vector2.Scale (md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
		smoothV.x = Mathf.Lerp (smoothV.x, md.x, 1.0f / smoothing);
		smoothV.y = Mathf.Lerp (smoothV.y, md.y, 1.0f / smoothing);
		mouseLook += smoothV;
		mouseLook.y = Mathf.Clamp (mouseLook.y, -60.0f, 60.0f);

		transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
		character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

		//if (Input.GetKeyDown(KeyCode.LeftControl)) 
		//{
		//	if (crouching) 
		//	{
		//		crouching = false;
		//	} 
		//	else 
		//	{
		//		crouching = true;
		//	}
		//}

		//if (crouching) 
		//{
		//	height = crouchHeight;
		//	transform.position = new Vector3(transform.position.x, crouchHeight, 
		//		transform.position.z);

		//} 
		//else 
		//{
		//	height = initialHeight;
		//	transform.position = new Vector3(transform.position.x, initialHeight, 
		//		transform.position.z);
		//}
	}

    //public float GetHeight()
    //{
    //    return height;
    //}
}
