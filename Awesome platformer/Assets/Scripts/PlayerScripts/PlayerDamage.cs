using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerDamage : MonoBehaviour
{
    private Text lifeText;
    private int lifeScoreCount;
    private bool canDamage;
    private AudioSource audioManager;
    private void Awake()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeScoreCount = 3;
        lifeText.text = "X" + lifeScoreCount.ToString();
        audioManager = gameObject.GetComponent<AudioSource>();
        canDamage = true;
    }
    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void DealDamage()
    {
        if (canDamage)
        {
            lifeScoreCount--;
            if (lifeScoreCount > 0)
            {
                lifeText.text = "X" + lifeScoreCount.ToString();
            }
            if (lifeScoreCount < 0)
            {
                audioManager.Play();
                Time.timeScale = 0f;
                StartCoroutine(RestartGame());
            }
            canDamage = false;
            StartCoroutine(WaitForDamage());
        }
    }
    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }
    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("GamePlay");
    }
}
