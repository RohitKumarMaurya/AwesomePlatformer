using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {
	private float speed = 10f;
    private Animator anim;
    private bool canMove;
    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Use this for initialization
    void Start () {
        canMove = true;
		StartCoroutine (DestroyBullet(1f));
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}
	void Move()
	{
        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
        }
	}
	IEnumerator DestroyBullet(float timer)
	{
		yield return new WaitForSeconds (timer);
		Destroy (gameObject);
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Beetle" || collision.gameObject.tag =="Snail" || collision.gameObject.tag == "Spider" || collision.gameObject.tag == "Boss")
        {
            anim.Play("Explode");
            canMove = false;
            StartCoroutine(DestroyBullet(timer: 0.3f));
        }
    }
}
