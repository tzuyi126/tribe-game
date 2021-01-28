using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject explode;
    public GameObject hit;
    public GameObject green_blood;
    public GameObject red_blood;
    public AudioSource SoundEffect;
    public AudioClip explode_sound;

    private GameObject p1;
    private GameObject p2;

    private void Awake()
    {
        p1 = GameObject.Find("p1");
        p2 = GameObject.Find("p2");
        SoundEffect = GameObject.Find("SoundEffect").GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(this.tag=="ThrownBomb")
        {
            Instantiate(explode, transform.position, Quaternion.identity);
            SoundEffect.PlayOneShot(explode_sound);
            float dist1 = Vector3.Distance(this.transform.position, p1.transform.position);
            float dist2 = Vector3.Distance(this.transform.position, p2.transform.position);
            if (dist1 < 2.5f)
            {
                p1.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                p1.gameObject.GetComponent<PlayerMovement>().enabled = false;
                p1.gameObject.GetComponent<PickThrowController>().enabled = false;

                p1.gameObject.GetComponent<CharacterController2D>().dead = true;

                if(CameraController.p2_alive)
                    CameraController.Dominance = 2;
                else
                {
                    CameraController.Dominance = 3;
                }
                CameraController.p1_alive = false;
                Instantiate(green_blood, p1.gameObject.transform.position, Quaternion.identity);
            }
            if (dist2 < 2.5f)
            {
                p2.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                p2.gameObject.GetComponent<PlayerMovement>().enabled = false;
                p2.gameObject.GetComponent<PickThrowController>().enabled = false;

                p2.gameObject.GetComponent<CharacterController2D>().dead = true;


                if(CameraController.p1_alive)
                    CameraController.Dominance = 1;
                else
                {
                    CameraController.Dominance = 3;
                }
                CameraController.p2_alive = false;
                Instantiate(red_blood, p2.gameObject.transform.position, Quaternion.identity);
            }

            if (col.gameObject.layer != 9)
            {
                Destroy(gameObject);
            }
        }
        if(this.tag=="ThrownKnife")
        {
            Instantiate(hit, transform.position, Quaternion.identity);
            if (col.gameObject.tag == "Player")
            {
                col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                col.gameObject.GetComponent<PlayerMovement>().enabled = false;
                col.gameObject.GetComponent<PickThrowController>().enabled = false;

                col.gameObject.GetComponent<CharacterController2D>().dead = true;
                

                if (col.gameObject.name == "p1")
                {
                    if(CameraController.p2_alive)
                        CameraController.Dominance = 2;
                    else
                    {
                        CameraController.Dominance = 3;
                    }
                    CameraController.p1_alive = false;
                    Instantiate(green_blood, col.gameObject.transform.position, Quaternion.identity);
                }
                else if (col.gameObject.name == "p2")
                {
                    if(CameraController.p1_alive)
                        CameraController.Dominance = 1;
                    else
                    {
                        CameraController.Dominance = 3;
                    }
                    CameraController.p2_alive = false;
                    Instantiate(red_blood, col.gameObject.transform.position, Quaternion.identity);
                }
            }
            
            if(col.gameObject.layer != 9)
            {
                Destroy(gameObject);
            }
            
        }
    }
}
