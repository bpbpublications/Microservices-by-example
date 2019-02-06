using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitBreakerPattern
{
    public class ClosedState : CircuitBreakerState
    {
        public ClosedState(PaymentServiceCircuitBreaker circuitBreaker)
            : base(circuitBreaker)
        {
            circuitBreaker.ResetFailureCount();
        }

        public override void ActionOnException(Exception e)
        {
            base.ActionOnException(e);
            if (circuitBreaker.IsThresholdReached())
            {
                circuitBreaker.MoveToOpenState();
            }
        }
    }
}
