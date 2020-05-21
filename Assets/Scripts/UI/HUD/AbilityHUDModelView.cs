using UnityEngine;
using UnityEngine.UI;

    public class AbilityHUDModelView : MonoBehaviour
    {
    public GameObject AbilityIconContainerPrefab;
    public GameObject ArrowPrefab;

    public Image MasterSlot;
    public GameObject LeftSlots;
    [HideInInspector] public Image[] LeftImages;
    public GameObject LeftArrows;
    public GameObject RightSlots;
    [HideInInspector] public Image[] RightImages;
    public GameObject RightArrows;
    public GameObject ForwardSlots;
    [HideInInspector] public Image[] ForwardImages;
    public GameObject ForwardArrows;
    public GameObject BackSlots;
    [HideInInspector] public Image[] BackImages;
    public GameObject BackArrows;

    public void AddContainers
        (ICannonModelView[] left, ICannonModelView[] right, ICannonModelView[] forward, ICannonModelView[] back)
    {
        LeftImages = new Image[left.GetLength(0)];

        for (int i = 0; i < left.GetLength(0); i++)
        {
            LeftImages[i] = Instantiate(AbilityIconContainerPrefab, LeftSlots.transform).GetComponent<Image>();
            Instantiate(ArrowPrefab, LeftArrows.transform);
        }

        RightImages = new Image[right.GetLength(0)];

        for (int i = 0; i < right.GetLength(0); i++)
        {
            RightImages[i] = Instantiate(AbilityIconContainerPrefab, RightSlots.transform).GetComponent<Image>();
            Instantiate(ArrowPrefab, RightArrows.transform);
        }

        ForwardImages = new Image[forward.GetLength(0)];

        for (int i = 0; i < forward.GetLength(0); i++)
        {
            ForwardImages[i] = Instantiate(AbilityIconContainerPrefab, ForwardSlots.transform).GetComponent<Image>();
            Instantiate(ArrowPrefab, ForwardArrows.transform);
        }

        BackImages = new Image[back.GetLength(0)];

        for (int i = 0; i < back.GetLength(0); i++)
        {
            BackImages[i] = Instantiate(AbilityIconContainerPrefab, BackSlots.transform).GetComponent<Image>();
            Instantiate(ArrowPrefab, BackArrows.transform);
        }
    }


    }
