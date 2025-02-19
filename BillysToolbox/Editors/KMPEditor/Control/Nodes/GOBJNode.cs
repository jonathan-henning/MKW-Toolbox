﻿using kartlib.Serial;
using static kartlib.Serial.KMP;

namespace KMP_Editor.Control.Nodes
{
    public class GOBJNode : Node
    {
        public _Section<_GOBJ> GOBJ { get; set; }

        public GOBJNode(KMP kmp)
        {
            GOBJ = kmp.GOBJ;
        }

        public override List<_ISectionEntry> GetData()
        {
            List<_ISectionEntry> result = new List<_ISectionEntry>();
            List<ushort> uniqueIds = new List<ushort>();
            foreach (_GOBJ obj in GOBJ.Entries)
            {
                if (!uniqueIds.Contains(obj.ID)) uniqueIds.Add(obj.ID);
            }

            foreach (ushort id in uniqueIds) result.Add(new GOBJGroupNode(GOBJ, id));
            return result;
        }

        public override string GetTitle(int index)
        {
            return ((GOBJGroupNode)GetData()[index]).ID.ToString();
        }

        public override void AddEntry()
        {
            // open window to determine which object
            ObjectBrowser ob = new ObjectBrowser();
            if (ob.ShowDialog() == DialogResult.OK)
            {
                _GOBJ entry = (_GOBJ)GOBJ.AddEntry();
                entry.ID = ob.SelectedID;
            }
        }

        public override void RemoveEntry(int index)
        {
            ObjectIDEnum id = ((GOBJGroupNode)GetData()[index]).ID;

            for (int i = GOBJ.Entries.Count - 1; i >= 0; i--)
                if (GOBJ.Entries[i].ID == (ushort)id) GOBJ.RemoveEntry(i);
        }

        public override void Populate(TreeNode node)
        {
            List<_ISectionEntry> data = GetData();

            node.Nodes.Clear();
            node.Tag = this;
            for (int i = 0; i < data.Count; i++)
            {
                TreeNode treeNode = new TreeNode(((GOBJGroupNode)data[i]).ID.ToString());
                treeNode.Tag = (GOBJGroupNode)data[i];
                treeNode.ImageIndex = 4;
                treeNode.SelectedImageIndex = 4;

                node.Nodes.Add(treeNode);
            }
        }
    }

    public class GOBJGroupNode : Node, _ISectionEntry
    {

        private List<_ISectionEntry> Objects;
        private _Section<_GOBJ> GOBJ;
        internal ObjectIDEnum ID;

        public GOBJGroupNode(_Section<_GOBJ> gobj, ushort id)
        {
            ID = (ObjectIDEnum)id;
            Objects = new List<_ISectionEntry>();
            GOBJ = gobj;

            foreach (_GOBJ obj in GOBJ.Entries)
            {
                if (obj.ID == (ushort)ID) Objects.Add(obj);
            }
        }

        public void Read(EndianReader reader)
        {
            throw new NotImplementedException();
        }

        public void Write(EndianWriter writer)
        {
            throw new NotImplementedException();
        }

        public override List<_ISectionEntry> GetData()
        {
            List<_ISectionEntry> result = new List<_ISectionEntry>();
            foreach (_GOBJ obj in GOBJ.Entries)
            {
                if (obj.ID == (ushort)ID) result.Add(obj);
            }
            return result;
        }

        public override string GetTitle(int index)
        {
            return "Instance " + index;
        }

        public override void AddEntry()
        {
            _GOBJ entry = (_GOBJ)GOBJ.AddEntry();
            entry.ID = (ushort)ID;
            Objects.Add(entry);
        }

        public override void RemoveEntry(int index)
        {
            for (int i = GOBJ.Entries.Count - 1; i >= 0; i--)
            {
                if (Objects[index] == GOBJ.Entries[i])
                {
                    GOBJ.RemoveEntry(i);
                }
            }
        }
    }

