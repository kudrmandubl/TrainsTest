using UnityEngine;
using TMPro;

namespace Modules.Minerals.Views
{
    public class MineralsUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _valueText;
        
        public void UpdateText(double value)
        {
            _valueText.text = $"{value:0.00}";
        }
    }
}