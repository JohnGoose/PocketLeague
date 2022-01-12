using UnityEngine;
using UnityEngine.EventSystems;

public class Driftbutton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
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
