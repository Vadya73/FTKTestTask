namespace CodeBase.StateMachine
{
    public interface IState : IExitableState
    {
        void Enter();
        void Update();
    }

    public interface IExitableState
    {
        void Exit();
    }
    
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}