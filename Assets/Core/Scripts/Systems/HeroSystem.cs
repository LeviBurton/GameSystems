using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SaveGameIdSystem))]
[RequireComponent(typeof(AbilitySystem))]
[RequireComponent(typeof(ClassSystem))]
[RequireComponent(typeof(HealthSystem))]
[RequireComponent(typeof(CharacterSystem))]
public class HeroSystem : MonoBehaviour
{
    public HeroRuntime heroRuntime;

    void Start()
    {
        heroRuntime = new HeroRuntime(GetComponent<SaveGameIdSystem>().SaveGameId, null, null);
    }

    public void OnSave(MySaveGame saveGame)
    {
        Debug.Log("HeroSystem OnSave: " + GetComponent<SaveGameIdSystem>().SaveGameId);

        var saveGameIdSystem = this.GetComponent<SaveGameIdSystem>();
        var abilitySystem = GetComponent<AbilitySystem>();
        var classSystem = GetComponent<ClassSystem>();
        var healthSystem = GetComponent<HealthSystem>();

        heroRuntime = new HeroRuntime(saveGameIdSystem.SaveGameId, abilitySystem.abilityRuntimes, classSystem.classRuntime);

        heroRuntime.position = transform.position;
        heroRuntime.rotation = transform.rotation;
        heroRuntime.scale = transform.localScale;

        heroRuntime.maxHealth = healthSystem.maxHealth;
        heroRuntime.currentHealth = healthSystem.currentHealth;
        heroRuntime.isAlive = healthSystem.IsAlive;

        saveGame.heroRuntimes.Add(heroRuntime);
    }

    public void OnLoad(MySaveGame saveGame)
    {
        Debug.Log("HeroSystem OnLoad");

        var saveGameIdSystem = this.GetComponent<SaveGameIdSystem>();
        var abilitySystem = GetComponent<AbilitySystem>();
        var classSystem = GetComponent<ClassSystem>();
        var healthSystem = GetComponent<HealthSystem>();

        var runtime = saveGame.heroRuntimes.SingleOrDefault(x => x.saveGameId == saveGameIdSystem.SaveGameId);
        if (runtime == null)
        {
            throw new System.Exception(string.Format("No HeroRuntime for SaveGameId {0}", saveGameIdSystem.SaveGameId));
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
          //  globalEvents.onLoad += OnLoad;
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

}