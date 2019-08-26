using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBackground : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(1, 1, 1);
        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        Vector3 tempscale = transform.localScale;
        tempscale.x = worldScreenWidth / width + 0.1f;
        tempscale.y = worldScreenHeight / height + 0.1f;

        transform.localScale = tempscale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
