﻿using System;

namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Фактическая операция.
    /// </summary>
    public sealed class ActualOperation : Operation, IOperationInfo
    {
        /// <summary>
        /// Название операции.
        /// </summary>
        public string Name { get; set; }

        DateTime _date = DateTime.Today;

        /// <summary>
        /// Дата осуществления операции.
        /// </summary>
        public DateTime Date
        {
            get => _date;
            set
            {
                if (value >= PlannedOperation.BeginDate && value <= PlannedOperation.EndDate)
                    _date = value;
            }
        }

        /// <summary>
        /// Статья бюджета.
        /// </summary>
        BudgetItem _budgetItem;

        /// <summary>
        /// Статья бюджета.
        /// </summary>
        public BudgetItem BudgetItem
        {
            get => PlannedOperation == null ? _budgetItem : PlannedOperation.BudgetItem;
            set => _budgetItem = value;
        }

        /// <summary>
        /// Центр финансовой ответственности (ЦФО).
        /// </summary>
        ResponsibilityCenter _responsibilityCenter;

        /// <summary>
        /// Центр финансовой ответственности (ЦФО).
        /// </summary>
        public ResponsibilityCenter ResponsibilityCenter
        {
            get => PlannedOperation == null ? _responsibilityCenter : PlannedOperation.ResponsibilityCenter;
            set => _responsibilityCenter = value;
        }

        /// <summary>
        /// Соответствующая запланированная операция.
        /// </summary>
        public PlannedOperation PlannedOperation { get; set; }

        public ActualOperation ( )
            : base ( )
        {
            BudgetItem = BudgetItem.Default;
            ResponsibilityCenter = ResponsibilityCenter.Default;
        }

        public ActualOperation (string name, DateTime date, BudgetItem budgetItem, ResponsibilityCenter responsibilityCenter, double value, double labourIntensity = 0)
            : base (value, labourIntensity)
        {
            Name = name;
            Date = date;

            BudgetItem = budgetItem;
            ResponsibilityCenter = responsibilityCenter;
        }

        public ActualOperation (string name, DateTime date, PlannedOperation plannedOperation, double value, double labourIntensity = 0)
            : base (value, labourIntensity)
        {
            PlannedOperation = plannedOperation;
            Name = name;
            Date = date;
        }
    }
}
