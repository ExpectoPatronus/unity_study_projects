using UnityEngine;
using System.IO;

/// <summary>
/// Статический класс для хранения данных игрока и топ-игроков.
/// </summary>
[System.Serializable]
public static class DataHolder
{
    public static int s_score = 0;  //  Очки игрока.
    public static string s_user_name = "anonymous";  //  Имя игрока, по умолчанию anonymous.
    public static string[] s_top_players_name = { "...", "...", "...", "...", "..." };  //  Массив имен топ-игроков.
    public static string[] s_top_players_score = { "0", "0", "0", "0", "0" };  //  Массив очков топ-игроков.
}

/// <summary>
/// Класс для сериализации данных сохранения игры.
/// </summary>
[System.Serializable]
public class SaveData
{
    public string UserName;
    public string[] TopPlayersName;
    public string[] TopPlayersScore;
}

/// <summary>
/// Класс для управления сохранением и загрузкой данных игры.
/// </summary>
public class SaveLoadManager
{
    /// <summary>
    /// Сохраняет данные игры в формате JSON.
    /// </summary>
    public void SaveData()
    {
        string savePath = Application.persistentDataPath + "/saveData.json"; // Определение пути к файлу сохранения.
        SaveData saveData = new SaveData();
        CopyToSaveData(saveData);
        if (saveData != null)
        {
            string jsonData = JsonUtility.ToJson(saveData);
            File.WriteAllText(savePath, jsonData);
            Debug.Log("Данные сохранены");
        }
        else
        {
            Debug.LogError("saveData is null.");
        }
    }

    /// <summary>
    /// Загружает данные игры из файла сохранения.
    /// </summary>
    public void LoadData()
    {
        string savePath = Application.persistentDataPath + "/saveData.json"; // Определение пути к файлу сохранения.
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(jsonData);
            CopyFromSaveData(saveData);
        }
        else
        {
            Debug.LogWarning("Файл сохранения не найден. Cоздаем новый файл");
            SaveData();
        }
    }

    /// <summary>
    /// Копирует данные из DataHolder в объект SaveData.
    /// </summary>
    private void CopyToSaveData(SaveData saveData)
    {
        saveData.UserName = DataHolder.s_user_name;
        saveData.TopPlayersName = (string[])DataHolder.s_top_players_name.Clone();
        saveData.TopPlayersScore = (string[])DataHolder.s_top_players_score.Clone();
    }


    /// <summary>
    /// Копирует данные из объекта SaveData в DataHolder.
    /// </summary>
    private void CopyFromSaveData(SaveData saveData)
    {
        DataHolder.s_user_name = saveData.UserName;
        DataHolder.s_top_players_name = (string[])saveData.TopPlayersName.Clone();
        DataHolder.s_top_players_score = (string[])saveData.TopPlayersScore.Clone();
    }
}