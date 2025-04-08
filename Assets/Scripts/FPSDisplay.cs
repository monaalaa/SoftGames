using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText; 
    private float deltaTime = 0.0f;

    void Update()
    {
        // Accumulate the time passed since the last frame
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        // Calculate the FPS
        float fps = 1.0f / deltaTime;

        // Update the FPS text UI with the calculated FPS
        fpsText.text = string.Format("FPS: {0:F2}", fps);
    }
}
