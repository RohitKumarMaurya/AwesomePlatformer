using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour {
    private Rigidbody2D myBody;
    private Animator anim;
    private Vector3 moveDirection = Vector3.left;
    private Vector3 originPosition;
    private float speed = 3f;
    public GameObject birdEgg;
    private Vector3 movePosition;
    public LayerMask playerLayer;
    private bool attacked = false;

    private bool canMove;
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        originPosition = transform.position;
        originPosition.x += 6f;

        movePosition = transform.position;
        movePosition.x -= 6f;

        canMove = true;
	}
	
	// Update is called once per frame
	void Update () {
        moveTheBird();
        dropTheEgg();
	}

    void moveTheBird()
    {
        if(canMove)
        {
            transform.Translate(moveDirection * speed * Time.smoothDeltaTime);
            if(transform.position.x >= originPosition.x)
            {
                moveDirection = Vector3.left;
                changeDirection(0.5f);
            }
            else if (transform.position.x <= movePosition.x)
            {
                moveDirection = Vector3.right;
                changeDirection(-0.5f);
            }
        }
    }
    void changeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }
    void dropTheEgg(){
        if(!attacked)
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer))
            {
                Instantiate(birdEgg, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
                attacked = true;
                anim.Play("BirdFly");
            }
        }
    }
    IEnumerator BirdDead()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            anim.Play("BirdDead");

            GetComponent<BoxCollider2D>().isTrigger = true;
            myBody.bodyType = RigidbodyType2D.Dynamic;
            canMove = false;
            StartCoroutine(BirdDead());
        }
    }
}
