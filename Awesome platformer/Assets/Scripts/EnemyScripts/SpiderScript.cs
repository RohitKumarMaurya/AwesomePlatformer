using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour {

    private Animator anim;
    private Rigidbody2D myBody;
    private Vector3 moveDirection = Vector3.down;
    private string coroutineName = "ChangeMovement";
    private void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start () {
        StartCoroutine(ChangeMovement());
	}
	
	// Update is called once per frame
	void Update () {
        moveSpider();
	}
    void moveSpider()
    {
        transform.Translate(moveDirection * Time.smoothDeltaTime);
    }
    IEnumerator ChangeMovement()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        if (moveDirection == Vector3.down)
            moveDirection = Vector3.up;
        else
            moveDirection = Vector3.down;
        StartCoroutine(coroutineName);
    }
    IEnumerator SpiderDead()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            anim.Play("SpiderDead");
            myBody.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(SpiderDead());
            StopCoroutine(coroutineName);
        }
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerDamage>().DealDamage();
        }
    }
}
