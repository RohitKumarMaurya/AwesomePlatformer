using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{

    public GameObject stone;
    public Transform attackInstantiate;
    private Animator anim;
    private string coroutine_name = "StartAttack";
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(coroutine_name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
        GameObject obj = Instantiate(stone, attackInstantiate.position, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300f, -700f), 0f));
    }

    void BackToIdle()
    {
        anim.Play("BossIdle");
    }
    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        anim.Play("BossAttack");
        StartCoroutine(coroutine_name);
    }
    public void DeactivateBoss()
    {
        StopCoroutine(coroutine_name);
    }
}
