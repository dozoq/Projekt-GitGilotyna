using Code.General.States.StateFactory;
using UnityEngine;

namespace Code.General.States
{
    public class MenuState: GameState
    {
        public override void Handle(GameManager controller)
        {
            
        }

        private void RemoveUnusedComponents(GameManager controller)
        {
            DayCycle dayCycle;
            if (controller.TryGetComponent<DayCycle>(out dayCycle)) Object.Destroy(dayCycle);

        }
    }
}