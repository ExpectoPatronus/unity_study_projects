using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/// <summary>
/// Управляет игровым процессом и интерфейсом.
/// </summary>
public class Game : MonoBehaviour
{
    [SerializeField] private Image imagePause; // Ссылка на изображение паузы в интерфейсе.
    [SerializeField] private Sprite[] spritePause; // Массив спрайтов для изображения паузы.
    [SerializeField] private GameObject pausePanel; // Панель паузы.
    [SerializeField] private Animator animator; // Компонент аниматора для анимаций.

    [SerializeField] private TMP_Text[] textAnswers; // Массив текстовых элементов для вариантов ответов.
    [SerializeField] private TMP_Text textTask; // Текстовый элемент для отображения задачи.
    [SerializeField] private TMP_Text textLevel; // Текстовый элемент для отображения текущего уровня.
    [SerializeField] private TMP_Text textScore; // Текстовый элемент для отображения текущего счета.

    private int _correctAnswer = 0; // Правильный ответ на текущую задачу.
    private int _level = 1; // Текущий уровень игры.
    private bool _isPause = false; // Флаг для определения, включена ли пауза.

    private TimeManager _timeManager; // Ссылка на менеджер времени.

    void Start()
    {
        InitializeGame();
    }

    /// <summary>
    /// Инициализирует начальное состояние игры.
    /// </summary>
    private void InitializeGame()
    {
        pausePanel.SetActive(_isPause);
        CreateTask();
        DataHolder.s_score = 0;
        _timeManager = FindObjectOfType<TimeManager>();
    }

    /// <summary>
    /// Приостанавливает или возобновляет игру.
    /// </summary>
    public void Pause()
    {
        _isPause = !_isPause;

        if (_isPause)
        {
            imagePause.sprite = spritePause[1];
            pausePanel.SetActive(_isPause);
            Time.timeScale = 0f;
            return;
        }

        imagePause.sprite = spritePause[0];
        pausePanel.SetActive(_isPause);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Завершает игру и возвращает в главное меню.
    /// </summary>
    public void ExitToMenu()
    {
        _timeManager.EndGame();
    }

    /// <summary>
    /// Обрабатывает выбор игрока и обновляет игровой процесс.
    /// </summary>
    /// <param name="text_answer">Выбранный ответ.</param>
    public void ClickOnAnswer(TMP_Text text_answer)
    {
        if (text_answer.text == _correctAnswer.ToString())
        {
            animator.SetTrigger("RightAnswer");
            DataHolder.s_score += 5 + _level / 10 * 2;
            _level += 1;
            IncreaseTime();
        } else
        {
            animator.SetTrigger("WrongAnswer");
            DecreaseTime();
        }        
        CreateTask();
        UpdateLevelAndScoreUI();
    }

    /// <summary>
    /// Обновляет отображение уровня и очков.
    /// </summary>
    private void UpdateLevelAndScoreUI()
    {
        textLevel.text = "Уровень - " + _level.ToString();
        textScore.text = "Очки - " + DataHolder.s_score.ToString();
    }

    /// <summary>
    /// Увеличивает оставшееся время в зависимости от уровня.
    /// </summary>
    private void IncreaseTime()
    {
        _timeManager.ChangeRemainingTime(Mathf.Max(0, 5 - _level / 10));
    }

    /// <summary>
    /// Уменьшает оставшееся время на уровне.
    /// </summary>
    private void DecreaseTime()
    {
        _timeManager.ChangeRemainingTime(-7);
    }

    /// <summary>
    /// Генерирует новую задачу для игрока.
    /// </summary>
    private void CreateTask()
    {
        textTask.text = "";
        var operation_type = new Dictionary<int, string>()
        {
            { 0, "+"},
            { 1, "-"}
        };
        int num1 = Random.Range(-10, 11);
        _correctAnswer = num1;
        textTask.text = num1.ToString();
        for (int i = 0; i <= _level / 7; i++) {
            int operation = Random.Range(0, 2);
            int num2 = Random.Range(-10, 11);

            _correctAnswer = (operation == 0) ? _correctAnswer + num2 : _correctAnswer - num2;

            if (operation == 0 && num2 < 0)
            {
                textTask.text += operation_type[operation + 1] + Mathf.Abs(num2).ToString();
            } else if (operation == 1 && num2 < 0)
            {
                textTask.text += operation_type[operation - 1] + Mathf.Abs(num2).ToString();
            } else
            {
                textTask.text += operation_type[operation] + num2.ToString();
            }
        }
        InitializeAnswerChoices();
        Debug.Log(_correctAnswer);
    }

    /// <summary>
    /// Инициализирует варианты ответов для задачи.
    /// </summary>
    private void InitializeAnswerChoices()
    {
        int correctAnswerIndex = Random.Range(0, 3);

        for (int i = 0; i < textAnswers.Length; i++)
        {
            int randomAnswer;

            do
            {
                randomAnswer = Random.Range(_correctAnswer - 10, _correctAnswer + 11);
            } while (randomAnswer == _correctAnswer);

            textAnswers[i].text = randomAnswer.ToString();
        }

        textAnswers[correctAnswerIndex].text = _correctAnswer.ToString();
    }
}
