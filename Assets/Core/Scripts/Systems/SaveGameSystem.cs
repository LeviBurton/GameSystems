using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSlot
{
    public string slotName;
    public DateTime slotDate;
    public int slotNum;

    public SaveSlot(int slotNum, string slotName, DateTime slotDate)
    {
        this.slotNum = slotNum;
        this.slotName = slotName;
        this.slotDate = slotDate;
    }
}

public class SaveGameSystem : MonoBehaviour
{
    public string savePath = @"/Saves";
    public string defaultSlotName = "Save";

    public RuntimePrefabsConfig runtimePrefabSettings;

    public List<SaveSlot> GetSaveSlots()
    {
        var slots = new List<SaveSlot>();

        var dirInfo = new DirectoryInfo(GetSlotSavePath());
        var fileInfos = dirInfo.GetFiles().OrderBy(p => p.CreationTime).ToList();

        for (int i = 0; i < fileInfos.Count; i++)
        {
            var fileInfo = fileInfos[i];

            slots.Add(new SaveSlot(i + 1, Path.GetFileNameWithoutExtension(fileInfo.Name), fileInfo.CreationTime));
        }

        return slots.Reverse<SaveSlot>().ToList();
    }

    public string GetNewSaveSlotName()
    {
        string name = defaultSlotName;
        var currentNames = GetSaveSlotNames();

        int i = 1;

        while (currentNames.Contains(name))
        {
            name = string.Format("{0} ({1})", defaultSlotName, i++);
        }

        return name;
    }

    public List<string> GetSaveSlotNames()
    {
        var names = new List<string>();

        DirectoryInfo dirInfo = new DirectoryInfo(GetSlotSavePath());
        var fileInfos = dirInfo.GetFiles().OrderByDescending(p => p.CreationTime).ToList();

        foreach (var fileInfo in fileInfos)
        {
            names.Add(Path.GetFileNameWithoutExtension(fileInfo.Name));
        }

        return names;
    }

    public MySaveGame LoadGameJson(string slotName)
    {
        var slotPath = GetSlotJsonPath(slotName);

        Debug.LogFormat("Loading Game from slot: {0}", slotPath);

        MySaveGame saveGame = null;

        try
        {
            saveGame = JsonUtility.FromJson<MySaveGame>(File.ReadAllText(slotPath));
        }
        catch (Exception ex)
        {
            throw ex;
        }

        if (!string.IsNullOrEmpty(saveGame.sceneName))
        {
            // Load the scene
            var foo = SceneManager.LoadSceneAsync(saveGame.sceneName, LoadSceneMode.Single);
        }

        // Load all of our save game things.  Note that this could be refactored at some point since
        // there is a lot of code duplication


        // Setup HeroSystems
        var heroes = FindObjectsOfType<HeroSystem>();
        foreach (var heroRuntime in saveGame.heroRuntimes)
        {
            HeroRuntime foundHeroRuntime = null;
            HeroSystem foundHeroSystem = null;

            foreach (var hero in heroes)
            {
                var heroSaveGameId = hero.GetComponent<SaveGameIdSystem>().SaveGameId;
                if (heroSaveGameId == heroRuntime.saveGameId)
                {
                    foundHeroRuntime = heroRuntime;
                    foundHeroSystem = hero.GetComponent<HeroSystem>();
                    break;
                }
            }

            if (foundHeroRuntime == null)
            {
                var spawnedHero = Instantiate(runtimePrefabSettings.heroPrefab, heroRuntime.position, heroRuntime.rotation);
                spawnedHero.transform.localScale = heroRuntime.scale;
                spawnedHero.GetComponent<SaveGameIdSystem>().SaveGameId = heroRuntime.saveGameId;
                spawnedHero.GetComponent<HeroSystem>().OnLoad(saveGame);
            }
            else
            {
                foundHeroSystem.OnLoad(saveGame);
            }
        }



        // Setup EnemySystems
        var enemies = FindObjectsOfType<EnemySystem>();
        foreach (var runtime in saveGame.enemyRuntimes)
        {
            EnemyRuntime foundRuntime = null;
            EnemySystem foundSystem = null;

            foreach (var enemy in enemies)
            {
                var enemySaveGameId = enemy.GetComponent<SaveGameIdSystem>().SaveGameId;
                if (enemySaveGameId == runtime.saveGameId)
                {
                    foundRuntime = runtime;
                    foundSystem = enemy.GetComponent<EnemySystem>();
                    break;
                }
            }

            if (foundRuntime == null)
            {
                var spawned = Instantiate(runtimePrefabSettings.heroPrefab, runtime.position, runtime.rotation);
                spawned.transform.localScale = runtime.scale;
                spawned.GetComponent<SaveGameIdSystem>().SaveGameId = runtime.saveGameId;
                spawned.GetComponent<EnemySystem>().OnLoad(saveGame);
            }
            else
            {
                foundSystem.OnLoad(saveGame);
            }
        }

        return saveGame;
    }

    public bool SaveGameJson(SaveGame saveGame)
    {
        var slotPath = GetSlotJsonPath(saveGame.slotName);
        var json = JsonUtility.ToJson(saveGame, true);

        Directory.CreateDirectory(Path.GetDirectoryName(slotPath));

        Debug.LogFormat("Saving Game to slot: {0}", slotPath);

        try
        {
            File.WriteAllText(slotPath, json);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return true;
    }

    public SaveGame LoadGame(string slotName)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        var slotPath = GetSlotPath(slotName);

        Debug.LogFormat("Loading Game from slot: {0}", slotPath);

        using (FileStream stream = new FileStream(slotPath, FileMode.Open))
        {
            try
            {
                return formatter.Deserialize(stream) as SaveGame;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public bool SaveGame(SaveGame saveGame)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        var slotPath = GetSlotPath(saveGame.slotName);
        saveGame.slotName = slotPath;

        Debug.LogFormat("Saving Game to slot: {0}", slotPath);

        // create the diretory if it does not exist.  does nothing if already exists.
        Directory.CreateDirectory(Path.GetDirectoryName(slotPath));

        using (FileStream stream = new FileStream(slotPath, FileMode.Create))
        {
            try
            {
                formatter.Serialize(stream, saveGame);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        return true;
    }

    public bool DeleteSaveGameFromSlot(string slotName)
    {
        try
        {
            File.Delete(GetSlotPath(slotName));
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public bool DoesSaveGameExist(string slotPath)
    {
        return File.Exists(GetSlotPath(slotPath));
    }

    private string GetSlotSavePath()
    {
        return Path.Combine(Application.persistentDataPath, savePath);
    }


    private string GetSlotJsonPath(string slotName)
    {
        if (slotName == string.Empty)
        {
            slotName = defaultSlotName;
        }

        return Path.Combine(GetSlotSavePath(), slotName + ".json");
    }

    private string GetSlotPath(string slotName)
    {
        if (slotName == string.Empty)
        {
            slotName = defaultSlotName;
        }

        return Path.Combine(GetSlotSavePath(), slotName + ".sav");
    }
}
