using UnityEngine;

[RequireComponent(typeof(MobileInput))]
public class GameManager : MonoBehaviour
{
    // public static InputManager InputManager;
    public static AudioManager AudioManager;
	public static MobileInput MobileInput;

	// public static TestingInputSystem TestingInput;
    
    void Awake()
    {
        // InputManager = GetComponent<InputManager>();
		// TestingInput = GetComponent<TestingInputSystem>();
        AudioManager = GetComponent<AudioManager>();
		MobileInput = GetComponent<MobileInput>();
        //DontDestroyOnLoad(gameObject);
    }
}