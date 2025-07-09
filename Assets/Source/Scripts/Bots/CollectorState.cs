using UnityEngine;

namespace Source.Scripts.Bots
{
    public abstract class CollectorState : MonoBehaviour
    {
        protected BotCollector BotCollector;

        private void Awake()
        {
            BotCollector = GetComponent<BotCollector>();
        }
    }
}