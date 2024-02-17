using System.Collections;
using UnityEngine;

/// <summary>
/// Класс, представляющий вражеский объект в игре.
/// </summary>
public class Enemy : MonoBehaviour
{
    public float _lifeTime;  // Время жизни врага (в секундах)
    public int _reward;  // Награда за убийство врага

    private float speed = 10.0f; // Скорость движения врага
    private bool _alive = true; // Флаг, указывающий, жив ли враг
    private ScoreController _scoreController; // Ссылка на компонент управления счетом

    private void Start()
    {
        _alive = true; // Инициализируем состояние врага как живой

        // Запускаем сопрограмму для уничтожения врага после истечения времени жизни
        StartCoroutine(DestroyAfterLifetime());

        // Находим компонент ScoreController для обновления счета
        _scoreController = FindObjectOfType<ScoreController>();

    }

    private void Update()
    {
        if (_alive)
        {
            // Непрерывное движение вперед
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Получить награду за убийство врага.
    /// </summary>
    /// <returns>Награда в виде количества очков.</returns>
    public int GetReward()
    {
        return _reward;
    }

    /// <summary>
    /// Метод для установки состояния жизни врага.
    /// </summary>
    /// <param name="alive">True, если враг жив, в противном случае - false.</param>
    public void SetAlive(bool alive)
    {
        _alive = alive;
    }

    /// <summary>
    /// Сопрограмма для уничтожения врага через указанное время.
    /// </summary>
    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);

        // Убедимся, что есть ссылка на компонент управления счетом
        if (_scoreController != null)
        {
            // Уменьшаем счет, если не успели убить врага
            _scoreController.UpdateScore(_scoreController.GetScore() - 1);
        }
    }

    /// <summary>
    /// Наградить игрока за убийство врага.
    /// </summary>
    public void GiveReward()
    {
        // Убедимся, что есть ссылка на компонент управления счетом
        if (_scoreController != null)
        {
            // Увеличиваем счет при награждении игрока за убийство
            _scoreController.UpdateScore(_scoreController.GetScore() + _reward);
        }
    }
}

