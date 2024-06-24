using System.Security.Cryptography;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CHEntryGate : MonoBehaviour
{
    public string targetSceneName; // Name of the scene to load upon collision

    // OnTriggerEnter2D is called when another Collider2D enters the trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding GameObject has the tag "Player"
        if (collision.CompareTag("Player"))
        {
            LoadTargetScene(); // Call method to load the target scene
        }
    }

    // Method to load the target scene
    private void LoadTargetScene()
    {
        SceneManager.LoadScene("CreepyHospital"); // Load scene by name
    }
}

