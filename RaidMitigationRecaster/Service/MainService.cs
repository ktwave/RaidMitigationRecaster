using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Interface.GameFonts;
using RaidMitigationRecaster.Enums;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;
using ImGuiNET;
using Microsoft.VisualBasic;
using RaidMitigationRecaster.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalamud.Interface.Utility;
using RaidMitigationRecaster.Model;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Plugin.Services;
using System.Diagnostics;
using System.Collections;
using Dalamud.Game.ClientState.Statuses;
using Dalamud.Logging;
using Lumina.Excel.GeneratedSheets2;
using Dalamud.Interface.Internal;

namespace RaidMitigationRecaster.Service {
    internal class MainService {
        internal static void DrawConfigWindow(ref Config config, ref bool isConfigOpen) {
            if (ImGui.Begin(RaidMitigationRecaster.Name + " Config", ref isConfigOpen, ImGuiWindowFlags.AlwaysAutoResize)) {
                // ImGui.SetWindowSize(new Vector2(350, 500));

                var isEnabled = config.IsEnabled;
                if (ImGui.Checkbox("プラグインを有効にする(Enable Plugin)", ref isEnabled)) {
                    config.IsEnabled = isEnabled;
                }

                ImGui.Spacing();

                var isPreview = config.IsPreview;
                if (ImGui.Checkbox("プレビュー(Preview)", ref isPreview)) {
                    config.IsPreview = isPreview;
                }
                ImGui.Spacing();

                var isEnabledInCombat = config.IsEnabledInCombat;
                if (ImGui.Checkbox("戦闘時のみ有効(Enable Only In Combat)", ref isEnabledInCombat)) {
                    config.IsEnabledInCombat = isEnabledInCombat;
                }
                ImGui.Spacing();
                ImGui.Separator();
                ImGui.Spacing();

                ImGui.Text("X座標のオフセット(X Offset)");
                var offsetX = config.OffsetX;
                ImGui.SetNextItemWidth(200f);
                if (ImGui.DragFloat(" ", ref offsetX, 0.1f)) {
                    config.OffsetX = offsetX;
                }
                ImGui.Spacing();

                ImGui.Text("Y座標のオフセット(Y Offset)");
                var offsetY = config.OffsetY;
                ImGui.SetNextItemWidth(200f);
                if (ImGui.DragFloat("  ", ref offsetY, 0.1f)) {
                    config.OffsetY = offsetY;
                }
                ImGui.Spacing();

                ImGui.Text("アイコンの拡大率(Icon Scale)");
                var size = config.Size;
                ImGui.SetNextItemWidth(200f);
                if (ImGui.DragFloat("   ", ref size, 0.5f, 1, 300)) {
                    config.Size = size;
                }
                ImGui.Spacing();

                ImGui.Text("アイコンの横間隔(Icon Padding X)");
                var paddingX = config.PaddingX;
                ImGui.SetNextItemWidth(200f);
                if (ImGui.DragFloat("     ", ref paddingX, 0.1f, -100, 100)) {
                    config.PaddingX = paddingX;
                }
                ImGui.Spacing();

                ImGui.Text("アイコンの縦間隔(Icon Padding Y)");
                var paddingY = config.PaddingY;
                ImGui.SetNextItemWidth(200f);
                if (ImGui.DragFloat("       ", ref paddingY, 0.1f, -100, 100)) {
                    config.PaddingY = paddingY;
                }
                ImGui.Spacing();

                ImGui.Text("フォントの拡大率(Font Scale)");
                var fontScale = config.FontScale;
                ImGui.SetNextItemWidth(200f);
                if (ImGui.DragFloat("        ", ref fontScale, 0.5f, 1, 300)) {
                    config.FontScale = fontScale;
                }
                ImGui.Spacing();

                ImGui.Text("フォント X座標のオフセット(Font X Offset)");
                var fontOffsetX = config.FontOffsetX;
                ImGui.SetNextItemWidth(200f);
                if (ImGui.DragFloat("         ", ref fontOffsetX, 0.1f)) {
                    config.FontOffsetX = fontOffsetX;
                }
                ImGui.Spacing();

                ImGui.Text("フォント Y座標のオフセット(Font Y Offset)");
                var fontOffsetY = config.FontOffsetY;
                ImGui.SetNextItemWidth(200f);
                if (ImGui.DragFloat("           ", ref fontOffsetY, 0.1f)) {
                    config.FontOffsetY = fontOffsetY;
                }
                ImGui.Spacing();

                var isLeftAligin = config.IsLeftAligin;
                if (ImGui.Checkbox("アイコンを左揃えにする(Icons Left Aligin)", ref isLeftAligin)) {
                    config.IsLeftAligin = isLeftAligin;
                }
                ImGui.Spacing();

                ImGui.Separator();
                ImGui.Spacing();

                config.Font = GameFontFamilyAndSize.Axis36;

                ImGui.SetCursorPosX(180f);
                if (ImGui.Button("閉じる(Close)")) {
                    isConfigOpen = false;
                    DalamudService.PluginInterface.SavePluginConfig(config);
                }
                ImGui.End();
            }

            if (!isConfigOpen) {
                DalamudService.PluginInterface.SavePluginConfig(config);
            }
        }

