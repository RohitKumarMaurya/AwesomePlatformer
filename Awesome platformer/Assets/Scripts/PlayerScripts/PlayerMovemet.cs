using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerMovemet : MonoBehaviour {
	public float speed;
	private Rigidbody2D myBody;
	private Animator myAnimation;
	public Transform GroundCheckPosition;
	public LayerMask groundLayer;
	private bool isGrounded;
    private bool drowned;
	private bool jumped;
	private float jumpPower = 15f;
    private AudioSource audioManager;
    public AudioClip jump;
	void Awake () {
		myBody = GetComponent<Rigidbody2D> ();
		myAnimation = GetComponent<Animator> ();
        audioManager = gameObject.GetComponent<AudioSource>();
	}
	void Start () {
    }
	// Update is called once per frame
	void Update () {
        CheckIfGrounded ();
		PlayerJump ();        
	}
	void FixedUpdate () {
		PlayerMovement ();
	}
	void PlayerMovement()
	{
		float movement = Input.GetAxisRaw ("Horizontal");
		if (movement < 0) {
			myBody.velocity = new Vector2 (-speed, myBody.velocity.y);
			ChangeDirection (-1);
		} else if (movement > 0) {
			myBody.velocity = new Vector2 (speed, myBody.velocity.y);
			ChangeDirection (1);
		} else {
			myBody.velocity = new Vector2 (0f, myBody.velocity.y);
		}
		myAnimation.SetInteger ("Speed",Mathf.Abs ((int)myBody.velocity.x));
	}
	void ChangeDirection(int Direction) {
		Vector3 temp = myBody.transform.localScale;
		temp.x = Direction;
		myBody.transform.localScale = temp;
	}
	void CheckIfGrounded (){
		isGrounded = Physics2D.Raycast (GroundCheckPosition.position,Vector2.down,0.1f,groundLayer);
		if (isGrounded) {
			if (jumped) {
				jumped = false;
				myAnimation.SetBool ("Jump",false);
			}
		}
	}
    void PlayerJump () {
		if(isGrounded){
			if (Input.GetKey (KeyCode.Space)) {
                audioManager.PlayOneShot(jump);
				jumped = true;
				myBody.velocity = new Vector2 (myBody.velocity.x, jumpPower);
				myAnimation.SetBool ("Jump",true);
			}
		}
	}
}
