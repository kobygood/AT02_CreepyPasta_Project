using UnityEngine;

public class DoorInteraction : Interactable
{
    [SerializeField] private bool openOnStart = false;
    [SerializeField] private DoorInteraction[] lockGroup;
    [SerializeField] private bool lockedOnStart = false;
    [SerializeField] private InteractionTextController interactionTextController;
    [SerializeField] private DoorSoundsController doorSoundsController; // Updated to use the new class name

    private bool locked = false;
    private Animator anim;

    protected override void Awake()
    {
        if (transform.parent != null)
        {
            if (transform.parent.TryGetComponent(out anim) == false)
            {
                Log($"{transform.parent.name} requires an Animator component!", 1);
            }
            if (transform.parent.TryGetComponent(out aSrc) == false)
            {
                Log($"{transform.parent.name} requires an Audio Source component!", 1);
            }
        }
        else
        {
            Log($"{gameObject.name} requires a parent object.", 2);
        }
    }

    void Start()
    {
        if (anim != null)
        {
            if (anim.GetBool("open") == true && openOnStart == false)
            {
                anim.SetBool("open", false);
            }
            else
            {
                anim.SetBool("open", openOnStart);
            }
        }
        if (lockedOnStart == true)
        {
            ToggleLockState(true);
        }
    }

    public override bool OnInteract(out Interactable engagedAction)
    {
        engagedAction = null;
        if (Active)
        {
            ToggleDoorState();

            if (interactionTextController != null)
            {
                interactionTextController.HideText();
            }
            else
            {
                Log("InteractionTextController is not assigned.", 2);
            }

            return true;
        }
        return false;
    }

    public void ToggleDoorState()
    {
        if (locked == false)
        {
            if (anim != null)
            {
                if (anim.GetBool("open") == false)
                {
                    anim.SetBool("open", true);
                }
                else if (anim.GetBool("open") == true)
                {
                    anim.SetBool("open", false);
                }
            }
            PlaySound(interactionClip, aSrc);
        }
        else
        {
            doorSoundsController.PlayLockSound(); // Use the updated reference to play the lock sound
        }
    }

    public void ToggleLockState(bool lockState = true)
    {
        locked = lockState;
        foreach (DoorInteraction door in lockGroup)
        {
            if (door != this && door.locked != lockState)
            {
                door.ToggleLockState(lockState);
            }
        }
        Log("Lockstate set to " + locked);
        if (locked == true)
        {
            if (anim != null)
            {
                if (anim.GetBool("open") == true)
                {
                    anim.SetBool("open", false);
                    PlaySound(interactionClip, aSrc);
                }
            }
            else
            {
                PlaySound(interactionClip, aSrc);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string actionText = IsOpen() ? "Press E to close door" : "Press E to open door";
            interactionTextController.ShowText(actionText);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionTextController.HideText();
        }
    }

    public bool IsOpen()
    {
        return anim.GetBool("open");
    }
}
