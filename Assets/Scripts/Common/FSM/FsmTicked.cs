using System;

namespace Common.FSM
{
    public abstract class FsmStateTicked<TStateEntity>
    {
        protected readonly TStateEntity entity;

        public FsmStateTicked(TStateEntity entity)
        {
            this.entity = entity;
        }

        public virtual void OnEnter() { }

        public virtual FsmStateTicked<TStateEntity> Update(float dt)
        {
            return this;
        }

        public virtual void OnLeave() { }
    }
    
    public class FsmTicked<TEntityState> : IDisposable
    {
        private FsmStateTicked<TEntityState> state;

        public FsmStateTicked<TEntityState> State
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

        public FsmTicked(FsmStateTicked<TEntityState> state)
        {
            this.state = state;
            state?.OnEnter();
        }

        public void Update(float dt)
        {
            var currentState = State;
            if (currentState != null)
            {
                State = currentState.Update(dt);
            }
        }

        public void Dispose()
        {
            State?.OnLeave();
            State = null;
        }
    }
}