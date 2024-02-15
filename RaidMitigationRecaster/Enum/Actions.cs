using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidMitigationRecaster.Enums {
    public class Actions {
        public enum TANK {
            Reprisal = 7535, // リプライザル
        }

        public enum PLD {
            DivineVeil = 3540, // ディヴァインヴェール
            PassageOfArms = 7385, // パッセージ・オブ・アームズ
            HallowedGround = 30, // インビンシブル
        }

        public enum WAR {
            ShakeItOff = 7388, // シェイクオフ
            Bloodwhetting = 25751, // 原初の血気
            NascentFlash = 16464, // 原初の猛り
            Holmgang = 43, // ホルムギャング
        }

        public enum DRK {
            DarkMissionary = 16471, // ダークミッショナリー
            TheBlackestNight = 7393, // ブラックナイト
            LivingDead = 3638, // リビングデッド
        }

        public enum GNB {
            HeartOfLight = 16160, // ハート・オブ・ライト
            HeartOfCorundum = 25758, // ハート・オブ・コランダム
            Superbolide = 16152, // ボーライド
        }

        public enum HEALERandMAGICAL {
            Swiftcast = 7561, // 迅速魔
        }

        public enum WHM {
            Asylum = 3569, // アサイラム
            Temperance = 16536, // テンパランス
            Benediction = 140, // ベネディクション
            LilyBell = 25862, // リタージー・オブ・ベル
        }

        public enum SCH {
            FeyIllumination = 16538, // フェイイルミネーション
            SacredSoil = 188, // 野戦治療の陣
            SummonSeraph = 16545, // サモン・セラフィム
            Expedient = 25868, // 疾風怒濤の計
        }

        public enum AST {
            Macrocosmos = 25874, // マクロコスモス
            EarthlyStar = 7439, // アーサリースター
            CollectiveUnconscious = 3613, // 運命の輪
            NeutralSect = 16559, // ニュートラルセクト
        }

        public enum SGE {
            Holos = 24310, // ホーリズム
            Panhaima = 24311, // パンハイマ
            Physis2 = 24302, // ピュシスII
            Pneuma = 24318, // プネウマ
            Kerachole = 24298, // ケーラコレ
        }

        public enum MELEE {
            Feint = 7549, // 牽制
        }

        public enum MNK {
            Mantra = 65,// マントラ
        }

        public enum RPR {
            ArcaneCrest = 24404,// アルケインクレスト
        }

        public enum BRD {
            Troubadour = 7405, // 地神のミンネ
            NaturesMinne = 7408, // トルバドゥール
        }

        public enum MCH {
            Tactician = 16889, // タクティシャン
            Dismantle = 2887, // ウェポンブレイク
        }

        public enum DNC {
            ShieldSamba = 16012, // 守りのサンバ
            Improvisation = 16014, // インプロビゼーション
            CuringWaltz = 16015, // 癒やしのワルツ
        }

        public enum MAGICAL {
            Addle = 7560, //アドル
        }

        public enum RDM {
            MagickBarrier = 25857, // パマジグ
        }
    }
}
