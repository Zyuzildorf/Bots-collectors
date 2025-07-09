using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Bots
{
    public class IdleState : CollectorState, IUpdatable
    {
        [SerializeField] private SearchingResourceState _searchingResourceState;

        public void UpdateState()
        {
            if (BotCollector.IsTaskRecieved)
            {
                BotCollector.SetState(_searchingResourceState);
            }
        }
    }
}