using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Party;
using FFXIVClientStructs.FFXIV.Client.System.Framework;
using FFXIVClientStructs.FFXIV.Client.UI.Misc;
using RaidMitigationRecaster.Enums;
using RaidMitigationRecaster.Enums;
using System.Collections.Generic;

namespace RaidMitigationRecaster.Service {
    public static class PartyOrderHelper {

        private enum PartySortingSetting {
            Tank_Healer_DPS = 0,
            Tank_DPS_Healer = 1,
            Healer_Tank_DPS = 2,
            Healer_DPS_Tank = 3,
            DPS_Tank_Healer = 4,
            DPS_Healer_Tank = 5,
            Count = 6
        }

        public class PartyRoles {
            internal int Tank;
            internal int Healer;
            internal int DPS;
            internal int Other;

            public PartyRoles() {
                Tank = 0;
                Healer = 0;
                DPS = 0;
                Other = 0;
            }

            public PartyRoles(int tank, int healer, int dps, int other) {
                Tank = tank;
                Healer = healer;
                DPS = dps;
                Other = other;
            }
        }

        public static PartyRoles GetPartyRoles() {
            PlayerCharacter? player = DalamudService.ClientState.LocalPlayer;
            if (player == null) { return null; }
            JobRoles role = RoleForJob(player.ClassJob.Id);

            PartySortingSetting? setting = GetPartySortingSetting(role);
            if (!setting.HasValue) { return null; }
            PartyRoles roleWeights = GetRoleWeights(role, setting.Value);
            return roleWeights;
        }

        public static PartyRoles GetPartyRoles(List<PartyMember> members) {
            PlayerCharacter? player = DalamudService.ClientState.LocalPlayer;
            if (player == null) { return null; }
            JobRoles role = RoleForJob(player.ClassJob.Id);

            PartySortingSetting? setting = GetPartySortingSetting(role);
            if (!setting.HasValue) { return null; }
            PartyRoles roleWeights = GetRoleWeights(role, setting.Value);
            return roleWeights;
        }

        public static JobRoles RoleForJob(uint id) {
            switch (id) {
                case (uint)JobIds.GLA:
                case (uint)JobIds.MRD:
                case (uint)JobIds.PLD:
                case (uint)JobIds.WAR:
                case (uint)JobIds.DRK:
                case (uint)JobIds.GNB:
                    return JobRoles.Tank;

                case (uint)JobIds.CNJ:
                case (uint)JobIds.WHM:
                case (uint)JobIds.SCH:
                case (uint)JobIds.AST:
                case (uint)JobIds.SGE:
                    return JobRoles.Healer;

                case (uint)JobIds.PGL:
                case (uint)JobIds.LNC:
                case (uint)JobIds.ROG:
                case (uint)JobIds.MNK:
                case (uint)JobIds.DRG:
                case (uint)JobIds.NIN:
                case (uint)JobIds.SAM:
                case (uint)JobIds.RPR:
                    return JobRoles.DPSMelee;

                case (uint)JobIds.ARC:
                case (uint)JobIds.BRD:
                case (uint)JobIds.MCH:
                case (uint)JobIds.DNC:
                    return JobRoles.DPSRanged;

                case (uint)JobIds.THM:
                case (uint)JobIds.ACN:
                case (uint)JobIds.BLM:
                case (uint)JobIds.SMN:
                case (uint)JobIds.RDM:
                case (uint)JobIds.BLU:
                    return JobRoles.DPSCaster;

                default:
                    return JobRoles.Unknown;
            }
        }

        private static unsafe PartySortingSetting? GetPartySortingSetting(JobRoles role) {
            ConfigModule* config = ConfigModule.Instance();
            if (config == null) { return null; }

            ConfigOption option;
            switch (role) {
                case JobRoles.Tank: option = ConfigOption.PartyListSortTypeTank; break;
                case JobRoles.Healer: option = ConfigOption.PartyListSortTypeHealer; break;
                case JobRoles.DPSMelee:
                case JobRoles.DPSRanged:
                case JobRoles.DPSCaster: option = ConfigOption.PartyListSortTypeDps; break;

                default: option = ConfigOption.PartyListSortTypeOther; break;
            }

            Framework* framework = Framework.Instance();
            if (framework == null || framework->SystemConfig.CommonSystemConfig.UiConfig.ConfigCount <= (int)option) {
                return PartySortingSetting.Tank_Healer_DPS;
            }

            uint value = framework->SystemConfig.CommonSystemConfig.UiConfig.ConfigEntry[(int)option].Value.UInt;
            if (value < 0 || value > (int)PartySortingSetting.Count) { return null; }

            return (PartySortingSetting)value;
        }

