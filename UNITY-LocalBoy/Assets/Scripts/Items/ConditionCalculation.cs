using System;
using System.Collections.Generic;
using UnityEngine;


public static class ConditionCalculation
{
    public static bool EvaluateConditionType(Condition condition, ConditionNumberType conditionNumber, float conditionValue, PlayerActor actor)
    {
        bool conditionMet = false;

        if(condition == Condition.None)
        {
            conditionMet = true;
        }
        else if (condition == Condition.Health)
        {
            conditionMet = EvaluateConditionNumber(conditionNumber, actor.CurrentHealth, conditionValue);
        }
        else if (condition == Condition.Halth_percent)
        {
            conditionMet = EvaluateConditionNumber(conditionNumber, actor.HpPercent, conditionValue);
        }
        else if (condition == Condition.Speed)
        {
            conditionMet = EvaluateConditionNumber(conditionNumber, actor.GetMovementSpeed, conditionValue);
        }
        else if (condition == Condition.AttackSpeed)
        {
            conditionMet = EvaluateConditionNumber(conditionNumber, actor.GetAttackSpeed, conditionValue);
        }

        return conditionMet;
    }

    private static bool EvaluateConditionNumber(ConditionNumberType conditionNumberType, float statValue, float conditionValue)
    {
        switch (conditionNumberType)
        {
            case ConditionNumberType.EQUAL:
                return Mathf.Approximately(statValue, conditionValue);

            case ConditionNumberType.LESS_THAN:
                return statValue < conditionValue;

            case ConditionNumberType.GREATER_THAN:
                return statValue > conditionValue;

            case ConditionNumberType.LESS_THAN_OR_EQUAL:
                return statValue <= conditionValue;

            case ConditionNumberType.GREATER_THAN_OR_EQUAL:
                return statValue >= conditionValue;

            case ConditionNumberType.MULTIPLE:
                return conditionValue > 0 && Mathf.FloorToInt(statValue / conditionValue) > 0;

            default:
                return false;
        }
    }
}