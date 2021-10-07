using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

	// Create public variables for player speed, and for the Text UI game objects
	public float speed, jumpForce;
	public GameObject pickUpParticles;
	public AudioSource pickUpSound;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;
	public Material colorWall;
	public int mapForce = 200;

	private float movementX;
	private float movementY;

	private Rigidbody rb;
	private int count;
	private int health;

	// At the start of the game..
	void Start()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Set the count to zero 
		count = 0;
		health = 3;

		SetCountText();

		// Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
		winTextObject.SetActive(false);
	}

	void Update()
	{
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);

		if (Input.GetButtonDown("Jump"))
		{
			rb.AddForce(new Vector3(0f, jumpForce, 0f));
		}

		//Added for mod
		//dash
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			rb.AddForce(movement * speed * 100);
		}
	}

    void FixedUpdate()
	{
		
		// Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);

		rb.AddForce(movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		// ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag("PickUp"))
		{
			other.gameObject.SetActive(false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText();
			pickUpSound.Play();

			GameObject particles = Instantiate(pickUpParticles, transform.position, Quaternion.identity);
			Destroy(particles, 2f); 
		}
	}

    void OnCollisionEnter(Collision collision)
    {
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);
		if (collision.gameObject.CompareTag("Wall"))
		{
			collision.gameObject.GetComponent<MeshRenderer>().material = colorWall;
			rb.AddForce(-movement*10000);
			if (collision.gameObject.GetComponent<MeshRenderer>().material = colorWall)
			{
				health -= 1;
				if (health <= 0) {
					SceneManager.LoadScene(1);
				}
			}
		}

	}

    void OnMove(InputValue value)
	{
		Vector2 v = value.Get<Vector2>();

		movementX = v.x;
		movementY = v.y;
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= 12)
		{
			// Set the text value of your 'winText'
			SceneManager.LoadScene(2);
		}
	}
}
