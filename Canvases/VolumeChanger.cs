using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VolumeChanger : MonoBehaviour, IEndDragHandler, ISelectHandler, IDeselectHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    bool pointerOut;
    void Start()
    {
        GetComponent<Slider>().value = AudioListener.volume * 100;
        GetComponentInChildren<Text>().text = ((int)(AudioListener.volume * 100)).ToString() + "%";
    }

    void Update()
    {
        GetComponent<Slider>().onValueChanged.AddListener(call => OnSliderMove(GetComponent<Slider>().value));
    }
    float OnSliderMove(float value)
    {
        AudioListener.volume = value / 100;
        GetComponentInChildren<Text>().text = value.ToString() + "%";
        if (Cursor.visible == false)
            GetComponent<AudioSource>().Play();
        return value;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<AudioSource>().Play();
        if (pointerOut)
        {
            transform.FindChild("Fill Area").FindChild("Fill").GetComponent<Image>().color = Color.white;
            transform.FindChild("Backgroundd").GetComponent<Image>().color = new Color(1, 1, 1, 0.098F);
        }
        PlayerPrefs.SetFloat("Volume", AudioListener.volume);
        PlayerPrefs.Save();
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.FindChild("Fill Area").FindChild("Fill").GetComponent<Image>().color = new Color(0.149F, 0.294F, 1, 1); //синий
        transform.FindChild("Backgroundd").GetComponent<Image>().color = new Color(0.149F, 0.294F, 1, 0.4F);
    }
    public void OnSelect(BaseEventData eventData)
    {
        transform.FindChild("Fill Area").FindChild("Fill").GetComponent<Image>().color = new Color(0.149F, 0.294F, 1, 1); //синий
        transform.FindChild("Backgroundd").GetComponent<Image>().color = new Color(0.149F, 0.294F, 1, 0.4F);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        if (Cursor.visible == false)
        {
            transform.FindChild("Fill Area").FindChild("Fill").GetComponent<Image>().color = Color.white;
            transform.FindChild("Backgroundd").GetComponent<Image>().color = new Color(1, 1, 1, 0.098F);
        }
        PlayerPrefs.SetFloat("Volume", AudioListener.volume);
        PlayerPrefs.Save();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerOut = false;
        transform.FindChild("Fill Area").FindChild("Fill").GetComponent<Image>().color = new Color(0.149F, 0.294F, 1, 1); //синий
        transform.FindChild("Backgroundd").GetComponent<Image>().color = new Color(0.149F, 0.294F, 1, 0.4F);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerOut = true;
        transform.FindChild("Fill Area").FindChild("Fill").GetComponent<Image>().color = Color.white;
        transform.FindChild("Backgroundd").GetComponent<Image>().color = new Color(1, 1, 1, 0.098F);
    }

}
