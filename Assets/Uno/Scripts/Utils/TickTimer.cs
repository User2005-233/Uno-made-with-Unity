using System;

public class TickTimer
{
    public float Duration { get; private set; }
    public float TimeRemaining { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsCompleted => TimeRemaining <= 0 && IsRunning;

    public event Action OnTimerComplete;

    public TickTimer(float duration)
    {
        Duration = duration;
        TimeRemaining = duration;
    }

    public void Start()
    {
        TimeRemaining = Duration;
        IsRunning = true;
    }

    public void Pause() => IsRunning = false;
    public void Resume() => IsRunning = true;

    /// <summary>
    /// Progresses the timer. Pass Time.deltaTime or Time.fixedDeltaTime explicitly.
    /// </summary>
    public void Tick(float deltaTime)
    {
        if (!IsRunning) return;

        TimeRemaining -= deltaTime;

        if (TimeRemaining <= 0)
        {
            TimeRemaining = 0;
            IsRunning = false;
            OnTimerComplete?.Invoke();
        }
    }

    public void ManualTriggerCallback()
    {
        if (!IsRunning) return;
        OnTimerComplete?.Invoke();
    }

    public void Reset()
    {
        TimeRemaining = Duration;
        IsRunning = false;
    }

    public void Next()
    {
        TimeRemaining = Duration;
        IsRunning = true;
    }
}