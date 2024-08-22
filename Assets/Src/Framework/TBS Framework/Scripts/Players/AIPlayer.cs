﻿using System.Collections;
using System.Linq;
using TbsFramework.Grid;
using TbsFramework.Grid.GridStates;
using TbsFramework.Players.AI;
using TbsFramework.Players.AI.Actions;
using UnityEngine;

namespace TbsFramework.Players
{
    public class AIPlayer : Player
    {
        public bool DebugMode;

        public override void Initialize(CellGrid cellGrid)
        {
            cellGrid.GameEnded += OnGameEnded;
        }

        public override void Play(CellGrid cellGrid)
        {
            cellGrid.cellGridState = new CellGridStateAITurn(cellGrid, this);
            StartCoroutine(PlayCoroutine(cellGrid));
        }

        private void OnGameEnded(object sender, System.EventArgs e)
        {
            StopAllCoroutines();
        }

        private IEnumerator PlayCoroutine(CellGrid cellGrid)
        {
            var UnitsOrdered = GetComponent<UnitSelection>()
                .SelectNext(() => cellGrid.GetCurrentPlayerUnits(), cellGrid);

            foreach (var unit in UnitsOrdered)
            {
                //if the unit is dead then skip it so the ai doesn't get stuck
                if (unit.HitPoints <= 0)
                    continue;

                if (unit.gameObject.TryGetComponent(out StationaryGroupSkill stationaryGroupSkill))
                    if (stationaryGroupSkill.IsStationary)
                        continue;

                if (DebugMode)
                {
                    unit.MarkAsSelected();
                    Debug.Log(string.Format("Current unit: {0}, press N to continue", unit.name));
                    while (!Input.GetKeyDown(KeyCode.N))
                    {
                        yield return 0;
                    }
                }

                float unitAp = 0;
                float lastUnitAp = 0;
                var AIActions = unit.GetComponentsInChildren<AIAction>();
                foreach (var aiAction in AIActions)
                {
                    TryToAttackAgain:
                    yield return 0;
                    unitAp = unit.ActionPoints;
                    aiAction.InitializeAction(this, unit, cellGrid);
                    var shouldExecuteAction = aiAction.ShouldExecute(this, unit, cellGrid);
                    if (DebugMode)
                    {
                        aiAction.Precalculate(this, unit, cellGrid);
                        aiAction.ShowDebugInfo(this, unit, cellGrid);
                        Debug.Log(string.Format("Current action: {0}, press A to execute",
                            aiAction.GetType().ToString()));
                        while (!Input.GetKeyDown(KeyCode.A))
                        {
                            yield return 0;
                        }

                        yield return 0;
                    }

                    if (shouldExecuteAction)
                    {
                        if (!DebugMode)
                        {
                            yield return 0;
                            aiAction.Precalculate(this, unit, cellGrid);
                        }

                        yield return (aiAction.Execute(this, unit, cellGrid));
                    }

                    aiAction.CleanUp(this, unit, cellGrid);
                    if (unit != null && unit.HitPoints > 0 && aiAction is AttackAIAction && unit.ActionPoints > 0 &&
                        lastUnitAp != unitAp)
                    {
                        lastUnitAp = unitAp;
                        goto TryToAttackAgain;
                    }
                }

                unit.MarkAsFriendly();
            }

            cellGrid.EndTurn();
            yield return 0;
        }

        private void Reset()
        {
            if (GetComponent<UnitSelection>() == null)
            {
                gameObject.AddComponent<MovementFreedomUnitSelection>();
            }
        }
    }

    public class DebugInfo
    {
        public string Metadata { get; set; }
        public Color Color { get; set; }

        public DebugInfo(string metadata, Color color)
        {
            Color = color;
            Metadata = metadata;
        }
    }
}