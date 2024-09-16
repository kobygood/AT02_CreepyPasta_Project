using UnityEngine;
using TMPro;

public class Battery : MonoBehaviour
{
    public float intensityToAdd = 0.25f;
    public KeyCode interactKey = KeyCode.E;
    public TextMeshProUGUI interactText;

    private bool isInRange = false;
    private Flashlight flashlight;

    void Start()
    {
        if (interactText != null)
        {
            interactText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(interactKey))
        {
            PickupBattery();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            flashlight = other.GetComponentInChildren<Flashlight>();
            isInRange = true;
            ShowInteractText(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            ShowInteractText(false);
        }
    }

    void PickupBattery()
    {
        if (flashlight != null)
        {
            flashlight.AddBattery(intensityToAdd);
            ShowInteractText(false);
            Destroy(gameObject);
        }
    }

    void ShowInteractText(bool show)
    {
        if (interactText != null)
        {
            interactText.gameObject.SetActive(show);
            interactText.text = "Pickup Battery";
        }
    }
}
