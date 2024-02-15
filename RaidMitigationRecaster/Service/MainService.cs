using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Interface.GameFonts;
using RaidMitigationRecaster.Enums;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;
using ImGuiNET;
using Microsoft.VisualBasic;
using RaidBuffRecaster.Service;
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
                ImGui.Separator();
                ImGui.Spacing();

                ImGui.Text("X座標のオフセット(X Offset)");
                var offsetX = config.OffsetX;
                ImGui.SetNextItemWidth(200f);
                if (ImGui.DragFloat(" ", ref offsetX, 0.5f)) {
                    config.OffsetX = offsetX;
                }
                ImGui.Spacing();

                ImGui.Text("Y座標のオフセット(Y Offset)");
                var offsetY = config.OffsetY;
                ImGui.SetNextItemWidth(200f);
                if (ImGui.DragFloat("  ", ref offsetY, 0.5f)) {
                    config.OffsetY = offsetY;
                }
                ImGui.Spacing();

                ImGui.Text("アイコンの拡大率(Icon Scale)");
                var size = config.Size;
                ImGui.SetNextItemWidth(200f);
                if (ImGui.DragFloat("   ", ref size, 1, 1, 300)) {
                    config.Size = size;
                }
                ImGui.Spacing();

                ImGui.Text("アイコンの横間隔(Icon Padding X)");
                var paddingX = config.PaddingX;
                ImGui.SetNextItemWidth(200f);
                if (ImGui.DragFloat("     ", ref paddingX, 0.5f, -100, 100)) {
                    config.PaddingX = paddingX;
                }
                ImGui.Spacing();

                ImGui.Text("アイコンの縦間隔(Icon Padding Y)");
                var paddingY = config.PaddingY;
                ImGui.SetNextItemWidth(200f);
                if (ImGui.DragFloat("       ", ref paddingY, 0.5f, -100, 100)) {
                    config.PaddingY = paddingY;
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
                if(partyList.Count != 0) {
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

        private static List<Dalamud.Game.ClientState.Statuses.Status> GetTargetStatuses() {
            GameObject target = DalamudService.TargetManager.Target;
            if (target is Dalamud.Game.ClientState.Objects.Types.BattleChara b) {
                return b.StatusList.Where(s => s.StatusId != 0).ToList();
            }
            return null;
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

        private static unsafe bool IsEnabledHotbar(AddonActionBarBase* a) {
            if (a is null) return false;
            if (!((AtkUnitBase*)a)->IsVisible) return false;
            return true;
        }

        internal static void DrawMainWindow(List<ActionModel.Action> ListActions, Config config) {
            var ptListPos = GetPtlistPosition();
            if (ptListPos == null) return;
            var x = ((Vector2)ptListPos).X;
            var y = ((Vector2)ptListPos).Y;

            ImGuiHelpers.ForceNextWindowMainViewport();
            ImGuiHelpers.SetNextWindowPosRelativeMainViewport(new Vector2(x + config.OffsetX, y + config.OffsetY));

            ImGui.PushFont(DalamudService.PluginInterface.UiBuilder.GetGameFontHandle(new GameFontStyle(config.Font.Value)).ImFont);

            if (ImGui.Begin("MainWindow", ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoBackground)) {
                var imageSize = (RaidMitigationRecaster.ImageSize * config.Size / 100);
                ImGui.SetWindowFontScale(config.Size / 100);

                for (int row = 0; row < RaidMitigationRecaster.MaxRow; row++) {
                    for (int col = 0; col < RaidMitigationRecaster.MaxCol; col++) {


                        var cursolPosX = col * (imageSize + config.PaddingX) + 10f;
                        var cursolPosY = row * (imageSize + config.PaddingY);

                        ImGui.PushStyleColor(ImGuiCol.Text, RaidMitigationRecaster.Black);
                        ImGui.SetCursorPos(new Vector2(cursolPosX + 1f, cursolPosY));
                        ImGui.SetNextItemWidth(imageSize);
                        ImGui.Text("300");

                        ImGui.SetCursorPos(new Vector2(cursolPosX, cursolPosY + 1f));
                        ImGui.SetNextItemWidth(imageSize);
                        ImGui.Text("300");

                        ImGui.SetCursorPos(new Vector2(cursolPosX - 1f, cursolPosY));
                        ImGui.SetNextItemWidth(imageSize);
                        ImGui.Text("300");

                        ImGui.SetCursorPos(new Vector2(cursolPosX, cursolPosY - 1f));
                        ImGui.SetNextItemWidth(imageSize);
                        ImGui.Text("300");

                        ImGui.PushStyleColor(ImGuiCol.Text, RaidMitigationRecaster.White);
                        ImGui.SetCursorPos(new Vector2(cursolPosX, cursolPosY));
                        ImGui.SetNextItemWidth(imageSize);
                        ImGui.Text("300");
                    }
                }
                ImGui.PopFont();
                ImGui.End();
            }
        }

        private static void DrawMyIcon(Config config) {
            PlayerCharacter localPlayer = DalamudService.ClientState.LocalPlayer;
            var startIndex = 0;
            var endIndex = RaidMitigationRecaster.MaxCol;
            if (!config.IsLeftAligin) {
                startIndex = RaidMitigationRecaster.MaxCol;
                endIndex = 0;
            }
            /*ActionList.Where(a => a.JobId == localPlayer.ClassJob.Id).ForEach(a => {

            });*/

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

        internal static bool IsChangedPartyList(ref List<uint> localPartyList) {
            // in combat or no party
            if (DalamudService.Condition[ConditionFlag.InCombat] || localPartyList == null) return false;

            // compare local to instance
            List<uint> instancePartyList = DalamudService.PartyList.Select(p => p.ClassJob.Id).Order().ToList();
            if(localPartyList == instancePartyList) return false;

            // get instance, set local
            localPartyList = instancePartyList;
            return true;
        }
    }
}