        internal static void DrawPleaviewWindow(List<ActionModel.Action> actions, Config config) {

        }

        internal static void DrawMainWindow(
            ref List<TimerModel.Timer> ListTimer,
            Config config,
            IDalamudTextureWrap imageBlackOut) {

            var isBegin = false;

            try {
                var ptListPos = GetPtlistPosition();
                if (ptListPos == null) return;
                var x = ((Vector2)ptListPos).X;
                var y = ((Vector2)ptListPos).Y;

                ImGuiHelpers.ForceNextWindowMainViewport();
                ImGuiHelpers.SetNextWindowPosRelativeMainViewport(new Vector2(x + config.OffsetX, y + config.OffsetY));

                ImGui.PushFont(DalamudService.PluginInterface.UiBuilder.GetGameFontHandle(new GameFontStyle(config.Font.Value)).ImFont);

                if (ImGui.Begin("MainWindow", ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoBackground)) {
                    isBegin = true;
                    var index = 0;
                    var imageSize = (RaidMitigationRecaster.ImageSize * config.Size / 100);
                    ImGui.SetWindowFontScale(config.FontScale / 100);

                    if (config.IsLeftAligin) {
                        for (var row = 0; row < RaidMitigationRecaster.MaxRow; row++) {
                            if (index == ListTimer.Count) break;
                            foreach (var col in Enumerable.Range(0, RaidMitigationRecaster.MaxCol)) {
                                if (index == ListTimer.Count) break;
                                var timer = ListTimer[index];
                                if (timer.IsBuff) {
                                    if (DalamudService.PartyList.Count == 0) {
                                        // solo
                                        var statuses = DalamudService.ClientState.LocalPlayer.StatusList;
                                        var status = statuses == null ? null : statuses.Where(s => s.StatusId == timer.StatusId).FirstOrDefault();
                                        DrawMainWindow(ref timer, config, imageSize, col, row, status, imageBlackOut);
                                    } else {
                                        // party
                                        var statuses = DalamudService.PartyList.Where(p => p.ObjectId == timer.ObjectId).FirstOrDefault().Statuses;
                                        var status = statuses == null ? null : statuses.Where(s => s.StatusId == timer.StatusId).FirstOrDefault();
                                        DrawMainWindow(ref timer, config, imageSize, col, row, status, imageBlackOut);
                                    }
                                } else {
                                    var statuses = GetTargetStatuses();
                                    var status = statuses == null ? null : statuses.Where(s => s.StatusId == timer.StatusId).FirstOrDefault();
                                    DrawMainWindow(ref timer, config, imageSize, col, row, status, imageBlackOut);
                                }
                                index++;
                                if (index != ListTimer.Count && ListTimer[index].ObjectId != ListTimer[index - 1].ObjectId) break;
                            }
                        }
                    } else {
                        for (var row = 0; row < RaidMitigationRecaster.MaxRow; row++) {
                            if (index == ListTimer.Count) break;
                            for (var col = RaidMitigationRecaster.MaxCol; col > 0; col--) {
                                if (index == ListTimer.Count) break;
                                var timer = ListTimer[index];
                                if (timer.IsBuff) {
                                    if(DalamudService.PartyList.Count == 0) {
                                        // solo
                                        var statuses = DalamudService.ClientState.LocalPlayer.StatusList;
                                        var status = statuses == null ? null : statuses.Where(s => s.StatusId == timer.StatusId).FirstOrDefault();
                                        DrawMainWindow(ref timer, config, imageSize, col, row, status, imageBlackOut);
                                    } else {
                                        // party
                                        var statuses = DalamudService.PartyList.Where(p => p.ObjectId == timer.ObjectId).FirstOrDefault().Statuses;
                                        var status = statuses == null ? null : statuses.Where(s => s.StatusId == timer.StatusId).FirstOrDefault();
                                        DrawMainWindow(ref timer, config, imageSize, col, row, status, imageBlackOut);
                                    }
                                } else {
                                    var statuses = GetTargetStatuses();
                                    var status = statuses == null ? null : statuses.Where(s => s.StatusId == timer.StatusId).FirstOrDefault();
                                    DrawMainWindow(ref timer, config, imageSize, col, row, status, imageBlackOut);
                                }
                                index++;
                                if (index != ListTimer.Count && ListTimer[index].ObjectId != ListTimer[index - 1].ObjectId) break;
                            }
                        }
                    }
                    ImGui.PopFont();
                    ImGui.End();
                    isBegin = false;
                }
            } catch (Exception e) {
                PluginLog.Error(e.Message + "\n" + e.StackTrace);
            } finally {
                if(isBegin) {
                    ImGui.PopFont();
                    ImGui.End();
                }
            }
        }

