using TMPro;  // Make sure to import TextMesh Pro namespace
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI fpsText; // Reference to the TextMesh Pro UI component

    [Header("Settings")]
    public float updateInterval = 0.5f; // Interval in seconds to update the FPS count

    private float timeElapsed = 0f;
    private int frameCount = 0;
    private float fps = 0f;

    void Update()
    {
        // Accumulate the time and frame count
        timeElapsed += Time.unscaledDeltaTime;
        frameCount++;

        // Update the FPS value after the specified interval
        if (timeElapsed >= updateInterval)
        {
            fps = frameCount / timeElapsed;  // Calculate the FPS
            fpsText.text = "FPS: " + Mathf.RoundToInt(fps).ToString();  // Display the FPS

            // Reset time and frame count for the next update
            timeElapsed = 0f;
            frameCount = 0;
        }
    }
}
