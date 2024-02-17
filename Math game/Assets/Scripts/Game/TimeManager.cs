using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Класс для управления отсчетом времени перед началом игры и запуском уровня.
/// </summary>
public class TimeManager : MonoBehaviour
{
    public TMP_Text countdownText; // Ссылка на текстовое поле для отображения отсчета времени

    private float _gameTimeInSeconds = 60f; // Длительность отсчета в секундах
    private float _remainingTime; // Оставшееся время игры
    private const string _gameSceneName = "Menu";  // Имя сцены для запуска игры.


    void Start()
    {
        // Устанавливаем нормальное время в игре (1.0 - обычная скорость времени)
        Time.timeScale = 1;
        _remainingTime = _gameTimeInSeconds;

        // Запускаем сопрограмму обновления таймера
        StartCoroutine(UpdateTimer());
    }

    /// <summary>
    /// Изменяет оставшееся время на заданное количество секунд.
    /// </summary>
    /// <param name="num">Количество секунд для изменения времени.</param>
    public void ChangeRemainingTime(int num)
    {
        _remainingTime += num;
        if (_remainingTime > 60f)
        {
            _remainingTime = 60f;
        }
    }

    /// <summary>
    /// Сопрограмма для обновления таймера.
    /// </summary>
    private IEnumerator UpdateTimer()
    {
        while (_remainingTime > 0)
        {
            // Отображаем оставшееся время в текстовом поле
            UpdateTimeText();

            // Уменьшаем оставшееся время на одну секунду
            _remainingTime -= 1f;
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
        int minutes = Mathf.FloorToInt(_remainingTime / 60);
        int seconds = Mathf.FloorToInt(_remainingTime % 60);

        // Форматируем время в текстовую строку
        string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Обновляем текстовое поле с отображением времени
        countdownText.text = "Осталось времени: " + timeText;
    }

    /// <summary>
    /// Выполняется выход в главное меню.
    /// </summary>
    public void EndGame()
    {
        UpdateTopPlayers();
        SaveLoadManager saveLoadManager = new SaveLoadManager();
        saveLoadManager.SaveData();

        // Загрузка сцены с игрой
        SceneManager.LoadScene(_gameSceneName);
    }


    private void UpdateTopPlayers()
    {
        // Проверяем, заслуживает ли игрок входа в топ игроков
        for (int i = 0; i < DataHolder.s_top_players_score.Length; i++)
        {
            int topPlayerScore = int.Parse(DataHolder.s_top_players_score[i]);

            if (DataHolder.s_score > topPlayerScore)
            {
                // Игрок заслуживает входа в топ
                // Сдвигаем текущих топ игроков ниже
                for (int j = DataHolder.s_top_players_score.Length - 1; j > i; j--)
                {
                    DataHolder.s_top_players_name[j] = DataHolder.s_top_players_name[j - 1];
                    DataHolder.s_top_players_score[j] = DataHolder.s_top_players_score[j - 1];
                }

                // Добавляем игрока в топ
                DataHolder.s_top_players_name[i] = DataHolder.s_user_name; // Замените на имя игрока
                DataHolder.s_top_players_score[i] = DataHolder.s_score.ToString();
                break; // Выходим из цикла, так как игрок уже добавлен
            }
        }
    }
}
