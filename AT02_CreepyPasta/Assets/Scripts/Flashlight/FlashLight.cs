// Made By Koby Good-Gerne 10/08/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light m_Light;
    public bool drainOverTime;
    public float maxBrightness;
    public float minBrightness;
    public float drainRate;

    
    void Start()
    {
        m_Light = GetComponent<Light>();
        m_Light.intensity = maxBrightness; 
    }

    
    void Update()
    {
        m_Light.intensity = Mathf.Clamp(m_Light.intensity, minBrightness, maxBrightness);
        if (drainOverTime == true && m_Light.enabled == true)
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
    }

    public void AddBattery(float amount)
    {
        m_Light.intensity = Mathf.Clamp(m_Light.intensity + amount, minBrightness, maxBrightness);
    }


    public float GetLightIntensity()
    {
        return m_Light.intensity;
    }
}