using Modules.Minerals.Views;
using UnityEngine;

namespace Modules.Minerals.Configs
{
    [CreateAssetMenu(fileName = "MineralsConfig", menuName = "Config/MineralsConfig")]
    public class MineralsConfig : ScriptableObject
    {
        [SerializeField] private MineralsUIView _mineralsUIViewPrefab;

        public MineralsUIView MineralsUIViewPrefab => _mineralsUIViewPrefab;

    }
}