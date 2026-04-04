namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI
{
    public class StateMachineBrain : IBrain
    {
        private AIStateMachine _stateMachine;
        private bool _isEndabled;

        public StateMachineBrain(AIStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Disable()
        {
            _stateMachine.Exit();
            _isEndabled = false;
        }

        public void Dispose()
        {
            _stateMachine.Dispose();
            _isEndabled = false;
        }

        public void Enable()
        {
            _stateMachine.Enter();
            _isEndabled = true;
        }

        public void Update(float deltaTime)
        {
            if (_isEndabled)
                return;

            _stateMachine.Update(deltaTime);
        }
    }
}
