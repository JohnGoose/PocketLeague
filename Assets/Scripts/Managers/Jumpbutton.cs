using UnityEngine;
using UnityEngine.EventSystems;

public class Jumpbutton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

public bool Pressed;
public bool NotPressed;

	public void OnPointerDown(PointerEventData eventData)
	{
		Pressed = true;
		NotPressed = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Pressed = false;
		NotPressed = true;
	}
}
