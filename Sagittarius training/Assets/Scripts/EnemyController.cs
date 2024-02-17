using System.Collections;
using UnityEngine;

/// <summary>
/// Класс, управляющий спавном и рождением противников в игре.
/// </summary>
public class EnemyController : MonoBehaviour
{
    /* Массив префабов противников. Состоит из трех черных противников и одного желтого противника.
       Он представляет собой смесь разных типов мишеней, где 25% мишеней являются желтыми, 
       а 75% мишеней - черными. */
    public GameObject[] enemyPrefabs; 
    public Transform[] spawnPoints; // Массив точек старта

    private float minSpawnDelay = 1f; // Минимальная задержка между спавном противников
    private float maxSpawnDelay = 2f; // Максимальная задержка между спавном противников

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    /// <summary>
    /// Сопрограмма для рождения противников. Генерирует случайные противники на случайных точках старта
    /// </summary>
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Генерируем случайную задержку перед спавном
            float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(spawnDelay);

            // Выбираем случайного противника из массива префабов
            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyPrefab = enemyPrefabs[randomEnemyIndex];

            // Выбираем случайную точку старта из массива
            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomSpawnPointIndex];

            float DistanceToLine = 33.0f; // Расстояние между точкой образования моба и линией

            // Проверяем, есть ли препятствия на пути спавна противника
            bool isOccupied = Physics.Raycast(spawnPoint.position, Vector3.forward, DistanceToLine);

            // Если нет препятствий, создаем нового противника
            if (!isOccupied)
            {
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    }
}

