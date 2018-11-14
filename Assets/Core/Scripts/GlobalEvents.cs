using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalEvents : MonoBehaviour
{
    public delegate void OnSave(MySaveGame saveGame);
    public event OnSave onSave;

    public delegate void OnLoad(MySaveGame saveGame);
    public event OnLoad onLoad;

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void SaveGameToSlot(string slotName)
    {
        var saveGameSystem = FindObjectOfType<SaveGameSystem>();

        if (!saveGameSystem)
            return;

        var saveGame = new MySaveGame(5, slotName);

        saveGame.sceneName = SceneManager.GetActiveScene().name;

        // TODO: consider changing this to use interfaces
        onSave.Invoke(saveGame);

        saveGameSystem.SaveGameJson(saveGame);
    }

    public void LoadGameFromSlot(string slotName)
    {
        var saveGameSystem = FindObjectOfType<SaveGameSystem>();

        if (!saveGameSystem)
            return;

        var saveGame = (MySaveGame) saveGameSystem.LoadGameJson(slotName);

        // TODO: consider changing this to use interfaces
        onLoad.Invoke(saveGame);
    }
}
