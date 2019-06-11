using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler {
	public static float horizontalTouchAxis;
	public static bool fire1TouchAxis;
	public float axisValue;
	public string axisName;
	// Use this for initialization
	void Start () {
		if (Application.platform != RuntimePlatform.Android)
			foreach (GameObject a in GameObject.FindGameObjectsWithTag("TouchButton")) 
			{
				Destroy (a);
			}
		
		horizontalTouchAxis = 0;
		fire1TouchAxis = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		if (axisName == "Horizontal")
			horizontalTouchAxis = Mathf.MoveTowards (0, axisValue, Time.deltaTime * 50);
		if (axisName == "Fire1")
			fire1TouchAxis = true;
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (axisName == "Horizontal")
			horizontalTouchAxis = Mathf.MoveTowards (0, axisValue, Time.deltaTime * 50);
		if (axisName == "Fire1")
			fire1TouchAxis = true;
	}
	public void OnPointerUp(PointerEventData eventData)
	{
		if (axisName == "Horizontal")
			horizontalTouchAxis = 0;
		if (axisName == "Fire1")
			fire1TouchAxis = false;
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		if (axisName == "Horizontal")
			horizontalTouchAxis = 0;
		if (axisName == "Fire1")
			fire1TouchAxis = false;
	}
}
