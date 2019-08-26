using UnityEngine;
using System.Collections;

public class SnailScript : MonoBehaviour {
	public float speed = 1f;
	private Rigidbody2D myBody;
	private Animator myAnimation;
	private bool moveLeft,canMove,stunned;
	public LayerMask playerlayer,groundlayer;
	public Transform leftCollision,rightCollision,topCollision,downCollision;
	void Awake (){
		myBody = GetComponent<Rigidbody2D> ();
		myAnimation = GetComponent<Animator> (); 
	}
	// Use this for initialization
	void Start () {
		moveLeft = true;
		canMove = true;
		stunned = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (canMove) {
			if (moveLeft) {
				myBody.velocity = new Vector2 (-speed, myBody.velocity.y);
			} else {
				myBody.velocity = new Vector2 (speed, myBody.velocity.y);
			}
		}
		CheckCollision ();
	}
	void CheckCollision ()
	{
		RaycastHit2D leftHit = Physics2D.Raycast (leftCollision.position,Vector2.left,0.1f,playerlayer); 
		RaycastHit2D rightHit = Physics2D.Raycast (rightCollision.position,Vector2.right,0.1f,playerlayer);
		Collider2D topHit = Physics2D.OverlapCircle (topCollision.position,0.1f,playerlayer);
		if (!Physics2D.Raycast (downCollision.position, Vector2.down, 0.1f,groundlayer)) {
			ChangeDirection (); 
		}
		if(topHit != null){
			if (topHit.gameObject.tag == "Player") {
				if (!stunned) {
					topHit.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x,12f);
					canMove = false;
					myBody.velocity = new Vector2 (0,0);
					stunned = true;
					if (myBody.tag == "Snail") {
						myAnimation.Play ("SnailStunned");
					}
					if (myBody.tag == "Beetle") {
						myAnimation.Play ("BeetleStunned");
						StartCoroutine (Dead(0.3f));
					}
				} 
			}
		}
		if (leftHit) {
			if (leftHit.collider.gameObject.tag == "Player") {
				if (!stunned) {
                    leftHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
				} else {
                    if(myBody.tag != "Beetle")
                    {
                        myBody.velocity = new Vector2(15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
				}
			}
		}

		if (rightHit) {
			if (rightHit.collider.gameObject.tag == "Player") {
				if (!stunned) {
                    rightHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                } else {
                    if (myBody.tag != "Beetle")
                    {
                        myBody.velocity = new Vector2(-15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                } 
			}
		}
	}
	void ChangeDirection() {
		moveLeft = !moveLeft;
		Vector3 temp = myBody.transform.localScale;
		if (moveLeft) {
			temp.x = Mathf.Abs (temp.x);
		} else {
			temp.x = -Mathf.Abs (temp.x);
		}
		myBody.transform.localScale = temp;
	}
	IEnumerator Dead (float timer)
	{
		yield return new WaitForSeconds (timer);
		Destroy (gameObject);
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            if(tag == "Beetle")
            {
                myAnimation.Play("BeetleStunned");
                canMove = false;
                myBody.velocity = new Vector2(0, 0);
                StartCoroutine(Dead(0.4f));
            }
            if(tag == "Snail")
            {
                if (!stunned)
                {
                    myAnimation.Play("SnailStunned");
                    stunned = true;
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                    StartCoroutine(Dead(0.4f));
                }
                else
                {
                    Destroy(obj: gameObject);
                }
            }
        }
    }
}
