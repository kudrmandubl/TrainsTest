using Modules.Common.DI;
using Modules.Trains.Interfaces;
using Zenject;

namespace Modules.Trains.DI
{
    public class TrainFactory : SimpleFactory<ITrain>
    {
        public TrainFactory(DiContainer container) : base(container) { }
    }
}
