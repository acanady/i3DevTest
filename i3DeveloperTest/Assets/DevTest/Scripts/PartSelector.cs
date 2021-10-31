using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Parts))]
public class PartSelector : MonoBehaviour
{
    private List<GameObject> selectable_parts;
    private Ray ray;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        selectable_parts = gameObject.GetComponent<Parts>().selectable_parts;
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            print(hit.collider.name);
            
        }
    }
}
