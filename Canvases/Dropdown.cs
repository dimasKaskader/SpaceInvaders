using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dropdown : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public RectTransform container;
    public bool isOpen;
    public Text textOnButton;
    static GameObject clicked;

    public void OnDeselect(BaseEventData eventData)
    {
		if (!isAndroid)
            isOpen = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
		if (!isAndroid)
			isOpen = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
		if (!isAndroid)
			isOpen = false;
    }

    public void OnSelect(BaseEventData eventData)
    {
		if (!isAndroid)
            isOpen = true;
    }

	bool isAndroid;
    void Start ()
    {
        container = transform.FindChild("Container").GetComponent<RectTransform>();
        isOpen = false;
		if (Application.platform == RuntimePlatform.Android)
			isAndroid = true;
		else
			isAndroid = false;
    }
	

	public void OnClickEvent()
	{
		if (isAndroid) {
			if (isOpen == false) {
				isOpen = true;
				clicked = gameObject;
			} else 
				clicked = null;
		}
	}

	void Update ()
    {
		if (clicked != gameObject && isAndroid)
			isOpen = false;
		if(isOpen)
        {
            Vector3 scale = container.localScale;
            scale.x = Mathf.Lerp(scale.x, 1, Time.deltaTime * 12);
            scale.y = Mathf.Lerp(scale.y, 1, Time.deltaTime * 12);
            container.localScale = scale;
        }
        else
        {
            Vector3 scale = container.localScale;
            scale.x = Mathf.Lerp(scale.x, 0, Time.deltaTime * 12);
            scale.y = Mathf.Lerp(scale.y, 0, Time.deltaTime * 12);
            container.localScale = scale;
        }
        if (textOnButton.text[3] == 'г')
            textOnButton.text = "По горизонтали: " + SpaceInvaders.HorizontalMax;
        if (textOnButton.text[3] == 'в')
            textOnButton.text = "По вертикали: " + SpaceInvaders.VerticalMax;
        if (textOnButton.text[0] == 'Ж')
            textOnButton.text = "Жизни: " + SpaceInvaders.lifes;
		if (!isAndroid) {
			GameObject currentSelected = GameObject.FindWithTag ("EventSystem").GetComponent<EventSystem> ().currentSelectedGameObject;
			if (currentSelected) {
				if (currentSelected.GetComponentInParent<HorizontalLayoutGroup> () == GetComponentInChildren<HorizontalLayoutGroup> () ||
				   currentSelected.transform == transform)
                { //если является кнопкой, принадлежайщей этому лэйауту, или самой кнопкой лэйаута
					isOpen = true;
				} else
					isOpen = false;
			} else if (Cursor.visible == false)
				isOpen = false;
		}
        if (isAndroid)
            GameObject.FindWithTag("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(null);
    }
}
