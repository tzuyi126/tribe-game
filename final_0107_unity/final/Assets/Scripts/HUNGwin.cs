using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUNGwin : MonoBehaviour
{
    public Animator sceneTransAnim;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "p1")
        {
            StartCoroutine(LoadScene());
            //SceneManager.LoadScene("hung_win");
        }
    }

    IEnumerator LoadScene()
    {
        sceneTransAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("hung_win");
    }
}
