using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb_number_controller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bomb;
    
    private GameObject[] now_bomb;

    void Start()
    {
        InvokeRepeating("gen_bomb",0,5.0f);
        now_bomb=new GameObject[9];
        for(int i=0;i<9;i++)
            now_bomb[i]=null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void gen_bomb()
    {
        if(now_bomb[0]==null)
        {
            now_bomb[0]=Instantiate(bomb, new Vector3(-50.0f,-2.98f,0), Quaternion.identity);
        }
        if(now_bomb[1]==null)
        {
            now_bomb[1]=Instantiate(bomb, new Vector3(-40.0f,-2.98f,0), Quaternion.identity);
            //now_bomb[1]=true;
        }
        if(now_bomb[2]==null)
        {
            now_bomb[2]=Instantiate(bomb, new Vector3(-30.0f,-2.98f,0), Quaternion.identity);
            //now_bomb[2]=true;
        }
        if(now_bomb[3]==null)
        {
            now_bomb[3]=Instantiate(bomb, new Vector3(-14.0f,-2.98f,0), Quaternion.identity);
            //now_bomb[3]=true;
        }
        if(now_bomb[4]==null)
        {
            now_bomb[4]=Instantiate(bomb, new Vector3(0,-2.98f,0), Quaternion.identity);
            //now_bomb[4]=true;
        }
        if(now_bomb[5]==null)
        {
            now_bomb[5]=Instantiate(bomb, new Vector3(15.0f,-2.98f,0), Quaternion.identity);
            //now_bomb[5]=true;
        }
        if(now_bomb[6]==null)
        {
            now_bomb[6]=Instantiate(bomb, new Vector3(27.0f,-2.98f,0), Quaternion.identity);
            //now_bomb[6]=true;
        }
        if(now_bomb[7]==null)
        {
            now_bomb[7]=Instantiate(bomb, new Vector3(40.0f,-2.98f,0), Quaternion.identity);
            //now_bomb[7]=true;
        } 
        if(now_bomb[8]==null)
        {
            now_bomb[8]=Instantiate(bomb, new Vector3(51.5f,-2.98f,0), Quaternion.identity);
        }    
    }
}
