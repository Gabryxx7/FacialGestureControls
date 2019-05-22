using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAffectiva
{
    class AffectivaAction{
        protected String name { get; set; }
        protected double thresholdValue;
        protected Func<double, bool> callback;
        public AffectivaAction(double threshold, Func<double, bool> action)
        {
            this.callback = action;
            this.thresholdValue = threshold;
        }
        public void setAction(Func<double, bool> action)
        {
            this.callback = action;
        }
        public bool executeAction(double val)
        {
            return callback.Invoke(val);
        }
    }
}
