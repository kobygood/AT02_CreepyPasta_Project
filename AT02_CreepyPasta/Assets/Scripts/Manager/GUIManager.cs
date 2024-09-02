using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

#region AUTHOR & COPYRIGHT DETAILS
// (Author and copyright details remain unchanged)
#endregion

/// <summary>
/// Script responsible for updating and managing elements of the user interface in response to gameplay events.
/// </summary>
public class GUIManager : MonoBehaviour, ILoggable
{
    [Tooltip("Toggle on to print console messages from this component.")]
    [SerializeField] private bool debug;

    [Header("Note Overlay Properties")]
    [Tooltip("A list of note overlay objects.")]
    [SerializeField] private List<GameObject> noteOverlays = new List<GameObject>();
    [Tooltip("A list of note text objects corresponding to each note overlay.")]
    [SerializeField] private List<Text> noteTexts = new List<Text>();

    [Header("Author Card Properties")]
    [Tooltip("A reference to the author card overlay object.")]
    [SerializeField] private GameObject authorCardOverlay;
    [Tooltip("A reference to the author card text object.")]
    [SerializeField] private Text authorCardText;

    private Dictionary<NoteInteraction, GameObject> activeNoteOverlays = new Dictionary<NoteInteraction, GameObject>();

    private void OnEnable()
    {
        NoteInteraction.NoteInteractionEvent += ToggleNoteOverlay;
        GameManager.AuthorCardEvent += TriggerAuthorCard;
    }

    private void OnDisable()
    {
        NoteInteraction.NoteInteractionEvent -= ToggleNoteOverlay;
        GameManager.AuthorCardEvent -= TriggerAuthorCard;
    }

    private void OnDestroy()
    {
        NoteInteraction.NoteInteractionEvent -= ToggleNoteOverlay;
        GameManager.AuthorCardEvent -= TriggerAuthorCard;
    }

    private void Start()
    {
        // Initialize all note overlays to inactive
        foreach (var overlay in noteOverlays)
        {
            overlay.SetActive(false);
        }

        if (authorCardOverlay != null)
        {
            authorCardOverlay.SetActive(false);
        }
    }

    /// <summary>
    /// Toggles the note overlay active state and updates the text to match the provided note interaction.
    /// </summary>
    /// <param name="noteInteraction">The note to update the note text to.</param>
    private void ToggleNoteOverlay(NoteInteraction noteInteraction, bool toggle)
    {
        if (noteInteraction == null || noteOverlays.Count == 0 || noteTexts.Count == 0)
        {
            Log("NoteInteraction or overlays not properly set up.", 2);
            return;
        }

        if (toggle)
        {
            // Activate a note overlay and update the text
            for (int i = 0; i < noteOverlays.Count; i++)
            {
                if (!noteOverlays[i].activeSelf) // Find the first inactive overlay
                {
                    noteOverlays[i].SetActive(true);
                    noteTexts[i].text = noteInteraction.ReadingContent;
                    activeNoteOverlays[noteInteraction] = noteOverlays[i]; // Link interaction to overlay
                    break;
                }
            }
        }
        else
        {
            // Deactivate the overlay associated with the note interaction
            if (activeNoteOverlays.TryGetValue(noteInteraction, out GameObject overlay))
            {
                overlay.SetActive(false);
                noteTexts[noteOverlays.IndexOf(overlay)].text = "";
                activeNoteOverlays.Remove(noteInteraction);
            }
        }

        Log($"Overlay toggled for {noteInteraction?.name ?? "unknown note"}.", 0);
    }

    private void TriggerAuthorCard(string authorText)
    {
        if (authorCardOverlay != null && !authorCardOverlay.activeSelf)
        {
            if (authorCardText != null)
            {
                authorCardText.text = authorText;
                authorCardOverlay.SetActive(true);
            }
        }
    }

    public void Log(string message, int level = 0)
    {
        if (debug)
        {
            switch (level)
            {
                default:
                case 0:
                    Debug.Log($"[GUI MANAGER] - {gameObject.name}: {message}");
                    break;
                case 1:
                    Debug.LogWarning($"[GUI MANAGER] - {gameObject.name}: {message}");
                    break;
                case 2:
                    Debug.LogError($"[GUI MANAGER] - {gameObject.name}: {message}");
                    break;
            }
        }
    }
}
