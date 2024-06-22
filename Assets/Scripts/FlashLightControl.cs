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
        RotateFlashlight();
    }

    void ToggleFlashlight()
    {
        isFlashlightOn = !isFlashlightOn;
        flashlight.SetActive(isFlashlightOn);
    }
    void RotateFlashlight()
    {
        // Get the mouse position in the world
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction to the mouse
        Vector3 direction = mousePos - transform.position;

        // Calculate the angle to rotate
        float angle = Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg;

        // Apply the rotation
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
