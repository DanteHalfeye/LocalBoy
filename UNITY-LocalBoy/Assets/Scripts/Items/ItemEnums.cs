public enum Stat
{
    SetMaxHealth,
    SetCurrentHealth,
    ModifyMaxHealth,
    ModifyCurrentHealth,
    ModifyAttackSpeed,
    SetAttackSpeed,
    AddAmmo,
    SetAmmo,
    ModifySpeed,
    SetSpeed
}

public enum EffectTrigger
{
    Always,
    OnEnemyKill,
    OnRoomEnter,
    OnFloorEnter
}

public enum Condition
{
    None,
    Health,
    Halth_percent,
    Ammo,
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
    Floor
}

public enum ItemTypeClass
{
    Stats,
    Orbital
}
