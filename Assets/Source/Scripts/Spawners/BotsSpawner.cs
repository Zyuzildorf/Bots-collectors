using System.Collections.Generic;
using UnityEngine;

public class BotsSpawner : Spawner
{
    private List<BotCollector> _bots = new List<BotCollector>();
    
    public List<BotCollector> SpawnBots(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnObject(out GameObject obj);
            
            _bots.Add(obj.GetComponent<BotCollector>());
        }
        
        return _bots;
    }
}