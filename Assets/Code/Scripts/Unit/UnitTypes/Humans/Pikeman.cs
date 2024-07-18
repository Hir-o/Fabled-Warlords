using TbsFramework.Units;
using UnityEngine;

public class Pikeman : LUnit, ISpearInfantry
{
    protected override int Defend(Unit other, int damage)
    {
        float newDamage = damage;
        if (other is ISwordInfantry) newDamage *= 1.5f;
        else if (other is IRanged) newDamage *= 1.5f;
        
        return base.Defend(other, Mathf.RoundToInt(newDamage));
    }
    
    /*protected override int CalculateDamage(AttackAction baseVal, Unit unitToAttack)
    {
        float totalFactorDamage = 0;
        int   baseDamage        = baseVal.Damage;
        
        if (StatusEffectsController.IsStatusApplied<Weaken>())
        {
            float weakenedFactor = StatusEffectsController.GetStatus<Weaken>().weakenFactor;
            baseDamage = Mathf.RoundToInt(baseDamage * weakenedFactor);
        }
        
        for (int i = 0; i < AttackSkillArray.Length; i++)
        {
            if (!AttackSkillArray[i].CanBeActivatedDuringEnemyTurn) continue;
            if (unitToAttack is IMonster && AttackSkillArray[i] is AntiLargeSkill antiLargeSkill)
            {
                antiLargeSkill.AggressorUnit =  unitToAttack as LUnit;
                totalFactorDamage            += AttackSkillArray[i].GetDamageFactor();
            }
        }

        int factoredDamage = totalFactorDamage > 0 ? baseDamage * (int) totalFactorDamage : baseDamage;
        return factoredDamage;
    }*/
}
