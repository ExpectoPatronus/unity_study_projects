using UnityEngine;
using TMPro;

/// <summary>
/// Класс для управления счетом игрока и отображением его на экране.
/// </summary>
public class ScoreController : MonoBehaviour
{
        public int _score = 0;
        public TMP_Text _textScore;

    void Start()
    {
        _score = 0;
        UpdateScore(); // Обновляем счет при старте, чтобы отображать "Очки: 0"
    }

    /// <summary>
    /// Обновляет отображение счета и сохраняет новое значение счета.
    /// </summary>
    /// <param name="score">Новое значение счета (по умолчанию 0).</param>
    public void UpdateScore(int score = 0)
    {
        _score = score;

        // Гарантируем, что счет не может быть отрицательным
        if (_score < 0) {
            _score = 0;
        }

        // Обновляем текстовое поле с отображением счета
        _textScore.text = "Очки: " + _score.ToString();
    }

    /// <summary>
    /// Возвращает текущее значение счета.
    /// </summary>
    /// <returns>Текущий счет.</returns>
    public int GetScore()
    {
        return _score;
    }
}
