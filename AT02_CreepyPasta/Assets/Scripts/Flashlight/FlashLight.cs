// Made By Koby Good-Gerne 10/08/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Light m_Light;
    public bool drainOverTime;
    public float maxBrightness;
    public float minBrightness;
    public float drainRate;

    private Inventory inventory;

    void Start()
    {
        m_Light = GetComponent<Light>();
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        m_Light.intensity = Mathf.Clamp(m_Light.intensity, minBrightness, maxBrightness);
        if (drainOverTime && m_Light.enabled)
        {
            if (m_Light.intensity > minBrightness)
            {
                m_Light.intensity -= Time.deltaTime * (drainRate / 1000);
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            m_Light.enabled = !m_Light.enabled;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RechargeFlashlight();
        }
    }

    public float GetLightIntensity()
    {
        return m_Light.intensity;
    }

     public void ToggleLight()
        {
            m_Light.enabled = !m_Light.enabled;
        }

    void RechargeFlashlight()
    {
        Item battery = inventory.GetItemByName("Battery");

        if (battery != null && battery.quantity > 0)
        {
            m_Light.intensity = maxBrightness; 
            inventory.UseItem(battery); 
            Debug.Log("Flashlight recharged!");
        }
        else
        {
            Debug.Log("No batteries available to recharge the flashlight.");
        }
    }
}
