using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator animator;      // Reference to the Animator controlling the door animations
    public AudioSource openSound;  // Reference to the opening sound AudioSource
    public AudioSource closeSound; // Reference to the closing sound AudioSource
    private bool isOpen;           // Track the door's state
    private bool initialized = false; // To prevent initial sound playing on start

    void Start()
    {
        // Initialize isOpen based on the animator's initial state
        isOpen = animator.GetCurrentAnimatorStateInfo(0).IsName("Open");

        // Directly set the initialized flag after a short delay
        Invoke("Initialize", 0.5f);  // Adjust the delay time as needed
    }

    void Initialize()
    {
        initialized = true;  // Now allow sounds to be played
    }

    // Method to open the door
    public void OpenDoor()
    {
        if (!isOpen && initialized)
        {
            openSound.Play();
            // Trigger the door opening animation
            animator.SetTrigger("Open");
            isOpen = true;
        }
    }

    // Method to close the door
    public void CloseDoor()
    {
        if (isOpen && initialized)
        {
            closeSound.Play();
            // Trigger the door closing animation
            animator.SetTrigger("Close");
            isOpen = false;
        }
    }

    // Example of a trigger or input to open/close the door
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseDoor();
        }
    }
}
