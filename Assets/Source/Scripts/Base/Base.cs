using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BotsSpawner), typeof(ResourcesSearcher))]
public class Base : MonoBehaviour
{
    [SerializeField] private int _startBotsAmount;

    private List<BotCollector> _bots;
    private BotsSpawner _spawner;
    private ResourcesSearcher _searcher;
    private float _gearsAmount;
    private float _barrelsAmount;

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
                Debug.Log(collector + " " + resource + " " + resource.transform + "- get task");
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
        if (resource is Gear)
        {
            _gearsAmount++;
            resource.gameObject.SetActive(false);
            Debug.Log(_gearsAmount + " gears collected");
        }
        else if (resource is Barrel)
        {
            _barrelsAmount++;
            resource.gameObject.SetActive(false);
            Debug.Log(_barrelsAmount + " barrels collected");
        }
    }
}