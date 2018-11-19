using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroGroupSystem : MonoBehaviour
{
    public int groupId;

    public List<HeroSystem> GetGroupMembers()
    {
        var heros = FindObjectsOfType<HeroSystem>();
        List<HeroSystem> heroesInGroup = new List<HeroSystem>();

        foreach (var hero in heros)
        {
            var group = hero.GetComponent<HeroGroupSystem>();
            if (group != null)
            {
                if (group.groupId == groupId)
                    heroesInGroup.Add(hero);
            }
        }

        return heroesInGroup;
    }
}
