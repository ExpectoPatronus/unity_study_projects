using System.Collections;
using UnityEngine;

/// <summary>
/// Класс для управления оружием игрока, позволяя выполнять выстрелы.
/// </summary>
public class Weapon : MonoBehaviour
{
    public AudioSource gunshotAudioSource; // Ссылка на компонент аудио для воспроизведения звука выстрела

    private Camera _camera;  // Ссылка на компонент камеры
    private bool _isGameActive = true;       // Флаг, указывающий, активна ли игра
    private float _lastShotTime;  // Время последнего выстрела
    private float fireRate = 0.5f;  // Скорострельность (выстрелов в секунду)

    void Start()
    {
        // Получаем компонент камеры при запуске скрипта
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        // Проверяем, активна ли игра и прошло ли достаточное время с момента последнего выстрела
        if (_isGameActive && Input.GetMouseButtonDown(0) && Time.time - _lastShotTime >= fireRate)
        {
            // Запоминаем время текущего выстрела
            _lastShotTime = Time.time;

            // Запоминаем центр экрана
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

            // Пускаем луч из центра экрана относительно камеры
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit; // Объект для хранения информации о попадании

            // Пускаем луч и проверяем, попали ли мы в какой-либо объект
            if (Physics.Raycast(ray, out hit))
            {
                gunshotAudioSource.Play();  // Воспроизводим звук выстрела

                // Распознаем объект, в который попал луч
                GameObject hitObject = hit.transform.gameObject;

                // Получаем компонент "мишень" из объекта, в который попал луч
                Target target = hitObject.GetComponent<Target>();

                if (target != null) // Проверяем, является ли объект мишенью
                {
                    target.GetReward(); // Обновляем значение очков на экране
                    target.ReactToHit();  // Вызываем реакцию на попадание в мишень
                } else
                {
                    // Запускаем сопрограмму для создания индикатора попадания
                    StartCoroutine(SphereInicatorCoroutine(hit.point));

                    // Рисуем отладочную линию, чтобы проследить траекторию луча
                    Debug.DrawLine(this.transform.position, hit.point, Color.green, 6);
                }
            }
        }
    }

    /// <summary>
    /// Отображает визуальный индикатор в центре экрана.
    /// </summary>
    private void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    /// <summary>
    /// Сопрограмма, которая создает сферу в месте попадания и уничтожает её через заданное время.
    /// </summary>
    /// <param name="pos">Позиция, где следует создать сферу.</param>
    private IEnumerator SphereInicatorCoroutine(Vector3 pos)
    {
        // Создаем игровой объект - сферу
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;  // Устанавливаем позицию сферы
        yield return new WaitForSeconds(0.5f);  // Ждем 0.5 секунды перед уничтожением
        Destroy(sphere);  // Удаляем сферу
    }

    /// <summary>
    /// Устанавливает состояние игры (активна или нет).
    /// </summary>
    /// <param name="isActive">Флаг, указывающий, активна ли игра.</param>
    public void SetGameActive(bool isActive)
    {
        _isGameActive = isActive;
    }
}
