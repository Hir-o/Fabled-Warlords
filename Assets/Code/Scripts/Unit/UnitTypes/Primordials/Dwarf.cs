using TbsFramework.Units;
using UnityEngine;

public class Dwarf : LUnit, ISwordInfantry
{
    protected override int Defend(Unit other, int damage)
    {
        Agressor = other;
        float defenceFactor = CalculateDefense();
        float defenceAmount = damage * defenceFactor;
        float newDamage = damage - defenceAmount;

        if (other is LUnit lUnit && lUnit.UnitClassCounter != null)
            newDamage *= lUnit.UnitClassCounter.VSSwordInfantryCounter;

        if (StatusEffectsController.IsStatusApplied<Weaken>())
        {
            float weakenedFactor = StatusEffectsController.GetStatus<Weaken>().weakenFactor;
            newDamage = Mathf.RoundToInt(newDamage * weakenedFactor);
        }

        bool isRetalationResilenceActive = TryUseRetaliationResilence();
        if (isRetalationResilenceActive) newDamage /= 2;
        if (newDamage <= 0) newDamage = 1;
        TempDamageReceived = Mathf.RoundToInt(newDamage);
        return TempDamageReceived;
    }

    protected override float CalculateDefense()
    {
        float defenceAmount = 0;
        for (int i = 0; i < DefendSkillArray.Length; i++)
        {
            if (DefendSkillArray[i] is StoneWillSkill)
                defenceAmount = DefendSkillArray[i].GetDefenceAmount();
        }

        return defenceAmount;
    }
}