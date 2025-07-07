using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(BotsSpawner), typeof(ResourcesSearcher))]
public class Base : MonoBehaviour
{
    [SerializeField] private Vector3 _colliderSize;
    [SerializeField] private int _startBotsAmount;

    private List<BotCollector> _bots;
    private BoxCollider _collider;
    private BotsSpawner _spawner;
    private ResourcesSearcher _searcher;
    private float _gearsAmount;
    private float _barrelsAmount;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _spawner = GetComponent<BotsSpawner>();
        _searcher = GetComponent<ResourcesSearcher>();
    }

    private void Start()
    {
        _collider.size = _colliderSize;
        
        _bots = _spawner.SpawnBots(_startBotsAmount);
    }

    private void Update()
    {
        // if (CheckForFreeBot(out BotCollector bot))
        // {
        //     Transform foundResource = _searcher.SearchResources();
        //
        //     if (foundResource != null)
        //     {
        //         bot.GetTask(foundResource);
        //     }
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            ProcessResource(resource);
        }
    }

    private bool CheckForFreeBot(out BotCollector collector)
    {
        foreach (BotCollector bot in _bots)
        {
            if (bot.IsTaskRecieved == false)
            {
                collector = bot;
                return true;
            }
        }
        
        collector = null;
        return false;
    }
    
    private void ProcessResource(Resource resource)
    {
        if (resource is Gear)
        {
            _gearsAmount++;
            Debug.Log(_gearsAmount + " gears");
        }
        else if (resource is Barrel)
        {
            _barrelsAmount++;
            Debug.Log(_barrelsAmount + " barrels");
        }
    }
}