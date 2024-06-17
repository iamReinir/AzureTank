using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public TMP_Text timeCounter;
    private float startTime;
    private static bool isRunning = false;
    private static string gameDuration;

    public static string GameDuration
    {
        get { return gameDuration; }
    }

    void Start()
    {
        startTime = Time.time;
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString("00");
            string seconds = (t % 60).ToString("00");
            timeCounter.text = gameDuration =   minutes + ":" + seconds;
        }
    }
    public static void StopTimer()
    {
        isRunning = false;
    }
}
