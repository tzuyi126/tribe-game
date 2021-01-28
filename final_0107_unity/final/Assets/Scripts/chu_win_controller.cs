using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class chu_win_controller : MonoBehaviour
{
    public GameObject smoke_particle;
    public Animator sceneTransAnim;

    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;
    private bool smoke_flag;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        smoke_flag=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.anyKey)
        {
            if(!smoke_flag)
            {
                InvokeRepeating("smoke",0,0.1f);
                smoke_flag=true;
            }
            Move(-0.5f);
        }
        else{
            Move(0);
            CancelInvoke("smoke");
            smoke_flag=false;
        }
        if(this.gameObject.transform.position.x<43){
            StartCoroutine(LoadScene());
            //SceneManager.LoadScene("menu");
        }
    }
    public void Move(float move)
    {

        
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        

    }
    void smoke()
    {
        if(smoke_particle)Instantiate(smoke_particle,transform.position- new Vector3(-1.2f,1.5f,0),Quaternion.identity);
    }

    IEnumerator LoadScene()
    {
        sceneTransAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("stage");
    }
}
