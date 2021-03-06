﻿using System.Linq;
using System;
using System.Collections.ObjectModel;

namespace PlanFactAnalysis.ViewModel
{ 
    [Magic]
    internal sealed class PlannedOperationComparisonViewModel: PropertyChangedBase
    {
        readonly PlannedOperationViewModel _plannedOperation;
        readonly ObservableCollection<ActualOperationViewModel> _actualOperations;
        
        public PlannedOperationComparisonViewModel (PlannedOperationViewModel plannedOperation,
            ObservableCollection<ActualOperationViewModel> actualOperation)
        {
            _plannedOperation = plannedOperation;
            _actualOperations = actualOperation;
        }

        public ObservableCollection<ActualOperationViewModel> ActualOperations => _actualOperations;

        public PlannedOperationViewModel PlannedOperation => _plannedOperation;
        public BudgetItemViewModel BudgetItem => _plannedOperation.BudgetItem;
        public ResponsibilityCenterViewModel ResponsibilityCenter => _plannedOperation.ResponsibilityCenter;

        public double PlannedValue => _plannedOperation.Value;
        public double ActualValue => _actualOperations.Sum (op => op.Value);

        public double Deviation => ActualValue - PlannedValue;

        public DateTime BeginDate => _plannedOperation.BeginDate;
        public DateTime EndDate => _plannedOperation.EndDate;
    }
}