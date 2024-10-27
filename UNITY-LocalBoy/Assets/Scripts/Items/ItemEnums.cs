public enum Stat
{
    SetMaxHealth,
    SetCurrentHealth,
    ModifyMaxHealth,
    ModifyCurrentHealth,
    ModifyAttackSpeed,
    SetAttackSpeed,
    ModifySpeed,
    SetSpeed,
    ModifyMoney,
    ModifyDashSpeed
}

public enum EffectTrigger
{
    Once,
    Always,
    OnEnemyKill,
    OnRoomEnter,
}

public enum Condition
{
    None,
    Health,
    Halth_percent,
    Speed,
    AttackSpeed
}

public enum ConditionNumberType
{
    EQUAL,
    GREATER_THAN,
    GREATER_THAN_OR_EQUAL,
    LESS_THAN,
    LESS_THAN_OR_EQUAL,
    MULTIPLE
}

public enum DurationType
{
    Infinite,
    Time,
    Rooms,
}

public enum ItemTypeClass
{
    Stats,
    Orbital
}

public enum ItemRarity
{
    Normal = 10,
    Epic = 15,
    Legendary = 20
}