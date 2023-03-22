using Code.Utilities;

namespace Code.General.States.StateFactory
{
    public abstract class GameState : IState<GameManager>, IFactoryData
    {
        public abstract void Handle(GameManager controller);

        public string Name => this.GetType().Name;
    }
}