using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Populates the UI based on what Game Objects are present in the Parts object
[RequireComponent(typeof(PartSelector))]
public class PopulateUI : MonoBehaviour
{
    public Button uiButton;
    public GameObject canvas;
    private List<GameObject> selectable_parts;
    private PartSelector partSelector;
    // Start is called before the first frame update
    void Start()
    {
        selectable_parts = gameObject.GetComponent<Parts>().selectable_parts;
        partSelector = gameObject.GetComponent<PartSelector>();
        UISetup();
    }

   public void UISetup()
    {
        foreach (GameObject part in selectable_parts){
            Button button = Instantiate(uiButton);
            button.transform.SetParent(canvas.transform);
            button.transform.localRotation = Quaternion.identity;
            button.transform.localPosition = Vector3.zero;
            button.transform.localScale = new Vector3(1, 1, 1);
            button.transform.GetChild(0).GetComponent<Text>().text = part.name;
            button.onClick.AddListener(delegate { partSelector.SelectPart(part); });
            
        }
    }
}
