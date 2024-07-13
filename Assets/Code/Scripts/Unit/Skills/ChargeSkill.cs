using NaughtyAttributes;
using TbsFramework.Units;
using UnityEngine;

public class ChargeSkill : MonoBehaviour, IAttackSkill
{
    [SerializeField] private string _skillName = "Charge";

    [SerializeField] private string _skillDescription =
        "Deals extra damage when attacking after moving at least three tiles away from its starting position.";

    [BoxGroup("Attack Amount")] [SerializeField] [Range(1, 5)]
    private int _attackPowerFactor = 2;

    [SerializeField] private int _totalTilesDistanceAmount = 3;

    private bool _applyCharge;

    private LUnit _lUnit;

    #region Properties

    public string SkillName                     => _skillName;
    public string SkillDescription              => _skillDescription;
    public bool   CanBeActivatedDuringEnemyTurn { get; set; } = true;

    #endregion

    private void Awake() { _lUnit = GetComponent<LUnit>(); }

    private void OnEnable()
    {
        _lUnit.UnitMoved          += OnMoveToAnotherCell;
        _lUnit.OnTurnEndUnitReset += ResetTileDistance;
    }

    private void OnDisable()
    {
        _lUnit.UnitMoved          -= OnMoveToAnotherCell;
        _lUnit.OnTurnEndUnitReset -= ResetTileDistance;
    }

    public int GetDamageFactor()
    {
        if (_applyCharge)
            return _attackPowerFactor;
        return 0;
    }

    private void OnMoveToAnotherCell(object obj, MovementEventArgs movementEventArgs) =>
        _applyCharge = movementEventArgs.Path.Count >= _totalTilesDistanceAmount;

    private void ResetTileDistance() => _applyCharge = false;
}