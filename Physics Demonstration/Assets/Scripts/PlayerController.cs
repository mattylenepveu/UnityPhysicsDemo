using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class PlayerController : MonoBehaviour 
{
	public float regSpeed = 10.0f;
    public float maxSpeed = 15.0f;
	public float jumpForce = 5.0f;
    public float launchThreshold = 15000.0f;
	public float crouchHeight = 0.5f;
	public CapsuleCollider col;
	public LayerMask groundLayers;

    private float speed;
    private float launchForce;
    private bool hasBox;
    private bool crouching;
    private float standHeight;
    private float yOffset;
	private Rigidbody rb;
	private GameObject pickUp = null;

	// Use this for initialization
	void Awake() 
	{
        speed = regSpeed;
        launchForce = 5000.0f;
        hasBox = false;
		Cursor.lockState = CursorLockMode.Locked;
		rb = GetComponent<Rigidbody>();
		col = GetComponent<CapsuleCollider>();
        standHeight = transform.localScale.y;
        crouching = false;
        yOffset = standHeight - crouchHeight;
	}
	
	// Update is called once per frame
	void FixedUpdate() 
	{
        int leftMouse = 0;
		float moveX = Input.GetAxis("Horizontal") * speed;
		float moveZ = Input.GetAxis("Vertical") * speed;
		moveX *= Time.deltaTime;
		moveZ *= Time.deltaTime;

		transform.Translate(moveX, 0, moveZ);

		if (Input.GetKeyDown(KeyCode.Space)) 
		{
			if (IsGrounded()) 
			{
                //Debug.Log("Grounded");
				rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			Cursor.lockState = CursorLockMode.None;
		}

        if (Input.GetMouseButtonDown(leftMouse)) 
		{
            hasBox = false;

            if (pickUp == null) 
			{
                if (!crouching)
                {
                    int layerMask = 1 << LayerMask.NameToLayer("Pick Up");
                    Vector3 boxSize = new Vector3(2, 2, 2);
                    Collider[] boxPickUp = Physics.OverlapBox(transform.position + transform.forward,
                                               boxSize * 0.5f, transform.rotation, layerMask);

                    if (boxPickUp.Length > 0)
                    {
                        pickUp = boxPickUp[0].gameObject;
                        pickUp.transform.parent = transform;
                        Rigidbody boxRb = pickUp.GetComponent<Rigidbody>();
                        boxRb.isKinematic = true;
                        pickUp.transform.localPosition = new Vector3(0, 1.5f, 1);
                        pickUp.transform.localRotation = Quaternion.identity;
                    }
                }
			} 

			else 
			{
                launchForce += 1000.0f;
                hasBox = true;

                if (launchForce > launchThreshold)
                {
                    launchForce = launchThreshold;
                }
			}
		}

        if (Input.GetMouseButtonUp(leftMouse))
        {
            if (hasBox)
            {
                pickUp.transform.parent = null;
                Rigidbody boxRb = pickUp.GetComponent<Rigidbody>();
                boxRb.isKinematic = false;
                boxRb.AddForce(transform.forward * launchForce);
                launchForce = 5000.0f;
                pickUp = null;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!crouching)
            {
                Vector3 scale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);
                transform.localScale = scale;
                crouching = true;
                transform.position -= Vector3.up * yOffset;
            }
            else
            {
                Vector3 up = transform.TransformDirection(Vector3.up);

                if (!Physics.Raycast(transform.position, up))
                {
                    Vector3 scale = new Vector3(transform.localScale.x, standHeight, transform.localScale.z);
                    transform.localScale = scale;
                    crouching = false;
                    transform.position += Vector3.up * yOffset;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && IsGrounded())
        {
            speed = maxSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = regSpeed;
        }
    }

    private bool IsGrounded()
	{
        return Physics.Raycast(transform.position, Vector3.down, 1.5f);
	}
}
