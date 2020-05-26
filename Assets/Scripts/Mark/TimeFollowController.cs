using System.Collections;
using UnityEngine;


/// <summary>
/// Управление временем
/// </summary>
public class TimeFollowController: Singleton<TimeFollowController>
{
    /// <summary>
    /// пауза
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    /// <summary>
    /// пауза с обратным отсчетом
    /// </summary>
    /// <param name="timer"></param>
    public void Pause(float timer = 5f)
    {
        StartCoroutine(TimerPause(timer));
    }

    /// <summary>
    /// возобновление игры
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1f;
    }

    /// <summary>
    /// установить множитель скорости игрового времени
    /// </summary>
    /// <param name="coeff"></param>
    public void SetTimeScale(float coeff = 2f)
    {
        Time.timeScale = coeff;
    }

    /// <summary>
    /// умножить игровое время на два
    /// </summary>
    public void DoubleTimeScale()
    {
        Time.timeScale *= 2;
    }

    private IEnumerator TimerPause(float timer)
    {
        Pause();
        yield return new WaitForSecondsRealtime(timer);
        Resume();
    }

}


