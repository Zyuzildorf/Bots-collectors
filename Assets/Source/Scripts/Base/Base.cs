using System.Collections;
using System.Collections.Generic;
using Source.Scripts.Bots;
using Source.Scripts.Other;
using Source.Scripts.Spawners;
using UnityEngine;

namespace Source.Scripts.Base
{
    [RequireComponent(typeof(BotsSpawner), typeof(ResourcesSearcher))]
    public class Base : MonoBehaviour
    {
        [SerializeField] private int _startBotsAmount;
        [SerializeField] private float _searchingBotsDelay;
        [SerializeField] private float _searchingResourcesDelay;
        [SerializeField] private ResourcesVault _vault;

        private List<BotCollector> _bots;
        private BotsSpawner _spawner;
        private ResourcesSearcher _searcher;
        private List<Resource> _resources;
        private WaitForSeconds _waitForBotsSearch;
        private WaitForSeconds _waitForResourcesSearch;

        private void Awake()
        {
            _spawner = GetComponent<BotsSpawner>();
            _searcher = GetComponent<ResourcesSearcher>();
            _waitForBotsSearch = new WaitForSeconds(_searchingBotsDelay);
            _waitForResourcesSearch = new WaitForSeconds(_searchingResourcesDelay);
            _resources = new List<Resource>();
        }

        private void Start()
        {
            _bots = _spawner.SpawnBots(_startBotsAmount);

            StartCoroutine(CheckForFreeBots());
            StartCoroutine(CheckForFreeResourcesAvailable());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public void SetResource(Resource resource)
        {
            _vault.RemoveBusyResource(resource);
            _resources.Add(resource);
        }

        private IEnumerator CheckForFreeBots()
        {
            while (enabled)
            {
                foreach (BotCollector bot in _bots)
                {
                    if (bot.IsBotFree)
                    {
                        TryGiveTask(bot);
                    }
                }

                yield return _waitForBotsSearch;
            }
        }

        private IEnumerator CheckForFreeResourcesAvailable()
        {
            while (enabled)
            {
                if (_vault.FreeResourcesCount <= 0)
                {
                    if (_searcher.TryFindResources(out List<Resource> resources))
                    {
                        _vault.SetResources(resources);
                    }
                }

                yield return _waitForResourcesSearch;
            }
        }

        private void TryGiveTask(BotCollector bot)
        {
            if (_vault.TryGetFreeResource(out Resource resource))
            {
                bot.GetTask(resource.transform.position);
            }
        }
    }
}