    public enum ObjectIDEnum : ushort
    {
        Null = 0,
        airblock = 1,
        Psea = 2,
        lensFX = 3,
        venice_nami = 4,
        sound_river = 5,
        sound_water_fall = 6,
        pocha = 7,
        sound_lake = 8,
        sound_big_fall = 9,
        sound_sea = 10,
        sound_fountain = 11,
        sound_volcano = 12,
        sound_audience = 13,
        sound_big_river = 14,
        sound_sand_fall = 15,
        sound_lift = 16,
        pochaYogan = 17,
        entry = 18,
        pochaMori = 19,
        eline_control = 20,
        sound_Mii = 21,
        begoman_manager = 22,
        ice = 23,
        startline2D = 24,
        itembox = 101,
        DummyPole = 102,
        flag = 103,
        flagBlend = 104,
        gnd_sphere = 105,
        gnd_trapezoid = 106,
        gnd_wave1 = 107,
        gnd_wave2 = 108,
        gnd_wave3 = 109,
        gnd_wave4 = 110,
        sun = 111,
        woodbox = 112,
        KmoonZ = 113,
        sunDS = 114,
        coin = 115,
        ironbox = 116,
        ItemDirect = 117,
        s_itembox = 118,
        pile_coin = 119,
        f_itembox = 201,
        MashBalloonGC = 202,
        WLwallGC = 203,
        CarA1 = 204,
        basabasa = 205,
        HeyhoShipGBA = 206,
        koopaBall = 207,
        kart_truck = 208,
        car_body = 209,
        skyship = 210,
        w_woodbox = 211,
        w_itembox = 212,
        w_itemboxline = 213,
        VolcanoBall1 = 214,
        penguin_s = 215,
        penguin_m = 216,
        penguin_l = 217,
        castleballoon1 = 218,
        dossunc = 219,
        dossunc_soko = 220,
        boble = 221,
        K_bomb_car = 222,
        K_bomb_car_dummy = 223,
        car_body_dummy = 224,
        kart_truck_dummy = 225,
        hanachan = 226,
        seagull = 227,
        moray = 228,
        crab = 229,
        basabasa_dummy = 230,
        CarA2 = 231,
        CarA3 = 232,
        Hwanwan = 233,
        HeyhoBallGBA = 234,
        Twanwan = 235,
        cruiserR = 236,
        bird = 237,
        sin_itembox = 238,
        Twanwan_ue = 239,
        BossHanachan = 240,
        Kdossunc = 241,
        BossHanachanHead = 242,
        K_bomb_car1 = 243,
        dummy = 301,
        dokan_sfc = 302,
        castletree1 = 303,
        castletree1c = 304,
        castletree2 = 305,
        castleflower1 = 306,
        mariotreeGC = 307,
        mariotreeGCc = 308,
        donkytree1GC = 309,
        donkytree2GC = 310,
        peachtreeGC = 311,
        peachtreeGCc = 312,
        npc_mii_a = 313,
        npc_mii_b = 314,
        npc_mii_c = 315,
        obakeblockSFCc = 316,
        WLarrowGC = 317,
        WLscreenGC = 318,
        WLdokanGC = 319,
        MarioGo64c = 320,
        PeachHunsuiGC = 321,
        kinokoT1 = 322,
        kinokoT2 = 323,
        pylon01 = 324,
        PalmTree = 325,
        parasol = 326,
        cruiser = 327,
        K_sticklift00 = 328,
        heyho2 = 329,
        HeyhoTreeGBAc = 330,
        MFaceBill = 331,
        truckChimSmk = 332,
        MiiObj01 = 333,
        MiiObj02 = 334,
        MiiObj03 = 335,
        gardentreeDS = 336,
        gardentreeDSc = 337,
        FlagA1 = 338,
        FlagA2 = 339,
        FlagB1 = 340,
        FlagB2 = 341,
        FlagA3 = 342,
        DKtreeA64 = 343,
        DKtreeA64c = 344,
        DKtreeB64 = 345,
        DKtreeB64c = 346,
        TownTreeDSc = 347,
        Piston = 348,
        oilSFC = 349,
        DKmarutaGCc = 350,
        DKropeGCc = 351,
        mii_balloon = 352,
        windmill = 353,
        dossun = 354,
        TownTreeDS = 355,
        Ksticketc = 356,
        monte_a = 357,
        MiiStatueM1 = 358,
        ShMiiObj01 = 359,
        ShMiiObj02 = 360,
        ShMiiObj03 = 361,
        Hanabi = 362,
        miiposter = 363,
        dk_miiobj00 = 364,
        light_house = 365,
        r_parasol = 366,
        obakeblock2SFCc = 367,
        obakeblock3SFCc = 368,
        koopaFigure = 369,
        pukupuku = 370,
        v_stand1 = 371,
        v_stand2 = 372,
        leaf_effect = 373,
        karehayama = 374,
        EarthRing = 375,
        SpaceSun = 376,
        BlackHole = 377,
        StarRing = 378,
        M_obj_kanban = 379,
        MiiStatueL1 = 380,
        MiiStatueD1 = 381,
        MiiSphinxY1 = 382,
        MiiSphinxY2 = 383,
        FlagA5 = 384,
        CarB = 385,
        FlagA4 = 386,
        Steam = 387,
        Alarm = 388,
        group_monte_a = 389,
        MiiStatueL2 = 390,
        MiiStatueD2 = 391,
        MiiStatueP1 = 392,
        SentakuDS = 393,
        fks_screen_wii = 394,
        KoopaFigure64 = 395,
        b_teresa = 396,
        MiiStatueDK1 = 397,
        MiiKanban = 398,
        BGteresaSFC = 399,
        kuribo = 401,
        choropu = 402,
        cow = 403,
        pakkun_f = 404,
        WLfirebarGC = 405,
        wanwan = 406,
        poihana = 407,
        DKrockGC = 408,
        sanbo = 409,
        choropu2 = 410,
        TruckWagon = 411,
        heyho = 412,
        Press = 413,
        Press_soko = 414,
        pile = 415,
        choropu_ground = 416,
        WLfireringGC = 417,
        pakkun_dokan = 418,
        begoman_spike = 419,
        FireSnake = 420,
        koopaFirebar = 421,
        Epropeller = 422,
        dc_pillar_c = 423,
        FireSnake_v = 424,
        honeBall = 425,
        puchi_pakkun = 426,
        sanbo_big = 427,
        sanbo_big_2 = 428,
        kinoko_ud = 501,
        kinoko_bend = 502,
        VolcanoRock1 = 503,
        bulldozer_left = 504,
        bulldozer_right = 505,
        kinoko_nm = 506,
        Crane = 507,
        VolcanoPiece = 508,
        FlamePole = 509,
        TwistedWay = 510,
        TownBridgeDSc = 511,
        DKship64 = 512,
        kinoko_kuki = 513,
        DKturibashiGCc = 514,
        FlamePoleEff = 515,
        aurora = 516,
        venice_saku = 517,
        casino_roulette = 518,
        BossField01_OBJ1 = 519,
        dc_pillar = 520,
        dc_sandcone = 521,
        venice_hasi = 522,
        venice_gondola = 523,
        quicksand = 524,
        bblock = 525,
        ami = 526,
        M_obj_jump = 527,
        starGate = 528,
        RM_ring1 = 529,
        FlamePole_v = 530,
        M_obj_s_jump = 531,
        InsekiA = 532,
        InsekiB = 533,
        FlamePole_v_big = 534,
        Mdush = 535,
        HP_pipe = 536,
        DemoCol = 537,
        M_obj_s_jump2 = 538,
        M_obj_jump2 = 539,
        DonkyCannonGC = 601,
        BeltEasy = 602,
        BeltCrossing = 603,
        BeltCurveA = 604,
        BeltCurveB = 605,
        escalator = 606,
        DonkyCannon_wii = 607,
        escalator_group = 608,
        tree_cannon = 609,
        group_enemy_b = 701,
        group_enemy_c = 702,
        taimatsu = 703,
        truckChimSmkW = 704,
        Mstand = 705,
        dkmonitor = 706,
        group_enemy_a = 707,
        FlagB3 = 708,
        spot = 709,
        group_enemy_d = 710,
        FlagB4 = 711,
        group_enemy_e = 712,
        group_monte_L = 713,
        group_enemy_f = 714,
        FallBsA = 715,
        FallBsB = 716,
        FallBsC = 717,
        volsmk = 718,
        ridgemii00 = 719,
        Flash_L = 720,
        Flash_B = 721,
        Flash_W = 722,
        Flash_M = 723,
        Flash_S = 724,
        MiiSignNoko = 725,
        UtsuboDokan = 726,
        Spot64 = 727,
        DemoEf = 728,
        Fall_MH = 729,
        Fall_Y = 730,
        DemoJugemu = 731,
        group_enemy_a_demo = 732,
        group_monte_a_demo = 733,
        volfall = 734,
        MiiStatueM2 = 735,
        RhMiiKanban = 736,
        MiiStatueL3 = 737,
        MiiSignWario = 738,
        MiiStatueBL1 = 739,
        MiiStatueBD1 = 740,
        Kamifubuki = 741,
        Crescent64 = 742,
        MiiSighKino = 743,
        MiiObjD01 = 744,
        MiiObjD02 = 745,
        MiiObjD03 = 746,
        mare_a = 747,
        mare_b = 748,
        EnvKareha = 749,
        EnvFire = 750,
        EnvSnow = 751,
        M_obj_start = 752,
        EnvKarehaUp = 753,
        M_obj_kanban_y = 754,
        DKfalls = 755
    }
}