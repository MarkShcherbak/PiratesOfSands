using UnityEngine;

/// <summary>
/// Класс контролирует ввод данных от пользователя.
/// </summary>
public class InputControl : Singleton<InputControl>
{
    private CinemachineModelView cinemachineModelView;

    const string changeCam = "ChangeCam";
    private void Start()
    {
        cinemachineModelView = CinemachineModelView.Instance;
    }
    void Update()
    {
        KeyInputUpdate();
        ProcedureCaller();
    }
   
    /// <summary>
    /// Опрашивает вводные данные от пользователя
    /// </summary>
    private void KeyInputUpdate()
    {

        InputParams.XAxis = Input.GetAxis("Horizontal");
        InputParams.ZAxis = Input.GetAxis("Vertical");


        if ((Input.GetButtonDown(changeCam)))
        {
            InputParams.ChangeCamButtonDown = true;
        }

    }

    /// <summary>
    /// Вызов дополнительных процедур по факту нажатия
    /// </summary>
    private void ProcedureCaller()
    {
        ///проверка переключения камеры на следующую
        if (InputParams.ChangeCamButtonDown)
        {
            cinemachineModelView.NextCam();
        }
    }

    /// <summary>
    /// процедура для вызова смены камера из других мест
    /// </summary>
    public void ChangeCamButtonDown()
    {
        InputParams.ChangeCamButtonDown = true;
    }

}

public static class InputParams
{
    private static float zAxis;
    private static float xAxis;
    private static bool changeCamButtonDown;

    public static float ZAxis { get => zAxis; set => zAxis = value; }
    public static float XAxis { get => xAxis; set => xAxis = value; }
    
    /// <summary>
    /// Проверяет нажатие кноки, если она нажата, возвращает true, но состояние переходит в false
    /// </summary>
    public static bool ChangeCamButtonDown
    {
        get
        {
            if (changeCamButtonDown)
            {
                changeCamButtonDown = false;
                return true;
            }
            else
            {
                return false;
            }
            
        }

        set => changeCamButtonDown = value;
    }


}
