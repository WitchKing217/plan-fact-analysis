﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal class RegistryViewModel<T, M> : SubViewModelBase<MainViewModel>
        where T : ViewModelBase<M>
        where M : class, new()
    {
        readonly protected IList<M> _modelItems;
        readonly protected ObservableCollection<T> _viewModelItems = new ObservableCollection<T> ( );

        protected M _newItemModel;
        public T NewItem { get; private set; }

        readonly RelayCommand _addItemCommand;
        public RelayCommand AddItemCommand => _addItemCommand;

        readonly RelayCommand _removeItemCommand;
        public RelayCommand RemoveItemCommand => _removeItemCommand;

        public RegistryViewModel (MainViewModel context, IList<M> modelItems)
            : base (context)
        {
            _modelItems = modelItems;

            foreach (M item in _modelItems)
                _viewModelItems.Add ((T)Activator.CreateInstance (typeof (T), item, _context));

            _newItemModel = new M ( );
            NewItem = (T)Activator.CreateInstance (typeof (T), _newItemModel, _context);

            _addItemCommand = new RelayCommand (AddItem, CanAddItem);
            _removeItemCommand = new RelayCommand (RemoveItem, CanRemoveItem);
        }

        protected virtual void AddItem (object obj)
        {
            _modelItems.Add (_newItemModel);
            _viewModelItems.Add (NewItem);

            _newItemModel = new M ( );
            NewItem = (T)Activator.CreateInstance (typeof (T), _newItemModel, _context);
        }

        protected virtual void RemoveItem (object obj)
        {
            T currentItem = ItemsCollectionView.CurrentItem as T;

            _modelItems.Remove (GetModelFromViewModel (currentItem));
            _viewModelItems.Remove (currentItem);
        }

        protected virtual bool CanAddItem (object obj)
        {
            return true;
        }

        protected virtual bool CanRemoveItem (object obj)
        {
            return true;
        }

        public void AddModel (M model)
        {
            if (!_modelItems.Contains(model))
            {
                _modelItems.Add (model);
                _viewModelItems.Add ((T)Activator.CreateInstance (typeof (T), model, _context));
            }
        }

        public virtual void Clear ( )
        {
            foreach (M item in _modelItems)
            {
                _modelItems.Remove (item);
                _viewModelItems.Remove (GetViewModelFromModel (item));
            }
        }

        public virtual void RefreshViewModelList ( )
        {
            _viewModelItems.Clear ( );

            foreach (var item in _modelItems)
            {
                if (!_viewModelItems.Contains (GetViewModelFromModel (item)))
                    _viewModelItems.Add ((T)Activator.CreateInstance (typeof (T), item, _context));
            }
        }

        public T GetViewModelFromModel (M model) => _viewModelItems.FirstOrDefault (item => item.Equals (model));
        public M GetModelFromViewModel (T viewModel) => _modelItems.FirstOrDefault (item => viewModel.Equals (item));

        public ObservableCollection<T> Items => _viewModelItems;
        public ICollectionView ItemsCollectionView => CollectionViewSource.GetDefaultView (_viewModelItems);
    }
}
