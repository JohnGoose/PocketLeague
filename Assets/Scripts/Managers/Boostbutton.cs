using UnityEngine;
using UnityEngine.EventSystems;

public class Boostbutton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
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