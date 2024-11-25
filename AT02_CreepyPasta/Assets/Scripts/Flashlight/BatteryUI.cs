using UnityEngine;
using UnityEngine.UI;

public class BatteryUI : MonoBehaviour
{
    public Flashlight flashlight; 
    public Image batteryFill; 

    private float maxBrightness;

    void Start()
    {
        //UI is full green on start
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

       
        batteryFill.fillAmount = batteryPercentage;
        //changes the colour of the battery UI from green to red as it loses intensity
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