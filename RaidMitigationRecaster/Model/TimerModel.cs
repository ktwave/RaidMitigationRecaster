using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidMitigationRecaster.Model {
    public class TimerModel {
        public class Timer : ActionModel.Action {
            public uint ObjectId;
            public float RemainingTime;
            public Stopwatch StopWatch;
        }
    }
}
