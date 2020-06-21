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
    public Text text;
    public Button exitButton;

    public event EventHandler OnExitToMainMenu = (sender, e) => { };

    private void Start()
    {
        exitButton.onClick.AddListener(delegate
            {
                OnExitToMainMenu(this, EventArgs.Empty);
            });
    }
    private void Update()
    {
        System.Text.StringBuilder strb = new System.Text.StringBuilder();
        if (trackPath.isWin)
        {
            strb.AppendLine("WINNER☺");
        }
        else
        {
            strb.AppendLine("LOOSE.!.");
        }
        foreach (string rawLeader in trackPath.GetFinishTrackLeaderBoard())
        {
            strb.AppendLine(rawLeader);
        }
        text.text = strb.ToString();
    }
}
