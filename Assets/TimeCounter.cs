using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    private float startTime;
    private bool isRunning = false;
    public string gameDuration { get; set; }
    public float playTime { get; private set; }

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
            gameDuration = $"{minutes}:{seconds}";
            playTime = t;
        }
    }
    public void StopTimer()
    {
        isRunning = false;
    }
    public void StartTimer()
    {
		isRunning = true;
	}
}
