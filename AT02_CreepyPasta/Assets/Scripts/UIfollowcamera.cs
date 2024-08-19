using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    public Transform playerCamera;

    void Update()
    {
        if (playerCamera != null)
        {
            // Make the text always face the camera
            transform.LookAt(transform.position + playerCamera.forward);
        }
    }
}
