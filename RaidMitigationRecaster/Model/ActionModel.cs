using Dalamud.Interface.Internal;
using Lumina.Excel.GeneratedSheets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidMitigationRecaster.Model {
    public class ActionModel {
        public class Action {
            public int ClassJobId;
            public int ActionId;
            public int StatusId;
            public bool IsBuff;
            public bool IsThrow;
            public float RecastTime;
            public IDalamudTextureWrap Image;
        }
    }
}
