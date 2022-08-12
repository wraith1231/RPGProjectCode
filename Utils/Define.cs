using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum LastOutfitChange
    {
        HeadCoveringBase,
        HeadCoveringMask,
        HeadCoveringNoHair,
        AllGenderHair,
        AllGenderExtra,
        Head,
        HeadGear,
        Facial,
    }

    public enum VillageSubButtonType
    {
        TalkPerson = 0 ,
        TalkMaster = 1,
        TalkMayer = 2,
        TalkGuard = 3,
        TalkGuild = 4,
        TalkMerchant = 5,
        OpenChangeOutfit = 6,
        OpenBlacksmith = 7,
        OpenEnchant = 8,
        OpenQuest = 9,
        CheckQuest = 10,
        OpenRest = 11,
        Unknown,
    }

    public enum InteractionEvent
    {
        End = 0,
        Context = 1,
        Question = 2,
        Reward = 3,
        Quest = 4,
        Outfit = 5,
        Blacksmith = 6,
        Enchant = 7,
        Rest = 8,
        Unknown = 9,
    }

    public enum VillageCondition
    {
        Idle,
        Battle,
        Recontract,
        Destroyed,
        Unknown,
    }

    public enum AreaStatus
    {
        Idle,
        Move,
        Battle,
        Unknown,
    }

    public enum GroupType
    {
        Mercenary,
        Merchant,
        Monster,
        Unknown,
    }

    public enum QuestType
    {
        AttackCamp,
        DefenseVillage,
        Raid,
        Hunt,
        Unknown
    }

    public enum Facilities
    {
        Gate,
        Market,
        Workshop,
        Square,
        Guild,
        Mayor,
        Inn,
        Unknown
    }

    public enum CharacterRelationship
    {
        Baddess,
        Bad,
        Normal,
        Good,
        Best,
    }

    public enum CharacterType
    {
        Human,
        Animal,
        Monster,
        Unknown
    }

    public enum HumanOutfitAllGender
    {
        HeadCoveringBase,
        HeadCoveringMask,
        HeadCoveringNoHair,
        AllGenderHair,
        AllGenderHeadAttachment,
        AllGenderBackAttachment,
        AllGenderShoulderRight,
        AllGenderShoulderLeft,
        AllGenderElbowRight,
        AllGenderElbowLeft,
        AllGenderHips,
        AllGenderKneeRight,
        AllGenderKneeLeft,
        AllGenderExtra,
        Unknown,
    }

    public enum HumanOutfitOneGender
    {
        Head,
        HeadGear,
        Eyebrows,
        Facial,
        Torso,
        ArmUpperRight,
        ArmUpperLeft,
        ArmLowerRight,
        ArmLowerLeft,
        HandRight,
        HandLeft,
        Hips,
        LegRight,
        LegLeft,
        Unknown,
    }

    public enum HumanGender
    {
        Female,
        Male,
        Unknown,
    }

    public enum NPCPersonality
    {
        Normal,
        Offensive,
        Defensive,
        Unknown,
    }

    public enum BattleObjective
    {
        Defense,
        Deathmatch,
        Occupation,
        Runaway,
        Unknown,
    }

    public enum WeaponCategory
    {
        OneHand = 0,
        TwoHand = 1,
        Shield = 2,
        Unknown = 3,
    }

    public enum WeaponType
    {
        Sword,
        Axe,
        Dagger,
        Mace,
        Spear,
        Bow,
        Gauntlet,
        Shield,
        Unknown,
    }

    public enum WorldObject
    {
        Unknown,
        Player,
        Monster,
    }

    public enum HeroState
    {
        Die = 0,
        Idle = 1,
        Strafe = 2,
        Running = 3,
        Attack = 4,
        Block = 5,
        Rolling = 6,
        Damaged = 7,
        Unknown = 8,
    }

    public enum SceneType
    {
        TitleScene,
        TestScene,
        AreaScene
    }
    
    public enum UIEvent
    {
        Click,
        DragBegin,
        DragEnd,
    }

    public enum MouseEvent
    {
        Press,
        PointerDown,
        Click,
        PointerUp,
    }

    public enum CameraMode
    {
        QuaterView,
        TopDown,
    }

    public enum SoundMode
    {
        BGM,
        Effect,
        MaxCount,
    }
}
