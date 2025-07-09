using System;
using System.Collections.Generic;
using Source.Scripts.Bots;
using Source.Scripts.Resources;
using Source.Scripts.Spawners;
using UnityEngine;

namespace Source.Scripts.Base
{
    [RequireComponent(typeof(BotsSpawner), typeof(ResourcesSearcher))]
    public class Base : MonoBehaviour
    {
        [SerializeField] private int _startBotsAmount;

        private List<BotCollector> _bots;
        private BotsSpawner _spawner;
        private ResourcesSearcher _searcher;
        private Dictionary<Type,int> _resources = new Dictionary<Type,int>();

        private void Awake()
        {
            _spawner = GetComponent<BotsSpawner>();
            _searcher = GetComponent<ResourcesSearcher>();
        }

        private void Start()
        {
            _bots = _spawner.SpawnBots(_startBotsAmount);
        }

        private void Update()
        {
            if (CheckForFreeBot(out BotCollector collector))
            {
                if (_searcher.TryGetResource(out Resource resource))
                {
                    resource.GetPreferToDeliver();

                    collector.GetTask(resource.transform.position);
                }
            }
        }

        public void GetResource(Resource resource)
        {
            ProcessResource(resource);
        }

        private bool CheckForFreeBot(out BotCollector botCollector)
        {
            botCollector = null;

            foreach (BotCollector bot in _bots)
            {
                if (bot.IsTaskRecieved == false)
                {
                    botCollector = bot;
                    return true;
                }
            }

            return false;
        }

        private void ProcessResource(Resource resource)
        {
            if (_resources.ContainsKey(resource.GetType()))
            {
                _resources[resource.GetType()]++;
            }
            else
            {
                _resources.Add(resource.GetType(), 1);
            }
        
            Destroy(resource.gameObject);

            foreach (var collectedResource in _resources) // Debug resources amount
            {
                Debug.Log(collectedResource.Key + " " + collectedResource.Value);
            }
        }
    }
}