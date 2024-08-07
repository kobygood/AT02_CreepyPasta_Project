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

    private Inventory inventory; // Reference to the player's inventory

    // Start is called before the first frame update
    void Start()
    {
        m_Light = GetComponent<Light>();
        inventory = FindObjectOfType<Inventory>(); // Find the inventory in the scene
    }

    // Update is called once per frame
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

    private void RechargeFlashlight()
    {
        if (inventory != null)
        {
            Item battery = inventory.GetItemByName("Battery");
            if (battery != null && battery.isBattery)
            {
                m_Light.intensity = maxBrightness;
                inventory.RemoveItem(battery);
            }
        }
    }
}
