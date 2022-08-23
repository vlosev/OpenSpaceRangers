using System;

namespace Common.FSM
{
    public abstract class FsmState<TStateEntity, TArgs>
    {
        protected readonly TStateEntity entity;

        public FsmState(TStateEntity entity)
        {
            this.entity = entity;
        }

        public virtual void OnEnter() { }

        public virtual FsmState<TStateEntity, TArgs> Update(TArgs args)
        {
            return this;
        }

        public virtual void OnLeave() { }
    }
    
    public class Fsm<TEntityState, TArgs> : IDisposable
    {
        private FsmState<TEntityState, TArgs> state;

        public FsmState<TEntityState, TArgs> State
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

        public Fsm(FsmState<TEntityState, TArgs> state)
        {
            this.state = state;
            state?.OnEnter();
        }

        public void Update(TArgs args)
        {
            var currentState = State;
            if (currentState != null)
            {
                State = currentState.Update(args);
            }
        }

        public void Dispose()
        {
            State?.OnLeave();
            State = null;
        }
    }
}
