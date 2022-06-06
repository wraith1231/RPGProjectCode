using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
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
        Sword = 0,
        Axe = 1,
        Dagger = 2,
        Mace = 3,
        Spear = 4,
        Bow = 5,
        Shield = 6,
        TwoHandShield = 7,
        Gauntlet = 8,
        Unknown = 9,
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
