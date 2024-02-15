using Dalamud.Interface.GameFonts;
using Dalamud.IoC;
using Dalamud.Logging;
using Dalamud.Plugin;
using RaidMitigationRecaster.Enums;
using Newtonsoft.Json.Linq;
using RaidBuffRecaster.Service;
using RaidMitigationRecaster.Service;
using System.Numerics;
using RaidMitigationRecaster.Model;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Plugin.Services;

namespace RaidMitigationRecaster {
    public unsafe class RaidMitigationRecaster : IDalamudPlugin {
        bool isDebug = true;
        // bool isDebug = false;

        public static string Name => "Raid Mitigation Recaster";
        bool isConfigOpen = false;
        internal Config config;
        internal static RaidMitigationRecaster R;

        private DalamudPluginInterface PluginInterface { get; init; }

        // user var
        public List<ActionModel.Action> Actions;
        public List<TimerModel.Timer> Timers;
        public List<uint> localPartyList;
        public bool IsPartyListChanged = false;

        // user constants
        public static float ImageSize => 76f;
        public static int MaxCol => 4;
        public static int MaxRow => 8;
        public static Vector4 White => new Vector4(1f,1f,1f,1f);
        public static Vector4 Red => new Vector4(1f, 0f, 0f, 0f);
        public static Vector4 Black => new Vector4(0f,0f,0f,1f);

        public void Dispose() {
            DalamudService.PluginInterface.UiBuilder.Draw -= Draw;
            R = null;
        }

        public RaidMitigationRecaster([RequiredVersion("1.0")] DalamudPluginInterface pluginInterface) {
            PluginInterface = pluginInterface;

            R = this;
            PluginInterface.Create<DalamudService>();
            DalamudService.PluginInterface.UiBuilder.Draw += Draw;
            config = DalamudService.PluginInterface.GetPluginConfig() as Config ?? new Config();

            Actions = ActionService.SetActions(config, pluginInterface);
            localPartyList = DalamudService.PartyList.Select(p => p.ClassJob.Id).Order().ToList(); ;

            PluginLog.Information("["+Name+"] Initialize!!!");

            DalamudService.PluginInterface.UiBuilder.OpenConfigUi += delegate { isConfigOpen = true; };
            DalamudService.Framework.RunOnFrameworkThread(() => {
                if (config.Font != null) {
                    _ = DalamudService.PluginInterface.UiBuilder.GetGameFontHandle(new GameFontStyle(config.Font.Value));
                }
            });
        }

        private void Draw() {
            try {
                if (isConfigOpen) MainService.DrawConfigWindow(ref config, ref isConfigOpen);

                if (DalamudService.ClientState.IsPvP) return;

                if (DalamudService.ClientState.LocalPlayer == null) return;

                if (!config.IsEnabled) return;

                if (isDebug) {
                    MainService.DrawDebugWindow(ref config);
                    MainService.DrawDebugHotbarInfo();
                }
                /*
                IsPartyListChanged = MainService.IsChangedPartyList(ref localPartyList);
                if (IsPartyListChanged) MainService.UpdateTimers();

                MainService.DrawMainWindow(, config);
                */
            } catch (Exception e) {
                PluginLog.Error(e.Message + "\n" + e.StackTrace);
            } finally {

            }
        }
    }
}