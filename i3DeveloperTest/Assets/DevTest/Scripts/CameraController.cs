using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera camera;
    private GameObject viewed_part;
    private Vector3 position;
    private Vector3 rotation;
    private float duration;
    private bool lerping;

    // Start is called before the first frame update
    void Start()
    {
        duration = .5f;
        camera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Moves camera to specific game obect location and looks at it
    public void MoveToPart(GameObject part)
    {
        position = part.GetComponent<LookPosition>().position;
        rotation = part.GetComponent<LookPosition>().rotation;
        camera.transform.eulerAngles = rotation;
        if(!lerping)
            StartCoroutine(Lerp());
        
    }

    IEnumerator Lerp()
    {
        lerping = true;
        float time = 0;
        while(time < duration)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position,position, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        lerping = false;
        camera.transform.position = position;
    }

}
