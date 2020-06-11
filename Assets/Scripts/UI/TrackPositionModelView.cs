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

    private void Update()
    {
        System.Text.StringBuilder strb = new System.Text.StringBuilder();
 
        foreach (string rawLeader in trackPath.GetShipPositionTable())
        {
            strb.AppendLine(rawLeader);
        }
        textMeshPro.SetText(strb);
    }
}
