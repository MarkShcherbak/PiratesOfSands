using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Завершение гонки
/// </summary>
public class TrackFinishMenuModelView : MonoBehaviour
{
    public TrackPath trackPath;
    public TextMeshProUGUI textMeshPro;

    private void Update()
    {
        System.Text.StringBuilder strb = new System.Text.StringBuilder();
        if (trackPath.isWin)
        {
            strb.AppendLine("WINNER!");
        }
        else
        {
            strb.AppendLine("LOOSER!");
        }
        foreach (string rawLeader in trackPath.GetFinishTrackLeaderBoard())
        {
            strb.AppendLine(rawLeader);
        }
        textMeshPro.SetText(strb);
    }
}
