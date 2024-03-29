﻿using Dalamud.Interface.GameFonts;
using Dalamud.IoC;
using Dalamud.Logging;
using Dalamud.Plugin;
using RaidMitigationRecaster.Enums;
using Newtonsoft.Json.Linq;
using RaidMitigationRecaster.Service;
using RaidMitigationRecaster.Service;
using System.Numerics;
using RaidMitigationRecaster.Model;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Plugin.Services;
using Dalamud.Interface.Internal;
using Lumina.Excel.GeneratedSheets;
using Dalamud.Utility.Signatures;
using System.Reflection.Emit;

namespace RaidMitigationRecaster {
    public unsafe class RaidMitigationRecaster : IDalamudPlugin {
        private DalamudPluginInterface PluginInterface { get; init; }

        public static string Name => "Raid Mitigation Recaster";
        bool isConfigOpen = false;
        internal Config config;
        internal static RaidMitigationRecaster R;

        // user var
        public List<ActionModel.Action> actions;
        public List<TimerModel.Timer> Timers;
        public bool isPartyListChanged = false;
        public IDalamudTextureWrap imageBlackOut;

        // bool isDebug = true;
        bool isDebug = false;

        // user constants
        public static float ImageSize => 76f;
        public static int MaxCol => 5;
        public static int MaxRow => 8;
        public static Vector4 White => new Vector4(1f, 1f, 1f, 1f);
        public static Vector4 Red => new Vector4(1f, 0f, 0f, 1f);
        public static Vector4 Black => new Vector4(0f, 0f, 0f, 1f);
        public static int offset = 0x4A8;
        public void Dispose() {
            DalamudService.PluginInterface.UiBuilder.Draw -= Draw;
            R = null;
        }

        public RaidMitigationRecaster([RequiredVersion("1.0")] DalamudPluginInterface pluginInterface) {
            try {
                PluginInterface = pluginInterface;

                R = this;
                PluginInterface.Create<DalamudService>();
                DalamudService.PluginInterface.UiBuilder.Draw += Draw;
                config = DalamudService.PluginInterface.GetPluginConfig() as Config ?? new Config();
                if (config.IsEnableAction == null) config.InitIsEnableAction();

                actions = ActionService.SetActions(config, pluginInterface);
                var ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\blackout.png");
                imageBlackOut = pluginInterface.UiBuilder.LoadImage(ImagePath);

                PluginLog.Information("[" + Name + "] Initialize!!!");

                DalamudService.PluginInterface.UiBuilder.OpenConfigUi += delegate { isConfigOpen = true; };
                DalamudService.Framework.RunOnFrameworkThread(() => {
                    if (config.Font != null) {
                        _ = DalamudService.PluginInterface.UiBuilder.GetGameFontHandle(new GameFontStyle(config.Font.Value));
                    }
                });
            } catch (Exception e) {
                PluginLog.Error(e.Message + "\n" + e.StackTrace);
            }
        }

        private void Draw() {
            try {
                if (isConfigOpen) MainService.DrawConfigWindow(ref config, ref isConfigOpen, actions);

                if (DalamudService.ClientState.IsPvP) return;

                if (DalamudService.ClientState.LocalPlayer == null) return;

                if (!config.IsEnabled) return;

                if (isDebug) MainService.DrawDebugWindow(ref config);

                if (DalamudService.Condition[ConditionFlag.InCombat] && !DalamudService.ClientState.IsPvP) {
                    // in combat
                    MainService.DrawMainWindow(ref Timers, config, imageBlackOut);
                } else {
                    // not in combat
                    MainService.UpdateTimers(actions, ref Timers, config);
                    MainService.DrawMainWindow(ref Timers, config, imageBlackOut);
                }
            } catch (Exception e) {
                PluginLog.Error(e.Message + "\n" + e.StackTrace);
            } finally {

            }
        }
    }
}