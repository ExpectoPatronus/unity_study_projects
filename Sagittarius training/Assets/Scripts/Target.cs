using System.Collections;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за реакцию на попадание и смерть целей.
/// </summary>
public class Target : MonoBehaviour
{
    private Enemy _enemy; // Ссылка на компонент Enemy

    private void Start()
    {
        // Получаем данные о компоненте Enemy
        _enemy = GetComponent<Enemy>();
    }

    /// <summary>
    /// Реакция на попадание.
    /// </summary>
    public void ReactToHit()
    {
        // Если такой компонент существует
        if (_enemy != null)
            _enemy.SetAlive(false); // Вызываем его открытый метод SetAlive с параметром false

        // Запускаем сопрограмму для смерти
        StartCoroutine(DieCoroutine(0.5f));
    }

    /// <summary>
    /// Получаем очки за убийство.
    /// </summary>
    public void GetReward()
    {
        _enemy.GiveReward();
    }

    /// <summary>
    /// Сопрограмма для смерти.
    /// </summary>
    /// <param name="waitSeconds">Время задержки перед уничтожением объекта.</param>
    private IEnumerator DieCoroutine(float waitSeconds)
    {
        // Поворачиваем объект, имитируя попадание
        this.transform.Rotate(-45, 0, 0);

        // Ждем указанное количество секунд
        yield return new WaitForSeconds(waitSeconds);

        // Уничтожаем объект
        Destroy(this.gameObject);
    }
}