        internal static void DrawMainWindow(
            ref TimerModel.Timer timer,
            Config config,
            float imageSize,
            int col,
            int row,
            Dalamud.Game.ClientState.Statuses.Status? status,
            IDalamudTextureWrap imageBlackOut) {

            var cursolPosX = col * (imageSize + config.PaddingX) + 10f;
            var cursolPosY = row * (imageSize + config.PaddingY);

            ImGui.SetCursorPos(new Vector2(cursolPosX + 1f, cursolPosY));
            ImGui.SetNextItemWidth(imageSize);
            ImGui.Image(timer.Image.ImGuiHandle, new Vector2(imageSize, imageSize));

            var dispTime = string.Empty;
            var fontColor = RaidMitigationRecaster.Black;

            // PluginLog.Information("Stopwatch IsRunnning; " + timer.StopWatch.IsRunning.ToString());

            if (status != null) {
                // effect time
                fontColor = RaidMitigationRecaster.Red;
                if (!timer.StopWatch.IsRunning) {
                    // effect start
                    timer.StopWatch.Start();
                    PluginLog.Information("Stopwatch Start;");
                }
                dispTime = (timer.RecastTime - timer.StopWatch.Elapsed.TotalMilliseconds / 1000).ToString("#");
            } else {
                // no effect time
                fontColor = RaidMitigationRecaster.Black;
                if (timer.StopWatch.IsRunning) {
                    if (timer.RecastTime <= timer.StopWatch.Elapsed.TotalMilliseconds / 1000) {
                        timer.StopWatch.Stop();
                        timer.StopWatch.Reset();
                        PluginLog.Information("Stopwatch Stop;");
                        dispTime = string.Empty;
                    } else {
                        // recasting
                        ImGui.SetCursorPos(new Vector2(cursolPosX + 1f, cursolPosY));
                        ImGui.SetNextItemWidth(imageSize);
                        ImGui.Image(imageBlackOut.ImGuiHandle, new Vector2(imageSize, imageSize));
                        dispTime = (timer.RecastTime - timer.StopWatch.Elapsed.TotalMilliseconds / 1000).ToString("#");
                    }
                }
            }

            if (dispTime != string.Empty) {
                var fontOffsetX = config.FontOffsetX + (ImGui.CalcTextSize(dispTime).X / dispTime.Length * (3 - dispTime.Length) / 2);
                cursolPosX = col * (imageSize + config.PaddingX) + 10f + fontOffsetX;
                cursolPosY = row * (imageSize + config.PaddingY) + config.FontOffsetY;

                ImGui.PushStyleColor(ImGuiCol.Text, fontColor);
                ImGui.SetCursorPos(new Vector2(cursolPosX + 1f, cursolPosY));
                ImGui.SetNextItemWidth(imageSize);
                ImGui.Text(dispTime);

                ImGui.SetCursorPos(new Vector2(cursolPosX, cursolPosY + 1f));
                ImGui.SetNextItemWidth(imageSize);
                ImGui.Text(dispTime);

                ImGui.SetCursorPos(new Vector2(cursolPosX - 1f, cursolPosY));
                ImGui.SetNextItemWidth(imageSize);
                ImGui.Text(dispTime);

                ImGui.SetCursorPos(new Vector2(cursolPosX, cursolPosY - 1f));
                ImGui.SetNextItemWidth(imageSize);
                ImGui.Text(dispTime);
                ImGui.PopStyleColor();

                ImGui.PushStyleColor(ImGuiCol.Text, RaidMitigationRecaster.White);
                ImGui.SetCursorPos(new Vector2(cursolPosX, cursolPosY));
                ImGui.SetNextItemWidth(imageSize);
                ImGui.Text(dispTime);
                ImGui.PopStyleColor();
            }
        }

