public class Cyclop : LUnit, IMonster
{
    private OverpowerSkill _overpowerSkill;
    
    #region Properties

    public OverpowerSkill OverpowerSkill => _overpowerSkill;

    #endregion
    
    public override void InitProperties()
    {
        base.InitProperties();
        _overpowerSkill = GetComponent<OverpowerSkill>();
    }
    
    protected override void ApplyDebuffsToEnemy(LUnit enemyUnit, bool isEnemyTurn = false)
    {
        if (OverpowerSkill == null) return;
        enemyUnit.StatusEffectsController.ApplyStatusEffect<Stun>(OverpowerSkill.DurationInTurns);
    }
}
