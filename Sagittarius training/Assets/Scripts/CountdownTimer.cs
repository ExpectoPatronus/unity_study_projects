using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс для управления отсчетом времени перед началом игры и запуском уровня.
/// </summary>
public class CountdownTimer : MonoBehaviour
{
    public TMP_Text countdownText; // Ссылка на текстовое поле для отображения отсчета
    private float countdownDuration = 3; // Длительность отсчета в секундах

    void Start()
    {
        // Устанавливаем нормальное время в игре (1.0 - обычная скорость времени)
        Time.timeScale = 1;

        // Обновляем текстовое поле отсчета времени, устанавливая начальное значение
        countdownText.text = countdownDuration.ToString();
    }

    void Update()
    {
        // Вычитаем прошедшее время (deltaTime) из текущей длительности отсчета (countdownDuration).
        countdownDuration -= Time.deltaTime;

        // Обновляем текст на экране, преобразуя текущую длительность отсчета в строку и округляя до целого числа.
        countdownText.text = Mathf.Round(countdownDuration).ToString();

        // Проверяем, если оставшаяся длительность отсчета стала менее чем 1 секунда.
        if (countdownDuration < 1)
        {
            // Вызываем метод PerformRestart() для запуска уровня.
            PerformRestart();
        }
    }

    /// <summary>
    /// Выполняет перезапуск текущего уровня.
    /// </summary>
    private void PerformRestart()
    {
        // Загрузка сцены с игрой
        SceneManager.LoadScene("SampleScene");
    }
}
