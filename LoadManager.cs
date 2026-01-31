using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    void Start()
    {
        if (!SaveSystem.HasSave())
            return;

        SaveData data = SaveSystem.LoadGame();
        StartCoroutine(LoadAndApply(data));
    }

    System.Collections.IEnumerator LoadAndApply(SaveData data)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(data.sceneName);

        while (!load.isDone)
            yield return null;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Load failed: Player not found");
            yield break;
        }

        // Position
        player.transform.position =
            new Vector3(data.posX, data.posY, data.posZ);

        // Health
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null)
            health.currentHealth = data.health;

        // Gold (USING YOUR SCRIPT)
        if (PlayerCurrency.Instance != null)
        {
            PlayerCurrency.Instance.gold = data.gold;
            // force UI refresh
            PlayerCurrency.Instance.SendMessage("UpdateUI", SendMessageOptions.DontRequireReceiver);
        }

        // Trophies
        PlayerTrophies trophies = player.GetComponent<PlayerTrophies>();
        if (trophies != null)
        {
            trophies.trophyCount = data.trophies;
            trophies.SendMessage("UpdateUI", SendMessageOptions.DontRequireReceiver);
        }

    }
}

