using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс для управлением перезапуска текущего уровня.
/// </summary>
public class RestartLevel : MonoBehaviour
{
    /// <summary>
    /// Вызывается для выполнения перезапуска текущего уровня.
    /// </summary>
    public void PerformRestart()
    {
        SceneManager.LoadScene("StartView");
    }
}
