namespace Code.Utilities
{
    public class StateContext<TState, TController> where TState: IState<TController>
    {
        public TState CurrentState { get; set; }

        private readonly TController _stateController;

        public StateContext(TController stateController)
        {
            _stateController = stateController;
        }

        public void Transition()
        {
            CurrentState.Handle(_stateController);
        }

        public void Transition(TState state)
        {
            CurrentState = state;
            Transition();
        }
    }
}