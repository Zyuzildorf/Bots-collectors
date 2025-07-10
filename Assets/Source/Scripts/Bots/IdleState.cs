using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Bots
{
    public class IdleState : CollectorState, IUpdatable
    {
        [SerializeField] private SearchingResourceState _searchingResourceState;

        public void UpdateState()
        {
            if (BotCollector.IsTaskReceived)
            {
                BotCollector.SetState(_searchingResourceState);
            }
        }
    }
}