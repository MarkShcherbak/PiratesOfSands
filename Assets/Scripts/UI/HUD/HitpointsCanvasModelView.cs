using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HitpointsCanvasModelView : MonoBehaviour
{

    [SerializeField] private Text hpText;
    [SerializeField] private Image greenBar;

    public Text HPAmount {get => hpText;set => hpText = value;}
    public float GreenBarFill { get => greenBar.fillAmount; set => greenBar.fillAmount = value; }

    private void Start()
    {
        hpText.text = "100%";
        greenBar.fillAmount = 1;
    }
}
