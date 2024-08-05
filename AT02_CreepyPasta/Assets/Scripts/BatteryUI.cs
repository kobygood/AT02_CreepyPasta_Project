using UnityEngine;
using UnityEngine.UI;

public class BatteryUI : MonoBehaviour
{
    public Flashlight flashlight; // Reference to your Flashlight script
    public Image batteryFill; // Reference to the BatteryFill Image

    private float maxBrightness;

    void Start()
    {
        if (flashlight == null)
        {
            flashlight = FindObjectOfType<Flashlight>();
        }
        maxBrightness = flashlight.maxBrightness;
    }

    void Update()
    {
        UpdateBatteryUI();
    }

    void UpdateBatteryUI()
    {
        float batteryPercentage = (flashlight.GetLightIntensity() / maxBrightness);

        // Update the fill amount of the battery image
        batteryFill.fillAmount = batteryPercentage;

        // Change the color based on the battery percentage
        if (batteryPercentage > 0.5f)
        {
            batteryFill.color = Color.Lerp(Color.yellow, Color.green, (batteryPercentage - 0.5f) * 2);
        }
        else
        {
            batteryFill.color = Color.Lerp(Color.red, Color.yellow, batteryPercentage * 2);
        }
    }
}
