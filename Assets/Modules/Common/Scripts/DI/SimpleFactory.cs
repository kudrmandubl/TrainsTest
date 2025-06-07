using Zenject;

namespace Modules.Common.DI
{
    public class SimpleFactory<T> : IFactory<T>
    {
        private DiContainer _container;

        public SimpleFactory(DiContainer container)
        {
            _container = container;
        }

        public T Create()
        {
            return _container.Resolve<T>();
        }
    }
}
