using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    public GameObject Panel;
    public Animator anim;
    
    void Start()
    {
        Panel.SetActive(false);
    }
    

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void btnPause()
    {
        Time.timeScale = 0;
        Panel.SetActive(true);
    }

    public void btnResume()
    {
        Panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void btnBackToMenu()
    {
        Time.timeScale = 1;
        Panel.SetActive(false);
        StartCoroutine(BackToMenu());
    }

    public void btnEXIT()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    IEnumerator BackToMenu()
    {
        anim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("menu");
    }
}
