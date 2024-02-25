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
            public uint ClassJobId;
            public string ActionName;
            public uint ActionId;
            public uint StatusId;
            public bool IsBuff;
            public bool IsThrow;
            public float RecastTime;
            public IDalamudTextureWrap Image;
        }
    }
}
