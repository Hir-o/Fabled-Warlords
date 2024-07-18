using TbsFramework.Units;
using UnityEngine;

public class StormElemental : LUnit, IMage
{
    private ThunderStrikeSkill _thunderStrikeSkill;

    #region Properties

    public ThunderStrikeSkill ThunderStrikeSkill => _thunderStrikeSkill;

    #endregion

    public override void InitProperties()
    {
        base.InitProperties();
        _thunderStrikeSkill = GetComponent<ThunderStrikeSkill>();
    }

    protected override int Defend(Unit other, int damage)
    {
        float newDamage = damage;
        if (other is LUnit lUnit && lUnit.UnitClassCounter != null)
            newDamage *= lUnit.UnitClassCounter.VSMageFactor;

        return base.Defend(other, Mathf.RoundToInt(newDamage));
    }

    protected override void ApplyDebuffsToEnemy(LUnit enemyUnit, bool isEnemyTurn = false)
    {
        if (ThunderStrikeSkill == null) return;
        float randomChance = UnityEngine.Random.Range(0f, 100f);
        if (randomChance < ThunderStrikeSkill.ProcChance) return;
        enemyUnit.StatusEffectsController.ApplyStatusEffect<Stun>(ThunderStrikeSkill.DurationInTurns);
    }
}