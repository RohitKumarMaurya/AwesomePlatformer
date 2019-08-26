using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    private Animator anim;
    private int health = 10;
    private bool canDamage;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        canDamage = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(canDamage)
        {
            if (collision.tag == "Bullet")
            {
                health--;
                canDamage = false;
                if (health == 0)
                {
                    GetComponent<BossScript>().DeactivateBoss();
                    anim.Play("BossDead");
                }
                StartCoroutine(WaitForDamage());
            }
        }
    }
    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(1f);
        canDamage = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
