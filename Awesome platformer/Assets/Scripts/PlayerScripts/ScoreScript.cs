using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private Text coinTextScore;
    private AudioSource collect;
    private GameObject coin;
    private int scoreCount = 0;
    private void Update()
    {
        coin = GameObject.FindGameObjectWithTag("Coin");
        collect = coin.GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {                       
            scoreCount++;
            coinTextScore.text = "X" + scoreCount.ToString();
            collect.Play();
            Destroy(collision.gameObject);
        }
    }
    public void BonusScore()
    {
        scoreCount +=3;
        coinTextScore.text = "X" + scoreCount.ToString();
        collect.Play();
    }
}
