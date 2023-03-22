using Code.General;
using Code.Player.States.StateFactory;
using Code.Utilities;
using UnityEngine;

namespace Code.Player.States
{
    public class DeadState: PlayerState
    {
        private Timer DeathTimer;

        public DeadState()
        {
            DeathTimer = new Timer(5f, () =>
            {
                GameManager.instance.GoToMainMenu();
            });
        }
        public override void Handle(Player controller)
        {
            DeathTimer.Update(Time.deltaTime);
            controller.GetRigidbody.isKinematic = true;
        }
    }
}