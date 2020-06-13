using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Завершение гонки
/// </summary>
public class TrackPositionModelView : MonoBehaviour
{
    public TrackPath trackPath;
    public TextMeshProUGUI textMeshPro;
    public TextMeshProUGUI place;

    private void Update()
    {
        System.Text.StringBuilder strb = new System.Text.StringBuilder();

        int position;
        foreach (string rawLeader in trackPath.GetShipPositionTable(out position))
        {
            strb.AppendLine(rawLeader);
        }
        textMeshPro.SetText(strb);

        place.SetText(position.ToString());
    }
}