        private static unsafe PartyRoles GetPartyCountByRole(List<PartyMember> members) {
            PartyRoles rolesCount = new PartyRoles();

            PlayerCharacter? player = DalamudService.ClientState.LocalPlayer;
            if (player == null) { return rolesCount; }

            foreach (PartyMember member in members) {
                if (member.ObjectId == player.ObjectId) { continue; }

                JobRoles role = RoleForJob(member.ClassJob.Id);
                switch (role) {
                    case JobRoles.Tank: rolesCount.Tank++; break;
                    case JobRoles.Healer: rolesCount.Healer++; break;
                    case JobRoles.DPSMelee:
                    case JobRoles.DPSRanged:
                    case JobRoles.DPSCaster: rolesCount.DPS++; break;
                    default: rolesCount.Other++; break;
                }
            }

            return rolesCount;
        }

        private static unsafe PartyRoles GetRoleWeights(JobRoles role, PartySortingSetting setting) {
            if (role == JobRoles.Crafter || role == JobRoles.Gatherer || role == JobRoles.Unknown) {
                return new PartyRoles(1, 1, 1, 0);
            }

            JobRoles mapRole = role == JobRoles.DPSRanged || role == JobRoles.DPSCaster ? JobRoles.DPSMelee : role;
            return RoleWeights[mapRole][setting];
        }

        private static Dictionary<JobRoles, Dictionary<PartySortingSetting, PartyRoles>> RoleWeights = new Dictionary<JobRoles, Dictionary<PartySortingSetting, PartyRoles>>() {
            [JobRoles.Tank] = new Dictionary<PartySortingSetting, PartyRoles>() {
                [PartySortingSetting.Tank_Healer_DPS] = new PartyRoles(0, 1, 2, 0),
                [PartySortingSetting.Tank_DPS_Healer] = new PartyRoles(0, 2, 1, 0),
                [PartySortingSetting.Healer_Tank_DPS] = new PartyRoles(1, 0, 2, 0),
                [PartySortingSetting.DPS_Tank_Healer] = new PartyRoles(2, 0, 1, 0),
                [PartySortingSetting.Healer_DPS_Tank] = new PartyRoles(1, 2, 0, 0),
                [PartySortingSetting.DPS_Healer_Tank] = new PartyRoles(2, 1, 0, 0)
            },

            [JobRoles.Healer] = new Dictionary<PartySortingSetting, PartyRoles>() {
                [PartySortingSetting.Tank_Healer_DPS] = new PartyRoles(0, 1, 2, 0),
                [PartySortingSetting.Tank_DPS_Healer] = new PartyRoles(0, 2, 1, 0),
                [PartySortingSetting.Healer_Tank_DPS] = new PartyRoles(1, 0, 2, 0),
                [PartySortingSetting.DPS_Tank_Healer] = new PartyRoles(1, 2, 0, 0),
                [PartySortingSetting.Healer_DPS_Tank] = new PartyRoles(2, 0, 1, 0),
                [PartySortingSetting.DPS_Healer_Tank] = new PartyRoles(2, 1, 0, 0)
            },

            [JobRoles.DPSMelee] = new Dictionary<PartySortingSetting, PartyRoles>() {
                [PartySortingSetting.Tank_Healer_DPS] = new PartyRoles(0, 1, 2, 0),
                [PartySortingSetting.Tank_DPS_Healer] = new PartyRoles(0, 2, 1, 0),
                [PartySortingSetting.Healer_Tank_DPS] = new PartyRoles(1, 0, 2, 0),
                [PartySortingSetting.DPS_Tank_Healer] = new PartyRoles(1, 2, 0, 0),
                [PartySortingSetting.Healer_DPS_Tank] = new PartyRoles(2, 0, 1, 0),
                [PartySortingSetting.DPS_Healer_Tank] = new PartyRoles(2, 1, 0, 0)
            }
        };
    }
}
