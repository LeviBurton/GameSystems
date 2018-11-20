using System;
using System.Collections.Generic;

public static class SaveGameDefaults
{
    public static int defaultSaveGameVersion = 1;
    public static string defaultSlotName = "Default Slot";
}


[Serializable]
public abstract class SaveGame
{
    public int version;
    public DateTime dateCreated;
    public int saveNumber;

    public string sceneName;
    public int sceneBuildIndex;

    [NonSerialized] public string slotName;
}

// TODO: split the save game out into a header type class (save name, save date, save screen shot, etc.) and the main save data.
// this will let us load just the header in our load/save game lists.
[Serializable]
public class MySaveGame : SaveGame
{
    public List<HeroRuntime> heroRuntimes = new List<HeroRuntime>();
    public List<EnemyRuntime> enemyRuntimes = new List<EnemyRuntime>();
    public List<NpcRuntime> npcRuntimes = new List<NpcRuntime>();

    public MySaveGame(int? version, string saveSlotName = "Default")
    {
        this.slotName = saveSlotName;

        if (version.HasValue)
        {
            this.version = version.Value;
        }
        else
        {
            this.version = SaveGameDefaults.defaultSaveGameVersion;
        }
      
        this.dateCreated = DateTime.Now;
    }
}