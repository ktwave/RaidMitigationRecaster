using Dalamud.Configuration;
using Dalamud.Interface.GameFonts;
using Dalamud.Plugin;
using ImGuiNET;
using RaidMitigationRecaster.Enums;
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
        public bool IsEnabled { get; set; } = false;
        public bool IsLeftAligin { get; set; } = false;
        // public bool IsEnabledInCombat { get; set; } = false;
        public float FontScale { get; set; } = 100;
        public float FontOffsetX { get; set; } = 0;
        public float FontOffsetY { get; set; } = 0;
        public Dictionary<uint, bool> IsEnableAction { get; set; } = null;

        public void InitIsEnableAction() {
            Dictionary<uint, bool> IsEnableActions = new Dictionary<uint, bool>();
            foreach (Actions.TANK a in Enum.GetValues(typeof(Actions.TANK))) {
                IsEnableActions[(uint)a] = true;
            }
            foreach (Actions.HEALERandMAGICAL a in Enum.GetValues(typeof(Actions.HEALERandMAGICAL))) {
                IsEnableActions[(uint)a] = true;
            }
            foreach (Actions.MELEE a in Enum.GetValues(typeof(Actions.MELEE))) {
                IsEnableActions[(uint)a] = true;
            }
            foreach (Actions.MAGICAL a in Enum.GetValues(typeof(Actions.MAGICAL))) {
                IsEnableActions[(uint)a] = true;
            }
            foreach (Actions.PLD a in Enum.GetValues(typeof(Actions.PLD))) {
                IsEnableActions[(uint)a] = true;
            }
            foreach (Actions.WAR a in Enum.GetValues(typeof(Actions.WAR))) {
                IsEnableActions[(uint)a] = true;
            }
            foreach (Actions.DRK a in Enum.GetValues(typeof(Actions.DRK))) {
                IsEnableActions[(uint)a] = true;
            }
            foreach (Actions.GNB a in Enum.GetValues(typeof(Actions.GNB))) {
                IsEnableActions[(uint)a] = true;
            }
            foreach (Actions.WHM a in Enum.GetValues(typeof(Actions.WHM))) {
                IsEnableActions[(uint)a] = true;
            }
            foreach (Actions.SCH a in Enum.GetValues(typeof(Actions.SCH))) {
                IsEnableActions[(uint)a] = true;
            }
            foreach (Actions.AST a in Enum.GetValues(typeof(Actions.AST))) {
                IsEnableActions[(uint)a] = true;
            }
            foreach (Actions.SGE a in Enum.GetValues(typeof(Actions.SGE))) {
                IsEnableActions[(uint)a] = true;
            }
            foreach (Actions.MNK a in Enum.GetValues(typeof(Actions.MNK))) {
                IsEnableActions[(uint)a] = true;
            }
            foreach (Actions.RPR a in Enum.GetValues(typeof(Actions.RPR))) {
                IsEnableActions[(uint)a] = true;
            }
            foreach (Actions.BRD a in Enum.GetValues(typeof(Actions.BRD))) {
                IsEnableActions[(uint)a] = true;
            }
            foreach (Actions.MCH a in Enum.GetValues(typeof(Actions.MCH))) {
                IsEnableActions[(uint)a] = true;
            }
            foreach (Actions.DNC a in Enum.GetValues(typeof(Actions.DNC))) {
                IsEnableActions[(uint)a] = true;
            }
            foreach (Actions.RDM a in Enum.GetValues(typeof(Actions.RDM))) {
                IsEnableActions[(uint)a] = true;
            }
            this.IsEnableAction = IsEnableActions;
        }

        public GameFontFamilyAndSize? Font = null;

        public void Save() {
            this.PluginInterface!.SavePluginConfig(this);
        }
    }
}
