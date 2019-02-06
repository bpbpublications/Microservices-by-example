using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitBreakerPattern
{
    public class PaymentServiceCircuitBreaker
    {
        private readonly object monitor = new object();
        private CircuitBreakerState state;

        public PaymentServiceCircuitBreaker(int threshold, TimeSpan timeout)
        {
            if (threshold < 1)
            {
                throw new ArgumentOutOfRangeException("threshold", "Threshold should be greater than 0");
            }

            if (timeout.TotalMilliseconds < 1)
            {
                throw new ArgumentOutOfRangeException("timeout", "Timeout should be greater than 0");
            }

            Threshold = threshold;
            Timeout = timeout;
            MoveToClosedState();
        }

        public int Failures { get; private set; }
        public int Threshold { get; private set; }
        public TimeSpan Timeout { get; private set; }
        public bool IsClosed
        {
            get { return state.Update() is ClosedState; }
        }

        public bool IsOpen
        {
            get { return state.Update() is OpenState; }
        }

        public bool IsHalfOpen
        {
            get { return state.Update() is HalfOpenState; }
        }

        internal CircuitBreakerState MoveToClosedState()
        {
            state = new ClosedState(this);
            return state;
        }

        internal CircuitBreakerState MoveToOpenState()
        {
            state = new OpenState(this);
            return state;
        }

        internal CircuitBreakerState MoveToHalfOpenState()
        {
            state = new HalfOpenState(this);
            return state;
        }

        internal void IncreaseFailureCount()
        {
            Failures++;
        }

        internal void ResetFailureCount()
        {
            Failures = 0;
        }

        public bool IsThresholdReached()
        {
            return Failures >= Threshold;
        }

        private Exception exceptionFromLastAttemptCall = null;

        public Exception GetExceptionFromLastAttemptCall()
        {
            return exceptionFromLastAttemptCall;
        }

        public PaymentServiceCircuitBreaker AttemptCall(Action protectedCode)
        {
            this.exceptionFromLastAttemptCall = null;
            lock (monitor)
            {
                state.ProtectedActionToBeInvokedOnFailure();
                if (state is OpenState)
                {
                    return this; // Stop execution of this method
                }
            }

            try
            {
                protectedCode();
            }
            catch (Exception e)
            {
                this.exceptionFromLastAttemptCall = e;
                lock (monitor)
                {
                    state.ActionOnException(e);
                }
                return this; // Stop execution of this method
            }

            lock (monitor)
            {
                state.ProtectedActionCalled();
            }
            return this;
        }

        public void Close()
        {
            lock (monitor)
            {
                MoveToClosedState();
            }
        }

        public void Open()
        {
            lock (monitor)
            {
                MoveToOpenState();
            }
        }
    }
}
