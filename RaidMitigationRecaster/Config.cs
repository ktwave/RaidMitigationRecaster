using Dalamud.Configuration;
using Dalamud.Interface.GameFonts;
using Dalamud.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidMitigationRecaster {
    [Serializable]
    class Config : IPluginConfiguration {
        [NonSerialized]
        private DalamudPluginInterface? PluginInterface;

        public int Version { get; set; } = 0;
        public float OffsetX { get; set; } = 0;
        public float OffsetY { get; set; } = 0;
        public float Size { get; set; } = 100;
        public float PaddingX { get; set; } = 0;
        public float PaddingY { get; set; } = 0;
        public bool IsPreview { get; set; } = false;
        public bool IsEnabled { get; set; } = false;
        public bool IsLeftAligin { get; set; } = false;

        public GameFontFamilyAndSize? Font = null;

        public void Save() {
            this.PluginInterface!.SavePluginConfig(this);
        }
    }
}
