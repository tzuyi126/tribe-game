using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject P1;
    public GameObject P2;

    public GameObject Canvas;
    private GameObject ArrowRight;
    private GameObject ArrowLeft;

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Camera camera;
    Vector3 camera_position = new Vector3(0,0,0);
    static public int Dominance = 0; // 0 for no dominance   1 for p1 dominance   2 for p2 dominance
    static public int playerDom = 0; //1 for p1   2 for p2
    static public bool p1_alive = true;
    static public bool p2_alive = true;

    void Start()
    {
        Dominance = 0;
        playerDom = 0;

        ArrowRight = Canvas.transform.Find("ArrowRight").gameObject;
        ArrowRight.SetActive(false);
        ArrowLeft = Canvas.transform.Find("ArrowLeft").gameObject;
        ArrowLeft.SetActive(false);
    }

    
    void Update()
    {
        if(playerDom == 0)
        {
            ArrowLeft.SetActive(false);
            ArrowRight.SetActive(false);
        }
        else if(playerDom == 1)
        {
            ArrowLeft.SetActive(true);
            ArrowRight.SetActive(false);
        }
        else if(playerDom == 2)
        {
            ArrowLeft.SetActive(false);
            ArrowRight.SetActive(true);
        }
        
        if(Dominance == 0){
            
            camera_position = new Vector3((P1.transform.position.x+P2.transform.position.x)/2,0,-10);
            
        }
        else if(Dominance == 1){
            playerDom = 1;
            camera_position = new Vector3(P1.transform.position.x-6,0,-10);
        }
        else if(Dominance == 2){
            playerDom = 2;
            camera_position = new Vector3(P2.transform.position.x+6,0,-10);
        }
        
        if(camera_position.x<=-54.4f)camera_position.x = -54.4f;
        else if(camera_position.x>=54.4f)camera_position.x = 54.4f;

        Vector3 point = camera.WorldToViewportPoint(camera_position);
        Vector3 delta = camera_position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    }
}
