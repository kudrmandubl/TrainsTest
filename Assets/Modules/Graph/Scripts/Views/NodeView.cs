using Modules.Common;
using UnityEngine;

namespace Modules.Graph.Views
{
    public class NodeView : MonoBehaviour
    {
        // колдовство для сохранения разделения данных и отображения
        // и при этом выполнения условий задания про возможность через инспектор поменять значения 
        [SerializeField] private float _multiplier;

        public ReactiveProperty<float> Multiplier = new ReactiveProperty<float>();

        public void UpdateMultiplier(float multiplier)
        {
            _multiplier = multiplier;
            Multiplier.Value = multiplier;
        }

        // При изменениях в инспекторе (только в редакторе)
        private void OnValidate()
        {
            // Обновляем значение в ReactiveProperty, если оно отличается
            if (Multiplier.Value != _multiplier)
            {
                Multiplier.Value = _multiplier;
            }
        }
    }
}