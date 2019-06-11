using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChosenElement : MonoBehaviour
{
    Vector3 previousPosition;
    
    void Start ()
    {
        if (Application.platform == RuntimePlatform.Android)
            Destroy(this);
		if (Cursor.visible == false)
            GameObject.FindWithTag("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(GameObject.FindWithTag("FirstMenuOption"));
        previousPosition = Input.mousePosition;
    }
	
	void Update ()
    {
        if ((Input.GetButton("Vertical") || Input.GetAxis("Vertical") != 0) && GameObject.FindWithTag("EventSystem").GetComponent<EventSystem>().currentSelectedGameObject == null)
        {
            Cursor.visible = false;
            GameObject.FindWithTag("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(GameObject.FindWithTag("FirstMenuOption"));
        }
        if(Input.mousePosition!=previousPosition)
        {
            GameObject selected = GameObject.FindWithTag("EventSystem").GetComponent<EventSystem>().currentSelectedGameObject;
            if(selected != null)
            {
                if(selected.name != "InputField")
                    GameObject.FindWithTag("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(null);
            }
            
            Cursor.visible = true;
        }
        previousPosition = Input.mousePosition;
    }
}
