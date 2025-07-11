using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Bots
{
    [RequireComponent(typeof(BotCollector))]
    public abstract class CollectorState : MonoBehaviour, IExitable, IEnterable
    {
        protected BotCollector BotCollector;

        private void Awake()
        {
            BotCollector = GetComponent<BotCollector>();
            enabled = false;
        }

        public virtual void Enter()
        {
            enabled = true;
        }
        
        public virtual void Exit()
        {
            enabled = false;
        }
    }
}