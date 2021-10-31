using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Parts))]
public class PartSelector : MonoBehaviour
{
    public Material highlight_material;
    private List<GameObject> selectable_parts;
    public Dictionary<GameObject, Renderer> renderers;
    private Ray ray;
    public CameraController camera_controller;
    private RaycastHit hit;
    private GameObject sel_part;
    private GameObject sel_label;
    private Material orig_mat;
    

    // Start is called before the first frame update
    void Start()
    {
        camera_controller = gameObject.GetComponent<CameraController>();
        renderers = new Dictionary<GameObject, Renderer>();
        selectable_parts = gameObject.GetComponent<Parts>().selectable_parts;

        //cache the renderers for the specific parts to save on lookup time
        foreach(GameObject parts in selectable_parts)
        {
            renderers.Add(parts, parts.gameObject.GetComponent<Renderer>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        highlightPart();
        
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (selectable_parts.Contains(hit.collider.gameObject))
                {
                    SelectPart(hit.collider.gameObject);
                }
            }
        }
    }

    //highlights parts that are currently being hovered over
    public void highlightPart()
    {   
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (sel_part != null)
            {
                if (sel_part.tag != "light" && orig_mat != null)
                {
                    renderers[sel_part].material = orig_mat;
                    orig_mat = null;
                }
                else if(sel_part.tag == "light")
                    renderers[sel_part].enabled = false;        
            }

            sel_part = hit.collider.gameObject;

            print(hit.collider.name);
            //Check and see if that object is a selectable object
            if (selectable_parts.Contains(sel_part) && sel_part.tag!= "light")
            {
                orig_mat = renderers[sel_part].material;
                renderers[sel_part].material = highlight_material;
            }
            else
            {
                renderers[sel_part].enabled = true;
            }
        }
    }

    public void SelectPart(GameObject part)
    {
        if (selectable_parts.Contains(part))
        {
            //turn of previously activated label if it exists
            if (sel_label != null && sel_label.activeSelf)
                sel_label.SetActive(false);
            //Activate label to display text
            sel_label = part.transform.GetChild(0).gameObject;
            sel_label.SetActive(true);
            camera_controller.MoveToPart(part);
        }
    }
}