        internal unsafe static Vector2? GetPtlistPosition() {
            var ptlist = DalamudService.GameGui.GetAddonByName("_PartyList", 1);
            if (ptlist != IntPtr.Zero) {
                var ptlistAtk = (AtkUnitBase*)ptlist;
                var x = ptlistAtk->X;
                var y = ptlistAtk->Y;
                if (ptlistAtk->IsVisible) {
                    return new Vector2(x, y);
                }
            }
            return null;
        }

        internal static bool IsChangedPartyList(ref uint localPlayerClassJobId, ref IPartyList localPartyList) {
            // TODO

            // in combat
            if (DalamudService.Condition[ConditionFlag.InCombat]) return false;

            // init?
            if(localPartyList == null) {
                localPartyList = DalamudService.PartyList;
                localPlayerClassJobId = DalamudService.ClientState.LocalPlayer.ClassJob.Id;
                PluginLog.Information("IsChangedPartyList.");
                return true;
            }

            // solo?
            if (localPartyList.Count == 0) {
                // solo => is change job?
                if (localPlayerClassJobId == null || (localPlayerClassJobId != DalamudService.ClientState.LocalPlayer.ClassJob.Id)) {
                    localPlayerClassJobId = DalamudService.ClientState.LocalPlayer.ClassJob.Id;
                    PluginLog.Information("IsChangedPartyList.");
                    return true;
                } else {
                    return false;
                }
            } else {
                // party => get ins partylist
                var instancePartyList = DalamudService.PartyList;

                // compare local to instance
                if(localPartyList.Count() != instancePartyList.Count()) {
                    // get instance, set local
                    localPartyList = instancePartyList;
                    PluginLog.Information("IsChangedPartyList.");
                    return true;
                }
                for (int i = 0; i < localPartyList.Count(); i++) {
                    if (localPartyList[0].ObjectId != instancePartyList[0].ObjectId || localPartyList[0].ClassJob.Id != instancePartyList[0].ClassJob.Id) {
                        // get instance, set local
                        localPartyList = instancePartyList;
                        PluginLog.Information("IsChangedPartyList.");
                        return true;
                    }
                }
                return false;
            }
        }

        private static List<Dalamud.Game.ClientState.Statuses.Status> GetTargetStatuses() {
            GameObject target = DalamudService.TargetManager.Target;
            if (target is Dalamud.Game.ClientState.Objects.Types.BattleChara b) {
                return b.StatusList.Where(s => s.StatusId != 0).ToList();
            }
            return null;
        }

        private static unsafe bool IsEnabledHotbar(AddonActionBarBase* a) {
            if (a is null) return false;
            if (!((AtkUnitBase*)a)->IsVisible) return false;
            return true;
        }

        public static void UpdateTimers(
            List<ActionModel.Action> actions,
            uint localPlayerClassJobId,
            IPartyList localPartyList,
            ref List<TimerModel.Timer> timers) {

            timers = new List<TimerModel.Timer>();

            if (localPartyList.Count == 0) {
                // solo
                var action = actions.Where(a => a.ClassJobId == localPlayerClassJobId).ToList();
                foreach (var i in Enumerable.Range(0, action.Count())) {
                    TimerModel.Timer timer = new TimerModel.Timer();
                    timer.ClassJobId = action[i].ClassJobId;
                    timer.ActionId = action[i].ActionId;
                    timer.StatusId = action[i].StatusId;
                    timer.IsBuff = action[i].IsBuff;
                    timer.IsThrow = action[i].IsThrow;
                    timer.RecastTime = action[i].RecastTime;
                    timer.Image = action[i].Image;
                    timer.ObjectId = DalamudService.ClientState.LocalPlayer.ObjectId;
                    timer.RemainingTime = 0f;
                    timer.StopWatch = new Stopwatch();
                    timers.Add(timer);
                }
            } else {
                // party
                foreach (var i in Enumerable.Range(0, localPartyList.Count())) {
                    var partyMember = localPartyList[i];
                    var action = actions.Where(a => a.ClassJobId == partyMember.ClassJob.Id).ToList();
                    foreach (var j in Enumerable.Range(0, action.Count())) {
                        TimerModel.Timer timer = new TimerModel.Timer();
                        timer.ClassJobId = action[j].ClassJobId;
                        timer.ActionId = action[j].ActionId;
                        timer.StatusId = action[j].StatusId;
                        timer.IsBuff = action[j].IsBuff;
                        timer.IsThrow = action[j].IsThrow;
                        timer.RecastTime = action[j].RecastTime;
                        timer.Image = action[j].Image;
                        timer.ObjectId = partyMember.ObjectId;
                        timer.RemainingTime = 0f;
                        timer.StopWatch = new Stopwatch();
                        timers.Add(timer);
                    }
                }
            }
            // PluginLog.Information("UpdateTimers.");
        }

