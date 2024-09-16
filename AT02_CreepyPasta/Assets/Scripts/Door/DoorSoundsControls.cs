using UnityEngine;

public class DoorSoundsController : MonoBehaviour
{
    public Animator animator;
    public AudioSource openSound;
    public AudioSource closeSound;
    public AudioSource lockSound;
    private bool isOpen;
    private bool initialized = false;

    void Start()
    {
        isOpen = animator.GetCurrentAnimatorStateInfo(0).IsName("Open");
        Invoke("Initialize", 0.5f);
    }

    void Initialize()
    {
        initialized = true;
    }

    public void OpenDoor()
    {
        if (!isOpen && initialized)
        {
            openSound.Play();
            animator.SetTrigger("Open");
            isOpen = true;
        }
    }

    public void CloseDoor()
    {
        if (isOpen && initialized)
        {
            closeSound.Play();
            animator.SetTrigger("Close");
            isOpen = false;
        }
    }

    public void PlayLockSound()
    {
        if (lockSound != null)
        {
            lockSound.Play();
        }
    }

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
