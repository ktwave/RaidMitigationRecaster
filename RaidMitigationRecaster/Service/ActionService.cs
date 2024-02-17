using Dalamud.Interface.Internal;
using RaidMitigationRecaster.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidMitigationRecaster.Service {
    internal class ActionService {
        internal static List<ActionModel.Action> SetActions(Config config, Dalamud.Plugin.DalamudPluginInterface pluginInterface) {
            List<ActionModel.Action> result = new List<ActionModel.Action>();

            // PLD
            ActionModel.Action a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.PLD;
            a.ActionId = (int)Enums.Actions.TANK.Reprisal;
            a.StatusId = (int)Enums.Statuses.DeBuff.TANK.Reprisal;
            a.RecastTime = 60f;
            a.IsBuff = false;
            a.IsThrow = false;
            var ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.PLD;
            a.ActionId = (int)Enums.Actions.PLD.PassageOfArms;
            a.StatusId = (int)Enums.Statuses.Buff.PLD.PassageOfArms;
            a.RecastTime = 120f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.PLD;
            a.ActionId = (int)Enums.Actions.PLD.DivineVeil;
            a.StatusId = (int)Enums.Statuses.Buff.PLD.DivineVeil;
            a.RecastTime = 90f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.PLD;
            a.ActionId = (int)Enums.Actions.PLD.HallowedGround;
            a.StatusId = (int)Enums.Statuses.Buff.PLD.HallowedGround;
            a.RecastTime = 420f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            // WAR
            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.WAR;
            a.ActionId = (int)Enums.Actions.TANK.Reprisal;
            a.StatusId = (int)Enums.Statuses.DeBuff.TANK.Reprisal;
            a.RecastTime = 60f;
            a.IsBuff = false;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.WAR;
            a.ActionId = (int)Enums.Actions.WAR.ShakeItOff;
            a.StatusId = (int)Enums.Statuses.Buff.WAR.ShakeItOff;
            a.RecastTime = 90f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.WAR;
            a.ActionId = (int)Enums.Actions.WAR.NascentFlash;
            a.StatusId = (int)Enums.Statuses.Buff.WAR.NascentFlash;
            a.RecastTime = 25f;
            a.IsBuff = true;
            a.IsThrow = true;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.WAR;
            a.ActionId = (int)Enums.Actions.WAR.Bloodwhetting;
            a.StatusId = (int)Enums.Statuses.Buff.WAR.Bloodwhetting;
            a.RecastTime = 25f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.WAR;
            a.ActionId = (int)Enums.Actions.WAR.Holmgang;
            a.StatusId = (int)Enums.Statuses.Buff.WAR.Holmgang;
            a.RecastTime = 240f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            // DRK
            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.DRK;
            a.ActionId = (int)Enums.Actions.TANK.Reprisal;
            a.StatusId = (int)Enums.Statuses.DeBuff.TANK.Reprisal;
            a.RecastTime = 60f;
            a.IsBuff = false;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.DRK;
            a.ActionId = (int)Enums.Actions.DRK.TheBlackestNight;
            a.StatusId = (int)Enums.Statuses.Buff.DRK.TheBlackestNight;
            a.RecastTime = 15f;
            a.IsBuff = true;
            a.IsThrow = true;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.DRK;
            a.ActionId = (int)Enums.Actions.DRK.DarkMissionary;
            a.StatusId = (int)Enums.Statuses.Buff.DRK.DarkMissionary;
            a.RecastTime = 90f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.DRK;
            a.ActionId = (int)Enums.Actions.DRK.LivingDead;
            a.StatusId = (int)Enums.Statuses.Buff.DRK.LivingDead;
            a.RecastTime = 300f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            // GNB
            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.GNB;
            a.ActionId = (int)Enums.Actions.TANK.Reprisal;
            a.StatusId = (int)Enums.Statuses.DeBuff.TANK.Reprisal;
            a.RecastTime = 60f;
            a.IsBuff = false;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.GNB;
            a.ActionId = (int)Enums.Actions.GNB.HeartOfLight;
            a.StatusId = (int)Enums.Statuses.Buff.GNB.HeartOfLight;
            a.RecastTime = 90f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.GNB;
            a.ActionId = (int)Enums.Actions.GNB.HeartOfCorundum;
            a.StatusId = (int)Enums.Statuses.Buff.GNB.HeartOfCorundum;
            a.RecastTime = 25f;
            a.IsBuff = true;
            a.IsThrow = true;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.GNB;
            a.ActionId = (int)Enums.Actions.GNB.Superbolide;
            a.StatusId = (int)Enums.Statuses.Buff.GNB.Superbolide;
            a.RecastTime = 360f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            // WHM
            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.WHM;
            a.ActionId = (int)Enums.Actions.HEALERandMAGICAL.Swiftcast;
            a.StatusId = (int)Enums.Statuses.Buff.HEALERandMAGICAL.Swiftcast;
            a.RecastTime = 60f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.WHM;
            a.ActionId = (int)Enums.Actions.WHM.LilyBell;
            a.StatusId = (int)Enums.Statuses.Buff.WHM.LilyBell;
            a.RecastTime = 180f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.WHM;
            a.ActionId = (int)Enums.Actions.WHM.Temperance;
            a.StatusId = (int)Enums.Statuses.Buff.WHM.Temperance;
            a.RecastTime = 120f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.WHM;
            a.ActionId = (int)Enums.Actions.WHM.Asylum;
            a.StatusId = (int)Enums.Statuses.Buff.WHM.Asylum;
            a.RecastTime = 90f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            // SCH
            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.SCH;
            a.ActionId = (int)Enums.Actions.HEALERandMAGICAL.Swiftcast;
            a.StatusId = (int)Enums.Statuses.Buff.HEALERandMAGICAL.Swiftcast;
            a.RecastTime = 60f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.SCH;
            a.ActionId = (int)Enums.Actions.SCH.SacredSoil;
            a.StatusId = (int)Enums.Statuses.Buff.SCH.SacredSoil;
            a.RecastTime = 30f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.SCH;
            a.ActionId = (int)Enums.Actions.SCH.FeyIllumination;
            a.StatusId = (int)Enums.Statuses.Buff.SCH.FeyIllumination;
            a.RecastTime = 120f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.SCH;
            a.ActionId = (int)Enums.Actions.SCH.Expedient;
            a.StatusId = (int)Enums.Statuses.Buff.SCH.Expedient;
            a.RecastTime = 120f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            // AST
            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.AST;
            a.ActionId = (int)Enums.Actions.HEALERandMAGICAL.Swiftcast;
            a.StatusId = (int)Enums.Statuses.Buff.HEALERandMAGICAL.Swiftcast;
            a.RecastTime = 60f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.AST;
            a.ActionId = (int)Enums.Actions.AST.NeutralSect;
            a.StatusId = (int)Enums.Statuses.Buff.AST.NeutralSect;
            a.RecastTime = 120f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.AST;
            a.ActionId = (int)Enums.Actions.AST.CollectiveUnconscious;
            a.StatusId = (int)Enums.Statuses.Buff.AST.CollectiveUnconscious;
            a.RecastTime = 60f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.AST;
            a.ActionId = (int)Enums.Actions.AST.EarthlyStar;
            a.StatusId = (int)Enums.Statuses.Buff.AST.EarthlyStar;
            a.RecastTime = 60f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.AST;
            a.ActionId = (int)Enums.Actions.AST.Macrocosmos;
            a.StatusId = (int)Enums.Statuses.Buff.AST.Macrocosmos;
            a.RecastTime = 180f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            // SGE
            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.SGE;
            a.ActionId = (int)Enums.Actions.HEALERandMAGICAL.Swiftcast;
            a.StatusId = (int)Enums.Statuses.Buff.HEALERandMAGICAL.Swiftcast;
            a.RecastTime = 60f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.SGE;
            a.ActionId = (int)Enums.Actions.SGE.Panhaima;
            a.StatusId = (int)Enums.Statuses.Buff.SGE.Panhaima;
            a.RecastTime = 120f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.SGE;
            a.ActionId = (int)Enums.Actions.SGE.Physis2;
            a.StatusId = (int)Enums.Statuses.Buff.SGE.Physis2;
            a.RecastTime = 60f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.SGE;
            a.ActionId = (int)Enums.Actions.SGE.Kerachole;
            a.StatusId = (int)Enums.Statuses.Buff.SGE.Kerachole;
            a.RecastTime = 30f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            // MNK
            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.MNK;
            a.ActionId = (int)Enums.Actions.MELEE.Feint;
            a.StatusId = (int)Enums.Statuses.DeBuff.MELEE.Feint;
            a.RecastTime = 90f;
            a.IsBuff = false;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.MNK;
            a.ActionId = (int)Enums.Actions.MNK.Mantra;
            a.StatusId = (int)Enums.Statuses.Buff.MNK.Mantra;
            a.RecastTime = 90f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            // DRG
            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.DRG;
            a.ActionId = (int)Enums.Actions.MELEE.Feint;
            a.StatusId = (int)Enums.Statuses.DeBuff.MELEE.Feint;
            a.RecastTime = 90f;
            a.IsBuff = false;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            // NIN
            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.NIN;
            a.ActionId = (int)Enums.Actions.MELEE.Feint;
            a.StatusId = (int)Enums.Statuses.DeBuff.MELEE.Feint;
            a.RecastTime = 90f;
            a.IsBuff = false;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            // SAM
            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.SAM;
            a.ActionId = (int)Enums.Actions.MELEE.Feint;
            a.StatusId = (int)Enums.Statuses.DeBuff.MELEE.Feint;
            a.RecastTime = 90f;
            a.IsBuff = false;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            // RPR
            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.RPR;
            a.ActionId = (int)Enums.Actions.MELEE.Feint;
            a.StatusId = (int)Enums.Statuses.DeBuff.MELEE.Feint;
            a.RecastTime = 90f;
            a.IsBuff = false;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.RPR;
            a.ActionId = (int)Enums.Actions.RPR.ArcaneCrest;
            a.StatusId = (int)Enums.Statuses.Buff.RPR.ArcaneCrest;
            a.RecastTime = 30f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            // BRD
            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.BRD;
            a.ActionId = (int)Enums.Actions.BRD.NaturesMinne;
            a.StatusId = (int)Enums.Statuses.Buff.BRD.NaturesMinne;
            a.RecastTime = 120f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.BRD;
            a.ActionId = (int)Enums.Actions.BRD.Troubadour;
            a.StatusId = (int)Enums.Statuses.Buff.BRD.Troubadour;
            a.RecastTime = 90f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            // MCH
            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.MCH;
            a.ActionId = (int)Enums.Actions.MCH.Tactician;
            a.StatusId = (int)Enums.Statuses.Buff.MCH.Tactician;
            a.RecastTime = 90f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.MCH;
            a.ActionId = (int)Enums.Actions.MCH.Dismantle;
            a.StatusId = (int)Enums.Statuses.DeBuff.MCH.Dismantle;
            a.RecastTime = 120f;
            a.IsBuff = false;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            // DNC
            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.DNC;
            a.ActionId = (int)Enums.Actions.DNC.ShieldSamba;
            a.StatusId = (int)Enums.Statuses.Buff.DNC.ShieldSamba;
            a.RecastTime = 90f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.DNC;
            a.ActionId = (int)Enums.Actions.DNC.Improvisation;
            a.StatusId = (int)Enums.Statuses.Buff.DNC.Improvisation;
            a.RecastTime = 120f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            // BLM
            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.BLM;
            a.ActionId = (int)Enums.Actions.MAGICAL.Addle;
            a.StatusId = (int)Enums.Statuses.DeBuff.MAGICAL.Addle;
            a.RecastTime = 90f;
            a.IsBuff = false;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.BLM;
            a.ActionId = (int)Enums.Actions.HEALERandMAGICAL.Swiftcast;
            a.StatusId = (int)Enums.Statuses.Buff.HEALERandMAGICAL.Swiftcast;
            a.RecastTime = 60f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            // SMN
            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.SMN;
            a.ActionId = (int)Enums.Actions.MAGICAL.Addle;
            a.StatusId = (int)Enums.Statuses.DeBuff.MAGICAL.Addle;
            a.RecastTime = 90f;
            a.IsBuff = false;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.SMN;
            a.ActionId = (int)Enums.Actions.HEALERandMAGICAL.Swiftcast;
            a.StatusId = (int)Enums.Statuses.Buff.HEALERandMAGICAL.Swiftcast;
            a.RecastTime = 60f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            // RDM
            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.RDM;
            a.ActionId = (int)Enums.Actions.HEALERandMAGICAL.Swiftcast;
            a.StatusId = (int)Enums.Statuses.Buff.HEALERandMAGICAL.Swiftcast;
            a.RecastTime = 60f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.RDM;
            a.ActionId = (int)Enums.Actions.MAGICAL.Addle;
            a.StatusId = (int)Enums.Statuses.DeBuff.MAGICAL.Addle;
            a.RecastTime = 90f;
            a.IsBuff = false;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            a = new ActionModel.Action();
            a.ClassJobId = (int)Enums.JobIds.RDM;
            a.ActionId = (int)Enums.Actions.RDM.MagickBarrier;
            a.StatusId = (int)Enums.Statuses.Buff.RDM.MagickBarrier;
            a.RecastTime = 120f;
            a.IsBuff = true;
            a.IsThrow = false;
            ImagePath = Path.Combine(pluginInterface.AssemblyLocation.Directory?.FullName!, "images\\" + a.ActionId + ".png");
            a.Image = pluginInterface.UiBuilder.LoadImage(ImagePath);
            result.Add(a);

            return result;
        }
    }
}
