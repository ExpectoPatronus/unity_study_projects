using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Класс для управлением отсчета времени и завершением игры.
/// </summary>
public class TimeManager : MonoBehaviour
{
    public TMP_Text _textTime;  // Ссылка на текстовое поле, в котором будет отображаться время
    public GameObject EndPanel;  // Ссылка на завершающий экран
    public TMP_Text scoreText;  // Ссылка на текстовое поле, в котором будет отображаться очки

    private Player player; // Ссылка на компонент Player
    private Weapon weapon; // Ссылка на компонент Weapon
    private ScoreController _scoreController;  // Ссылка на компонент управления счетом
    private float gameTimeInSeconds = 30f; // Общее время игры в секундах
    private float remainingTime; // Оставшееся время игры

    private void Start()
    {
        Time.timeScale = 1;
        remainingTime = gameTimeInSeconds;

        // Получаем компонент Player из объекта игрока
        player = FindObjectOfType<Player>();

        // Получаем компонент Weapon из объекта оружие
        weapon = FindObjectOfType<Weapon>();

        // Получаем компонент ScoreController для обновления счета
        _scoreController = FindObjectOfType<ScoreController>();

        // Запускаем сопрограмму обновления таймера
        StartCoroutine(UpdateTimer());
    }

    /// <summary>
    /// Сопрограмма для обновления таймера.
    /// </summary>
    private IEnumerator UpdateTimer()
    {
        while (remainingTime > 0)
        {
            // Уменьшаем оставшееся время на одну секунду
            remainingTime -= 1f;

            // Отображаем оставшееся время в текстовом поле
            UpdateTimeText();

            yield return new WaitForSeconds(1f); // Ждем одну секунду перед следующим обновлением времени
        }

        // Время вышло, завершаем игру
        EndGame();
    }

    /// <summary>
    /// Обновление текстового поля с отображением времени.
    /// </summary>
    private void UpdateTimeText()
    {
        // Преобразуем оставшееся время в формат минут:секунды
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        // Форматируем время в текстовую строку
        string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Обновляем текстовое поле с отображением времени
        _textTime.text = "Осталось времени: " + timeText;
    }

    /// <summary>
    /// Метод для завершения игры и отображением заверщающего экрана.
    /// </summary>
    private void EndGame()
    {
        // Включаем панель завершения игры
        EndPanel.SetActive(true);

        // Останавливаем время
        Time.timeScale = 0;

        // Отключаем скрытие и блокировку курсора
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        // Отключаем игрока и оружие
        player.SetGameActive(false);
        weapon.SetGameActive(false);

        // Обновляем текст с итоговым счетом
        scoreText.text = "Итоговый счет: " + _scoreController._score.ToString();

        // Находим всех противников на сцене и удаляем их
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }
}
