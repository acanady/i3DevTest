using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera camera;
    private GameObject viewed_part;
    private Vector3 offset;
    private Vector3 original_pos;

    // Start is called before the first frame update
    void Start()
    {
       
        camera = Camera.main;
        original_pos = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Moves camera to specific game obect location and looks at it
    public void MoveToPart(GameObject part)
    {
        camera.transform.position = original_pos;
        viewed_part = part;

        //Need a better way of handling objects that require a different zoom level
        if(part.name == "SkyCarBody")
        {
            Vector3 direction = (camera.transform.position - part.transform.position).normalized;
            camera.transform.position = part.transform.position + direction * 4f;
        }
        
        else
        {
            Vector3 direction = (camera.transform.position - part.transform.position).normalized;
            camera.transform.position = part.transform.position + direction * 3f;
        }
    }
}
