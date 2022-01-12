using UnityEngine;
using UnityEngine.EventSystems;

public class Ballcambutton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

public bool Pressed;

	public void OnPointerDown(PointerEventData eventData)
	{
		Pressed = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Pressed = false;
	}
}
