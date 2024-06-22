using UnityEngine;

public class FlashlightControl : MonoBehaviour
{
    public GameObject flashlight; // Assign the Flashlight GameObject in the Inspector
    private bool isFlashlightOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }
    }

    void ToggleFlashlight()
    {
        isFlashlightOn = !isFlashlightOn;
        flashlight.SetActive(isFlashlightOn);
    }
}
