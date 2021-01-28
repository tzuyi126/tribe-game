using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefab_controller : MonoBehaviour
{
    public GameObject p2_prefab;
    private float press_time = 0;
    private float Instantiate_time = 0;
    private float count_time = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate_time = Random.Range(0.1f,0.15f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey){
            press_time+=Time.deltaTime;
            count_time+=Time.deltaTime;
        }
        else{
        }
        if(press_time>0 && press_time<=2.5f){
           if(count_time>Instantiate_time){
               Instantiate(p2_prefab,new Vector3(58.93f,-2.74f,0),Quaternion.identity);
               Instantiate_time = Random.Range(0.05f,0.18f);
               count_time = 0;
           }
        }
    }
}
