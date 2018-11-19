using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SaveGameIdSystem))]
[RequireComponent(typeof(AbilitySystem))]
[RequireComponent(typeof(ClassSystem))]
[RequireComponent(typeof(CharacterSystem))]
[RequireComponent(typeof(HealthSystem))]
public class NpcSystem : MonoBehaviour
{
    public NpcRuntime npcRuntime;

    void Start()
    {
        var gameSystem = FindObjectOfType<GameSystem>();
        gameSystem.npcs.Add(this);
    }

    public void OnSave(MySaveGame saveGame)
    {
        Debug.Log("NpcSystem OnSave: " + GetComponent<SaveGameIdSystem>().SaveGameId);

        var saveGameIdSystem = this.GetComponent<SaveGameIdSystem>();
        var abilitySystem = GetComponent<AbilitySystem>();
        var classSystem = GetComponent<ClassSystem>();
        var healthSystem = GetComponent<HealthSystem>();

        npcRuntime = new NpcRuntime(saveGameIdSystem.SaveGameId, abilitySystem.abilityRuntimes, classSystem.classRuntime);

        npcRuntime.position = transform.position;
        npcRuntime.rotation = transform.rotation;
        npcRuntime.scale = transform.localScale;

        npcRuntime.maxHealth = healthSystem.maxHealth;
        npcRuntime.currentHealth = healthSystem.currentHealth;
        npcRuntime.isAlive = healthSystem.IsAlive;

        saveGame.npcRuntimes.Add(npcRuntime);
    }

    public void OnLoad(MySaveGame saveGame)
    {
        Debug.Log("NpcSystem OnLoad");

        var saveGameIdSystem = this.GetComponent<SaveGameIdSystem>();
        var abilitySystem = GetComponent<AbilitySystem>();
        var classSystem = GetComponent<ClassSystem>();
        var healthSystem = GetComponent<HealthSystem>();

        var runtime = saveGame.npcRuntimes.SingleOrDefault(x => x.saveGameId == saveGameIdSystem.SaveGameId);
        if (runtime == null)
        {
            throw new System.Exception(string.Format("No NpcRuntime for SaveGameId {0}", saveGameIdSystem.SaveGameId));
        }

        transform.position = runtime.position;
        transform.rotation = runtime.rotation;
        transform.localScale = runtime.scale;

        classSystem.classRuntime = runtime.classRuntime;
        abilitySystem.abilityRuntimes = runtime.abilityRuntimes;
        healthSystem.currentHealth = runtime.currentHealth;
        healthSystem.maxHealth = runtime.maxHealth;
        healthSystem.IsAlive = runtime.isAlive;
    }

    void OnEnable()
    {
        var globalEvents = FindObjectOfType<GlobalEvents>();
        if (globalEvents != null)
        {
            globalEvents.onSave += OnSave;
            globalEvents.onLoad += OnLoad;
        }
    }

    void OnDisable()
    {
        var globalEvents = FindObjectOfType<GlobalEvents>();
        if (globalEvents != null)
        {
            globalEvents.onSave -= OnSave;
            globalEvents.onLoad -= OnLoad;
        }
    }

}
