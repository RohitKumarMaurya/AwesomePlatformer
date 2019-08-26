using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {
	public GameObject shot;
	public Transform shotSpawn;
    public AudioClip fire;
    private AudioSource audioManager;
    // Use this for initialization
    private void Awake()
    {
        audioManager = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
		ShootBullet ();	
	}
	void ShootBullet()
	{
		if (Input.GetMouseButtonDown(0)) {
			GameObject bullet = (GameObject)Instantiate (shot,shotSpawn.position,Quaternion.identity);
            audioManager.PlayOneShot(fire);
            bullet.GetComponent<BulletMovement>().Speed *= transform.localScale.x;
		}
	}

}
