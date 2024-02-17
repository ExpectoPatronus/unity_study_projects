using UnityEngine;

/// <summary>
/// Класс для управления вращением игрока с помощью мыши.
/// </summary>
public class Player : MonoBehaviour
{
    private float _rotationSpeedHor = 5.0f;  // Скорость горизонтального вращения игрока
    private float _rotationSpeedVer = 5.0f;  // Скорость вертикального вращения игрока
    private float _rotationX = 0;            // Угол вращения по оси X
    private float _rotationY = 0;            // Угол вращения по оси Y
    private bool _isGameActive = true;       // Флаг, указывающий, активна ли игра


    void Start()
    {
        // Блокируем курсор в центре экрана и скрываем его
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Проверяем, активна ли игра
        if (_isGameActive)
        {
            // Получаем входные данные от мыши по вертикали (Mouse Y) и изменяем угол вращения по оси X
            _rotationX -= Input.GetAxis("Mouse Y") * _rotationSpeedVer;

            // Ограничиваем угол вращения по оси X от -45 до 45 градусов
            _rotationX = Mathf.Clamp(_rotationX, -45.0f, 45.0f);

            // Получаем входные данные от мыши по горизонтали (Mouse X) и изменяем угол вращения по оси Y
            float delta = Input.GetAxis("Mouse X") * _rotationSpeedHor;
            _rotationY = transform.localEulerAngles.y + delta;

            // Применяем вращение к объекту игрока, изменяя его углы Эйлера
            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        }
    }

    /// <summary>
    /// Устанавливает состояние активности игры.
    /// </summary>
    /// <param name="isActive">Флаг активности игры.</param>
    public void SetGameActive(bool isActive)
    {
        _isGameActive = isActive;
    }
}

