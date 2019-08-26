using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Drowned : MonoBehaviour
{
    private AudioSource audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            audioManager.Play();
            Time.timeScale = 0f;
            StartCoroutine(RestartGame());
        }
    }
    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("GamePlay");
    }
}
