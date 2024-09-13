using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CustomizableTimerWithTMP : MonoBehaviour
{
    [Tooltip("Set the timer duration in seconds.")]
    public float totalTime = 300f; // Default is 5 minutes, adjustable in the Inspector.

    private float currentTime;
    public UnityEvent onTimeOver; // Event triggered when time is over

    private bool isTimerRunning;

    [Tooltip("Reference to the TextMeshPro component to display the timer.")]
    public TextMeshProUGUI timerText; // Reference to the TMP text component

    void Start()
    {
        StartTimer();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                isTimerRunning = false;
                onTimeOver.Invoke(); // Trigger the event when time is over
            }

            UpdateTimerDisplay(); // Update the TMP text
        }
    }

    // Start the timer with the set total time
    public void StartTimer()
    {
        currentTime = totalTime;
        isTimerRunning = true;
    }

    // Function to manually stop the timer if needed
    public void StopTimer()
    {
        isTimerRunning = false;
    }

    // Updates the TMP text with the current time formatted as MM:SS
    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // Optional: Function to get the remaining time
    public float GetCurrentTime()
    {
        return currentTime;
    }
}