        // --- Debug -----------------------------------
        internal static void DrawDebugWindow(ref Config config) {
            PlayerCharacter localPlayer = DalamudService.ClientState.LocalPlayer;
            if (ImGui.Begin("[DBG]Statuses", ImGuiWindowFlags.AlwaysAutoResize)) {
                var playerStatuses = localPlayer.StatusList.Where(s => s.StatusId != 0).ToList();
                ImGui.Text("ObjectId: " + localPlayer.ObjectId.ToString());
                ImGui.Text("ClassJobId: " + localPlayer.ClassJob.Id.ToString());
                ImGui.Text("ClassJob: " + Enum.GetName(typeof(JobIds), localPlayer.ClassJob.Id));
                ImGui.Text("statuses.count: " + playerStatuses.Count);
                ImGui.Separator();
                foreach (var i in Enumerable.Range(0, playerStatuses.Count)) {
                    ImGui.Text("StatusId[" + i.ToString() + "]: " + playerStatuses[i].StatusId.ToString());
                    ImGui.Text("RemainingTime[" + i.ToString() + "]: " + playerStatuses[i].RemainingTime.ToString("#"));
                    ImGui.Text("");
                }
                ImGui.Separator();

                var targetStatus = GetTargetStatuses();
                if (targetStatus != null) {
                    ImGui.Text("statuses.count: " + targetStatus.Count);
                    foreach (var i in Enumerable.Range(0, targetStatus.Count)) {
                        ImGui.Text("StatusId[" + i.ToString() + "]: " + targetStatus[i].StatusId.ToString());
                        ImGui.Text("RemainingTime[" + i.ToString() + "]: " + targetStatus[i].RemainingTime.ToString("#"));
                        ImGui.Text("");
                    }
                }

                ImGui.Separator();

                var partyList = DalamudService.PartyList;
                ImGui.Text("PartyList.Count: " + partyList.Count);
                if (partyList.Count != 0) {
                    foreach (var i in Enumerable.Range(0, partyList.Count)) {
                        ImGui.Text("ObjectId[" + i.ToString() + "]: " + partyList[i].ObjectId.ToString());
                        ImGui.Text("Name[" + i.ToString() + "]: " + partyList[i].Name);
                        ImGui.Text("ClassJob.Id[" + i.ToString() + "]: " + partyList[i].ClassJob.Id.ToString());
                        ImGui.Text("ClassJob.Name[" + i.ToString() + "]: " + Enum.GetName(typeof(JobIds), partyList[i].ClassJob.Id));
                        ImGui.Text("");
                    }
                }

                ImGui.End();
            }
        }

        public static unsafe void DrawDebugHotbarInfo() {
            List<string> HotbarAddonName = new List<string>() {
                "_ActionBar",
                "_ActionBar04",
                "_ActionBar05",
                "_ActionBar07",
            };
            foreach (var i in Enumerable.Range(0, HotbarAddonName.Count)) {
                var hotbarNo = (i + 1).ToString();
                var addonName = HotbarAddonName[i];
                var a = (AddonActionBarBase*)DalamudService.GameGui.GetAddonByName(addonName);
                if (!IsEnabledHotbar(a)) continue;
                if (ImGui.Begin("[DBG]Hotbar" + hotbarNo, ImGuiWindowFlags.AlwaysAutoResize)) {
                    foreach (var index in Enumerable.Range(0, a->Slot.Length)) {
                        var slot = a->Slot.GetPointer(index);
                        var positonX = slot->ComponentDragDrop->AtkComponentBase.OwnerNode->AtkResNode.ScreenX;
                        var positonY = slot->ComponentDragDrop->AtkComponentBase.OwnerNode->AtkResNode.ScreenY;
                        var actionId = slot->ActionId;
                        ImGui.Text("Hotbar" + hotbarNo + "-" + (index + 1).ToString());
                        ImGui.Text("ActionId: " + actionId.ToString());
                        ImGui.Text("X: " + positonX.ToString());
                        ImGui.SameLine();
                        ImGui.Text("Y: " + positonY.ToString());
                        ImGui.Separator();
                    }
                }
                ImGui.End();
            }
        }
    }
}