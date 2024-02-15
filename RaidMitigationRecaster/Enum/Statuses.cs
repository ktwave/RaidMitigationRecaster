using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO
namespace RaidMitigationRecaster.Enums {
    public static class Statuses {
        public class Buff {
            public enum HEALERandMAGICAL {
                Swiftcast = 167, // 迅速魔
            }

            public enum PLD {
                DivineVeil = 1362, // ディヴァインヴェール
                PassageOfArms = 1175, // パッセージ・オブ・アームズ
                HallowedGround = 82, // インビンシブル
            }

            public enum WAR {
                ShakeItOff = 2108, // シェイクオフ
                Bloodwhetting = 2680, // 原初の血気
                NascentFlash = 16464, // 原初の猛り
                Holmgang = 409, // ホルムギャング
            }

            public enum DRK {
                DarkMissionary = 1894, // ダークミッショナリー
                TheBlackestNight = 1178, // ブラックナイト
                LivingDead = 810, // リビングデッド
            }

            public enum GNB {
                HeartOfLight = 1839, // ハート・オブ・ライト
                HeartOfCorundum = 2685, // ハート・オブ・コランダム
                Superbolide = 1836, // ボーライド
            }



            public enum WHM {
                Asylum = 1911, // アサイラム
                Temperance = 1872, // テンパランス
                LilyBell = 2709, // リタージー・オブ・ベル
            }

            public enum SCH {
                FeyIllumination = 317, // フェイイルミネーション
                SacredSoil = 1944, // 陣
                Expedient = 2712, // 疾風怒濤の計
            }

            public enum AST {
                Macrocosmos = 2718, // マクロコスモス
                EarthlyStar = 1224, // アーサリースター
                CollectiveUnconscious = 956, // 運命の輪
                NeutralSect = 1892, // ニュートラルセクト
            }

            public enum SGE {
                Holos = 3365, // ホーリズム
                Panhaima = 2613, // パンハイマ
                Physis2 = 2620, // ピュシスII
                Kerachole = 2618, // ケーラコレ
            }

            public enum MNK {
                Mantra = 102,// マントラ
            }

            public enum RPR {
                ArcaneCrest = 2597,// アルケインクレスト
            }

            public enum BRD {
                Troubadour = 1202, // 地神のミンネ
                NaturesMinne = 1934, // トルバドゥール
                Pian = 866,
            }

            public enum MCH {
                Tactician = 1951, // タクティシャン
            }

            public enum DNC {
                ShieldSamba = 1826, // 守りのサンバ
                Improvisation = 2695, // インプロビゼーション
                // CuringWaltz = 16015, // 癒やしのワルツ
            }

            public enum RDM {
                MagickBarrier = 2707, // パマジグ
            }
        }

        public class DeBuff {
            public enum TANK {
                Reprisal = 1193, // リプライザル
            }

            public enum MELEE {
                Feint = 1195, // 牽制
            }

            public enum MCH {
                Dismantle = 860, // ウェポンブレイク
            }

            public enum MAGICAL {
                Addle = 1203, //アドル
            }
        }
    }
}
