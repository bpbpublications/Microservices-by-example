using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitBreakerPattern
{
    public abstract class CircuitBreakerState
    {
        protected readonly PaymentServiceCircuitBreaker circuitBreaker;

        protected CircuitBreakerState(PaymentServiceCircuitBreaker circuitBreaker)
        {
            this.circuitBreaker = circuitBreaker;
        }

        public virtual PaymentServiceCircuitBreaker ProtectedActionToBeInvokedOnFailure()
        {
            return this.circuitBreaker;
        }
        public virtual void ProtectedActionCalled() { }
        public virtual void ActionOnException(Exception e) { circuitBreaker.IncreaseFailureCount(); }

        public virtual CircuitBreakerState Update()
        {
            return this;
        }
    }

    
}
