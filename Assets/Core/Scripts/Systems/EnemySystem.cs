using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SaveGameIdSystem))]
[RequireComponent(typeof(AbilitySystem))]
[RequireComponent(typeof(ClassSystem))]
[RequireComponent(typeof(HealthSystem))]
[RequireComponent(typeof(CharacterSystem))]
public class EnemySystem : MonoBehaviour
{
    public EnemyRuntime enemyRuntime;

    void Start()
    {
        enemyRuntime = new EnemyRuntime(GetComponent<SaveGameIdSystem>().SaveGameId, null, null);

        var gameSystem = FindObjectOfType<GameSystem>();
        gameSystem.enemies.Add(this);
    }

    void OnEnable()
    {
        var globalEvents = FindObjectOfType<GlobalEvents>();
        if (globalEvents != null)
        {
            globalEvents.onSave += OnSave;
            //globalEvents.onLoad += OnLoad;
        }
    }

    void OnDisable()
    {
        var globalEvents = FindObjectOfType<GlobalEvents>();
        if (globalEvents != null)
        {
            globalEvents.onSave -= OnSave;
           // globalEvents.onLoad -= OnLoad;
        }
    }

    public void OnSave(MySaveGame saveGame)
    {
        Debug.Log("EnemySystem OnSave: " + GetComponent<SaveGameIdSystem>().SaveGameId);
        var saveGameIdSystem = this.GetComponent<SaveGameIdSystem>();
        var abilitySystem = GetComponent<AbilitySystem>();
        var classSystem = GetComponent<ClassSystem>();
        var healthSystem = GetComponent<HealthSystem>();

        enemyRuntime = new EnemyRuntime(saveGameIdSystem.SaveGameId, abilitySystem.abilityRuntimes, classSystem.classRuntime);

        enemyRuntime.position = transform.position;
        enemyRuntime.rotation = transform.rotation;
        enemyRuntime.scale = transform.localScale;

        enemyRuntime.maxHealth = healthSystem.maxHealth;
        enemyRuntime.currentHealth = healthSystem.currentHealth;
        enemyRuntime.isAlive = healthSystem.IsAlive;

        saveGame.enemyRuntimes.Add(enemyRuntime);
    }

    public void OnLoad(MySaveGame saveGame)
    {
        Debug.Log("EnemySystem OnLoad");

        var saveGameIdSystem = this.GetComponent<SaveGameIdSystem>();
        var abilitySystem = GetComponent<AbilitySystem>();
        var classSystem = GetComponent<ClassSystem>();
        var healthSystem = GetComponent<HealthSystem>();

        var runtime = saveGame.enemyRuntimes.SingleOrDefault(x => x.saveGameId == saveGameIdSystem.SaveGameId);
        if (runtime == null)
        {
            throw new System.Exception(string.Format("No EnemyRuntime for SaveGameId {0}", saveGameIdSystem.SaveGameId));
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
}
