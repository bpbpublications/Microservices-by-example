using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitBreakerPattern
{
    public class HalfOpenState : CircuitBreakerState
    {
        public HalfOpenState(PaymentServiceCircuitBreaker circuitBreaker) : base(circuitBreaker) { }

        public override void ActionOnException(Exception e)
        {
            base.ActionOnException(e);
            circuitBreaker.MoveToOpenState();
        }

        public override void ProtectedActionCalled()
        {
            base.ProtectedActionCalled();
            circuitBreaker.MoveToClosedState();
        }
    }
}
