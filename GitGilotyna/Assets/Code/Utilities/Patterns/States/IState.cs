namespace Code.Utilities
{
    public interface IState<Controller>
    {
        public void Handle(Controller controller);
    }
}