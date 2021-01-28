using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickThrowController : MonoBehaviour
{
    GameObject player; // who
    Animator anim;
    CharacterController2D controller;
    Image fullCircle;
    Image emptyCircle;

    KeyCode pressKey;

    [SerializeField] private LayerMask m_WhatIsWeapon;
    [SerializeField] private Transform m_GroundCheck;
    const float k_interactRadius = 0.2f;

    public GameObject carryItem = null; // carry what
    public GameObject thrownItem = null;

    public GameObject knife;

    public AudioSource SoundEffect;
    public AudioClip throwSE;

    int throwPower = 1600;
    bool throwing = false;
    float carryCD = 0f;
    float carryCDMax;
    bool facingRight = false;

    Vector3 position_center_offset = new Vector3(0.1f, -0.1f, 0);

    void Start()
    {
        player = this.gameObject;
        anim = this.gameObject.GetComponent<Animator>();
        controller = this.gameObject.GetComponent<CharacterController2D>();
        fullCircle = this.gameObject.transform.Find("Canvas").transform.Find("Full").gameObject.GetComponent<Image>();
        emptyCircle = this.gameObject.transform.Find("Canvas").transform.Find("Empty").gameObject.GetComponent<Image>();

        if (player.gameObject.name == "p1")
        {
            //p1 use Enter
            pressKey = KeyCode.Return;
        }
        else
        {
            //p2 use Space
            pressKey = KeyCode.Space;
        }

        carryItem = this.gameObject.transform.Find("HoldPoint").transform.Find("knife").gameObject;

        throwing = false;

        carryCD = 2f;
        carryCDMax = 2f;
        fullCircle.enabled = false;
        emptyCircle.enabled = false;
        fullCircle.fillAmount = carryCD / 2f;
     
    }

    
    void FixedUpdate()
    {
        if (carryItem)
        {
            carryItem.transform.position = this.transform.Find("HoldPoint").transform.position;
            carryItem.GetComponent<CircleCollider2D>().enabled = false;
        }



        Collider2D touch = Physics2D.OverlapCircle(m_GroundCheck.position, k_interactRadius, m_WhatIsWeapon);

        if (touch)
        {
            //Debug.Log(touch.gameObject.name);
            if(Input.GetKeyDown(pressKey))
            {
                if (carryItem)
                {
                    Destroy(carryItem);
                }

                if(touch.gameObject.tag == "Bomb")
                {
                    GameObject newBomb = Instantiate(touch.gameObject, this.transform.Find("HoldPoint").transform.position, new Quaternion(0, 0, 0, 1));
                    newBomb.gameObject.name = "bomb";
                    newBomb.transform.parent = this.gameObject.transform.Find("HoldPoint").transform;

                    carryItem = newBomb.gameObject;
                }
                else
                {
                    /*
                    touch.gameObject.layer = 0;
                    carryItem = touch.gameObject;
                    carryItem.transform.parent = this.transform.Find("HoldPoint").transform;
                    carryItem.transform.position = this.transform.Find("HoldPoint").transform.position;
                    */
                }
                carryItem.gameObject.layer = 0;
                carryItem.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "CarryWeapon";
                Destroy(touch.gameObject);

            }
            
        }

        if (!touch && carryItem && !throwing && Input.GetKeyDown(pressKey))
        {
            anim.SetBool("isThrowing", true);

            if (carryItem.gameObject.tag == "Knife")
            {
                GameObject thrownKnife = Instantiate(knife, this.transform.Find("ThrowPoint").transform.position, carryItem.transform.rotation);
                thrownKnife.gameObject.GetComponent<CircleCollider2D>().enabled = true;
                thrownKnife.gameObject.name = "thrownKnife";

                thrownItem = thrownKnife.gameObject;
                thrownItem.gameObject.tag = "ThrownKnife";

                if (controller.FacingRight)
                {
                    thrownItem.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(throwPower, 0));
                }
                else
                {
                    thrownItem.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-throwPower, 0));
                }

            }
            else if(carryItem.gameObject.tag == "Bomb")
            {
                GameObject newKnife = Instantiate(knife, this.transform.Find("HoldPoint").transform.position, new Quaternion(0, 0, 0, 1));
                newKnife.gameObject.name = "knife";
                newKnife.transform.parent = this.gameObject.transform.Find("HoldPoint").transform;

                carryItem.transform.parent = null;
                thrownItem = carryItem.gameObject;
                carryItem = newKnife.gameObject;

                thrownItem.transform.position = this.transform.Find("ThrowPoint").transform.position;
                thrownItem.tag = "ThrownBomb";
                thrownItem.GetComponent<Rigidbody2D>().gravityScale = 3;



                if (controller.FacingRight)
                {
                    float angle = 45 * 2 * Mathf.PI / 360;
                    thrownItem.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(angle) * 600 + this.GetComponent<Rigidbody2D>().velocity.x, Mathf.Sin(angle) * 1200 + this.GetComponent<Rigidbody2D>().velocity.y));
                }
                else
                {
                    float angle = (180 - 45) * 2 * Mathf.PI / 360;
                    thrownItem.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(angle) * 600 + this.GetComponent<Rigidbody2D>().velocity.x, Mathf.Sin(angle) * 1200 + this.GetComponent<Rigidbody2D>().velocity.y));
                }
                
            }

            SoundEffect.PlayOneShot(throwSE);

            

            throwing = true;
            carryCD = 0f;
            fullCircle.enabled = true;
            emptyCircle.enabled = true;
       
        }
        


        
        if (throwing && carryCD < carryCDMax)
        {
            carryCD += Time.deltaTime;
            
            if(!thrownItem)
            {
                thrownItem = null;
            }
        }
        else if(throwing)
        {
            
            throwing = false;
            carryCD = carryCDMax;
            fullCircle.enabled = false;
            emptyCircle.enabled = false;
        }

        fullCircle.fillAmount = carryCD / carryCDMax;


    }
}
