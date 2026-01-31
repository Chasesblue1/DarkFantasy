using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public static class SaveSystem
{
    private static string savePath =
        Application.persistentDataPath + "/save.json";

    public static void SaveGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Save failed: Player not found");
            return;
        }

        SaveData data = new SaveData();

        // Scene
        data.sceneName = SceneManager.GetActiveScene().name;

        // Position
        Vector3 pos = player.transform.position;
        data.posX = pos.x;
        data.posY = pos.y;
        data.posZ = pos.z;

        // Health
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        data.health = health != null ? health.currentHealth : 100;

        // Gold (USING YOUR SCRIPT)
        if (PlayerCurrency.Instance != null)
            data.gold = PlayerCurrency.Instance.gold;
        else
            data.gold = 0;

        // Trophies
        PlayerTrophies trophies = player.GetComponent<PlayerTrophies>();
        data.trophies = trophies != null ? trophies.trophyCount : 0;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Game Saved Successfully");
    }

    public static bool HasSave()
    {
        return File.Exists(savePath);
    }

    public static SaveData LoadGame()
    {
        if (!HasSave())
            return null;

        string json = File.ReadAllText(savePath);
        return JsonUtility.FromJson<SaveData>(json);
    }
}

