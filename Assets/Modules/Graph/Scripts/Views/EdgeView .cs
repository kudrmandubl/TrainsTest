using Modules.Common;
using UnityEngine;

namespace Modules.Graph.Views
{
    public class EdgeView : CashedMonoBehaviour
    {
        public const float ModelScale = 0.1f;

        // колдовство для сохранения разделения данных и отображения
        // и при этом выполнения условий задания про возможность через инспектор поменять значения 
        [SerializeField] private float _distance;
        private MeshRenderer _meshRenderer;

        public ReactiveProperty<float> Distance = new ReactiveProperty<float>();

        public void SetShape(float distance)
        {
            Transform.localScale = new Vector3(1f, 1f, distance);
        }

        public void UpdateDistance(float distance)
        {
            _distance = distance;
            Distance.Value = distance;
        }

        public void UpdateSelectedMaterial(Material material)
        {
            if (!_meshRenderer)
            {
                _meshRenderer = GetComponentInChildren<MeshRenderer>();
            }

            _meshRenderer.material = material;
        }

        // При изменениях в инспекторе (только в редакторе)
        private void OnValidate()
        {
            // Обновляем значение в ReactiveProperty, если оно отличается
            if (Distance.Value != _distance)
            {
                Distance.Value = _distance;
            }
        }
    }
}