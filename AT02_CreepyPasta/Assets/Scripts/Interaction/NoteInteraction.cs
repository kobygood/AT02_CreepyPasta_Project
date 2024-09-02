using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#region AUTHOR & COPYRIGHT DETAILS
// (Author and copyright details remain unchanged)
#endregion

/// <summary>
/// Script responsible for defining note interaction functionality.
/// </summary>
public class NoteInteraction : Interactable
{
    public delegate void NoteInteractionDelegate(NoteInteraction note, bool toggle);

    [Tooltip("The text content that will be displayed when the player interacts with the note.")]
    [SerializeField][TextArea] private string readingContent;
    [Tooltip("The audio clip that plays on disengaging with the interaction.")]
    [SerializeField] protected AudioClip disengageClip;
    [Tooltip("If true, the interaction events will only execute the first time the note is interacted with.")]
    [SerializeField] private bool interactionEventsFireOnce = true;
    [Tooltip("If true, the interaction events are executed when the note is disengaged with. If false, the interaction events are executed when the note is initially interacted with.")]
    [SerializeField] private bool interactionEventsFireOnDisengage = true;
    [Tooltip("Defines the functions that will be executed upon an interaction event.")]
    [SerializeField] private UnityEvent interactionEvents;

    [Header("Assigned Note Overlay Components")]
    [Tooltip("The specific overlay GameObject for this note interaction.")]
    [SerializeField] private GameObject assignedNoteOverlay;
    [Tooltip("The text component associated with this note overlay.")]
    [SerializeField] private Text assignedNoteText;

    private bool readingActive = false;
    private bool eventsFired = false;

    /// <summary>
    /// Returns the text content that will be displayed when the player interacts with the note.
    /// </summary>
    public string ReadingContent { get { return readingContent; } }

    /// <summary>
    /// Event invoked on execution of interaction on disengage interaction methods.
    /// </summary>
    public static event NoteInteractionDelegate NoteInteractionEvent = delegate { };

    /// <summary>
    /// Invokes events associated with player interaction and disengagement.
    /// </summary>
    /// <returns>Returns true if the events were successfully invoked.</returns>
    private bool InvokeInteractionEvents()
    {
        if (interactionEventsFireOnce && !eventsFired)
        {
            interactionEvents.Invoke();
            eventsFired = true;
            return true;
        }
        else if (!interactionEventsFireOnce)
        {
            interactionEvents.Invoke();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Executed when the note is interacted with by the player.
    /// </summary>
    /// <param name="interactionInfo">The interaction data from the interaction system.</param>
    /// <param name="engagedAction">Outs a reference back to the interaction if it requires disengagement.</param>
    /// <returns>Returns true if the interaction was successfully completed.</returns>
    public override bool OnInteract(out Interactable engagedAction)
    {
        if (base.OnInteract(out engagedAction))
        {
            ToggleOverlay(!readingActive); // Toggle based on current state
            readingActive = !readingActive; // Update state
            engagedAction = readingActive ? this : null;

            if (readingActive && !interactionEventsFireOnDisengage)
            {
                InvokeInteractionEvents();
            }
        }
        return readingActive;
    }

    /// <summary>
    /// Executed when the player activates the interaction input while a note is actively engaged.
    /// </summary>
    /// <returns>Returns true if the note was active and successfully disengaged.</returns>
    public override bool OnDisengageInteraction()
    {
        if (readingActive)
        {
            ToggleOverlay(false);
            readingActive = false;
            PlaySound(disengageClip, aSrc);

            if (interactionEventsFireOnDisengage)
            {
                InvokeInteractionEvents();
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Toggles the assigned overlay and updates its text content based on the interaction state.
    /// </summary>
    private void ToggleOverlay(bool toggle)
    {
        if (assignedNoteOverlay != null)
        {
            assignedNoteOverlay.SetActive(toggle);
            if (assignedNoteText != null)
            {
                assignedNoteText.text = toggle ? readingContent : "";
            }
        }
    }
}
