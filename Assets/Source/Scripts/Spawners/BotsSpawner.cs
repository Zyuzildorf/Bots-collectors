using System.Collections.Generic;
using Source.Scripts.Bots;
using UnityEngine;

namespace Source.Scripts.Spawners
{
    public class BotsSpawner : Spawner
    {
        public List<BotCollector> SpawnBots(int amount)
        {
            List<BotCollector> bots = new List<BotCollector>();
        
            for (int i = 0; i < amount; i++)
            {
                SpawnObject(out GameObject obj);
                obj.TryGetComponent(out BotCollector bot);
            
                bots.Add(bot);
            }
        
            return bots;
        }
    }
}