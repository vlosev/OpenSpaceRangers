using System;

namespace Common.FSM
{
    public abstract class FsmState<TStateEntity>
    {
        protected readonly TStateEntity entity;

        public FsmState(TStateEntity entity)
        {
            this.entity = entity;
        }

        public virtual void OnEnter() { }

        public virtual FsmState<TStateEntity> Update()
        {
            return this;
        }

        public virtual void OnLeave() { }
    }
    
    public class Fsm<TEntityState> : IDisposable
    {
        private FsmState<TEntityState> state;

        public FsmState<TEntityState> State
        {
            get => state;
            set
            {
                if (ReferenceEquals(state, value) != true)
                {
                    state?.OnLeave();
                    state = value;
                    state?.OnEnter();
                }
            }
        }

        public Fsm(FsmState<TEntityState> state)
        {
            this.state = state;
            state?.OnEnter();
        }

        public void Update()
        {
            var currentState = State;
            if (currentState != null)
            {
                State = currentState.Update();
            }
        }

        public void Dispose()
        {
            State?.OnLeave();
            State = null;
        }
    }
}
