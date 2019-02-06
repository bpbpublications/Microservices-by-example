using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitBreakerPattern
{
    public class OpenState : CircuitBreakerState
    {
        private readonly DateTime openDateTime;
        public OpenState(PaymentServiceCircuitBreaker circuitBreaker)
            : base(circuitBreaker)
        {
            openDateTime = DateTime.UtcNow;
        }

        public override PaymentServiceCircuitBreaker ProtectedActionToBeInvokedOnFailure()
        {
            base.ProtectedActionToBeInvokedOnFailure();
            this.Update();
            return base.circuitBreaker;
        }

        public override CircuitBreakerState Update()
        {
            base.Update();
            if (DateTime.UtcNow >= openDateTime + base.circuitBreaker.Timeout)
            {
                return circuitBreaker.MoveToHalfOpenState();
            }
            return this;
        }
    }
}
