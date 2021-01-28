using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_button_controller : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip click;
    public GameObject Music;

    void Start()
    {
        
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
        Music.GetComponent<AudioSource>().PlayOneShot(click, 1);
    }
    void start()
    {
        SceneManager.LoadScene("Stage");
    }
    public void Exit_Game()
    {
        Invoke("exit",0.3f);
        Music.GetComponent<AudioSource>().PlayOneShot(click, 1);
    }
    void exit()
    {
        Application.Quit();
    }
}
