using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


/// <summary>
/// Управляет меню игры, включая ввод имени игрока, отображение результатов и запуск игры.
/// </summary>
public class Menu : MonoBehaviour
{
    public TMP_InputField playerName;     // Ссылка на поле ввода имени игрока.
    public TMP_Text textResult;           // Ссылка на текст для отображения результатов игрока.
    public TMP_Text[] topPlayersName;    // Ссылка на текстовые поля для отображения имен топ-игроков.
    public TMP_Text[] topPlayersScore;   // Ссылка на текстовые поля для отображения очков топ-игроков.

    private SaveLoadManager _saveLoadManager;  // Менеджер для сохранения и загрузки данных.
    private const string _gameSceneName = "Game";  // Имя сцены для запуска игры.


    private void Awake()
    {
        _saveLoadManager = new SaveLoadManager();
    }

    void Start()
    {
        // Загружаем сохраненные данные при запуске меню.
        _saveLoadManager.LoadData();

        // Устанавливаем имя игрока и отображаем результаты.
        playerName.text = DataHolder.s_user_name;
        UpdateResultText(DataHolder.s_score);
        SetTopPlayers(topPlayersName, DataHolder.s_top_players_name);
        SetTopPlayers(topPlayersScore, DataHolder.s_top_players_score);
    }

    /// <summary>
    /// Запускает игру, сохраняя введенное имя игрока.
    /// </summary>
    public void StartGame()
    {
        DataHolder.s_user_name = playerName.text;
        SceneManager.LoadScene(_gameSceneName);
    }


    /// <summary>
    /// Выходит из игры, сохраняя данные игрока.
    /// </summary>
    public void ExitGame()
    {
        _saveLoadManager.SaveData();
        Application.Quit();
    }

    /// <summary>
    /// Обновляет текст для отображения результатов игрока.
    /// </summary>
    private void UpdateResultText(int score)
    {
        textResult.text = string.Format("Ваш результат - {0}", score);
    }

    /// <summary>
    /// Устанавливает текст для отображения топ-игроков.
    /// </summary>
    private void SetTopPlayers(TMP_Text[] textFields, string[] topPlayers)
    {
        if (textFields.Length == topPlayers.Length)
        {
            for (int i = 0; i < textFields.Length; i++)
            {
                textFields[i].text = topPlayers[i];
            }
        }
    }

}
