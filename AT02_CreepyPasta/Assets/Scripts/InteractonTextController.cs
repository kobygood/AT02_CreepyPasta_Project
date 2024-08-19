using TMPro;
using UnityEngine;

public class InteractionTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactionText;

    // Show the interaction text with a given message
    public void ShowText(string message)
    {
        if (interactionText != null)
        {
            interactionText.text = message;
            interactionText.gameObject.SetActive(true);
        }
    }

    // Hide the interaction text
    public void HideText()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }
}
