using System;
using System.Collections.Generic;
using Modules.Graph.Interfaces;
using Modules.Trains.Views;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Modules.Graph.Implementations
{
    public class RouteShower : IRouteShower
    {
        private static TrainView _selectedTrainView;
        private IEnumerable<IEdge> _lastShownRoute;

        private static Action<TrainView> SelectRoute, UnselectRoute;

        public RouteShower()
        {
#if UNITY_EDITOR
            // Подписываемся на событие изменения выбора
            Selection.selectionChanged += OnSelectionChanged;
#endif

            SelectRoute += SelectTrainView;
            UnselectRoute += UnselectTrainView;
        }

        public void ShowRoute(IEnumerable<IEdge> route)
        {
            HideRoute(_lastShownRoute);
            SetRouteSelected(route, true);
            _lastShownRoute = route;
        }

        public void HideRoute(IEnumerable<IEdge> route)
        {
            SetRouteSelected(_lastShownRoute, false);
        }

        private void SelectTrainView(TrainView trainView)
        {
            ShowRoute(trainView.Route);
            trainView.OnRouteChange += ShowRoute;
        }

        private void UnselectTrainView(TrainView trainView)
        {
            HideRoute(trainView.Route);
            trainView.OnRouteChange -= ShowRoute;
        }

        private void SetRouteSelected(IEnumerable<IEdge> route, bool value)
        {
            if (route != null)
            {
                foreach (var edge in route)
                {
                    edge.IsSelected.Value = value;
                }
            }
        }

#if UNITY_EDITOR
        private static void OnSelectionChanged()
        {
            // Получаем текущий выбранный объект
            GameObject selectedObject = Selection.activeGameObject;
            TrainView trainView = selectedObject?.GetComponentInParent<TrainView>();
            if (selectedObject != null && trainView)
            {
                _selectedTrainView = trainView;
                SelectRoute?.Invoke(trainView);
            }
            else if(_selectedTrainView)
            { 
                UnselectRoute?.Invoke(_selectedTrainView);
                _selectedTrainView = null;
            }
        }
#endif
    }
}