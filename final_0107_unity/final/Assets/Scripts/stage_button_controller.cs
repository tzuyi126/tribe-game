using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class stage_button_controller : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    public AudioClip click;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void Start_Game()
    {
        Invoke("start",0.3f);
        audioSource.PlayOneShot(click, 1);
    }
    public void btnBack()
    {
        Invoke("back", 0.3f);
        audioSource.PlayOneShot(click, 1);
    }

    void start()
    {
        SceneManager.LoadScene("level1");
    }
    void back()
    {
        SceneManager.LoadScene("menu");
    }
}
