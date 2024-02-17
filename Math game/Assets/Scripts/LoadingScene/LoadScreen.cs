using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// Управляет экраном загрузки и асинхронной загрузкой сцены.
/// </summary>
public class LoadScreen : MonoBehaviour
{
    // Ссылки на объекты и константы для имен
    private GameObject _loadingScreen;  // Ссылка на экран загрузки
    private Slider _loadingProgress;  // Ссылка на ползунок прогресса загрузки
    private const string _loadingScreenName = "LoadingScreen";
    private const string _loadSliderName = "LoadSlider";
    private const string _loadLevelName = "Menu";


    /// <summary>
    /// Вызывается при старте сцены. Ищет и проверяет объекты и компоненты.
    /// </summary>
    private void Awake()
    {
        // Поиск объектов в сцене
        _loadingScreen = GameObject.Find(_loadingScreenName);
        _loadingProgress = GameObject.Find(_loadSliderName).GetComponent<Slider>();

        // Проверка наличия объектов и компонентов
        if (_loadingScreen == null || _loadingProgress == null)
        {
            Debug.LogError("GameObject not found!");
        }
    }


    /// <summary>
    /// Вызывается при старте сцены. Запускает загрузку сцены.
    /// </summary>
    private void Start()
    {
        LoadScene();
    }


    /// <summary>
    /// Начинает загрузку сцены, активируя экран загрузки.
    /// </summary>
    public void LoadScene()
    {
        // Активация экрана загрузки и запуск асинхронной загрузки сцены
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync());
    }

    /// <summary>
    /// Асинхронно загружает сцену и обновляет прогресс.
    /// </summary>
    IEnumerator LoadSceneAsync()
    {
        AsyncOperation loadAsyn = SceneManager.LoadSceneAsync(_loadLevelName);
        loadAsyn.allowSceneActivation = false;

        while (!loadAsyn.isDone)
        {
            _loadingProgress.value = loadAsyn.progress;

            // Проверка завершения загрузки и разрешение перехода
            if (loadAsyn.progress >= 0.9f && !loadAsyn.allowSceneActivation)
            {
                yield return new WaitForSeconds(1.0f);
                loadAsyn.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
