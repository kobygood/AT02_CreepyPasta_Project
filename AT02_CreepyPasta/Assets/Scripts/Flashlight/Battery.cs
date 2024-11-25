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
        //starts the game with interact text set to false
        if (interactText != null)
        {
            interactText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        //When the player is in range and they press their assigned interact key it will pick up the battery
        if (isInRange && Input.GetKeyDown(interactKey))
        {
            PickupBattery();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //shows the interact text when a player is in range
        if (other.CompareTag("Player"))
        {
            flashlight = other.GetComponentInChildren<Flashlight>();
            isInRange = true;
            ShowInteractText(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        //stops showing interact text when a player is no longer in range
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            ShowInteractText(false);
        }
    }

    void PickupBattery()
    {
        //adds battery to the flashlight and stops showing interact text
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
