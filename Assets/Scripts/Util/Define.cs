using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    #region constant

    public const int GACHA_POP_NUM = 4;
    public const int PITY_SYSTEM = 80;

    public const int TOTAL_FIGURE_NUM = 15;

    public const int DEFAULT_GACHA_COST = 10;

    public const int SELL_NORMAL_TO_EARN = 2;
    public const int SELL_RARE_TO_EARN = 4;
    public const int SELL_ULTRA_TO_EARN = 8;
    public const int SELL_HIDDEN_TO_EARN = 16;

    public const int NORMAL_STAR = 1;
    public const int RARE_STAR = 2;
    public const int ULTRA_STAR = 3;

    public const int FIGURE_POOL_SIZE = 4;
    public const int POOLABLE_POOL_SIZE = 30;

    public const int NORMAL_CHANCE = 76;
    public const int RARE_CHANCE = 20;
    public const int ULTRA_CHANCE = 4;

    public const float STAR_MOVE_SPEED = 0.5f;
    public const float STAR_MAX_SIZE = 2.0f;
    public const float STAR_MIN_SIZE = 0.5f;

    public const float FADE_IN_OUT_TIME = 2.0f;

    public const float DEFAULT_DIRECTIONAL_LIGHT_INTENSITY = 3.0f;

    public const float INTERACTION_DISTANCE = 6.0f;

    public const int MINIGAME_TIMER = 5;
    public const int MINIGAME_LIFE = 4;
    public const int MINIGAME_SUCCESS_REWARD = 1;

    #endregion

    #region enum

    public enum BGM
    {
        None,
        Title,
        Main,
        Gacha,
        Loading,
        Room,

        //Minigame
        Minigame_TitleLoop,
        GuraPat,
        AmeWario,
    }

    public enum SFX
    {
        None,
        Click,
        Gacha,
        LeverSpin,
        GachaDrop,
        BallSpin,
        Open,
        NormalPop,
        RarePop,
        UltraPop,
        ClearFigure,
        RarityStar,
        SellEarn,

        DanceButtonClick,

        //Minigame
        Minigame_TitleDrop,
        Minigame_TitleStart,
        Minigame_OP,
        Minigame_Good,
        Minigame_Bad,
        Minigame_GameOver,

        GuraPat_Pat,

        SuiseiImpostor_Kill,

        Ame_Groundpound,
    }

    public enum Scene
    {
        DownLoad,
        Title,
        Main,
        Loading,
        Room,
        Minigame,
    }

    public enum DataType
    {
        FigureItemData,
    }

    public enum FigurePrefabName
    {
        HoushouMarine,
        InugamiKorone,
        MomosuzuNene,
        Nekko,
        OmaruPolka,
        SakuraMiko,
        ShubaDuck,
        SmolAme,
        SSRB_1,
        SSRB_2,
        SSRB_3,
        Takodachi,
        UsadaPekora,
        YozoraMel,
        YukihanaLamy
    }

    #endregion

}
