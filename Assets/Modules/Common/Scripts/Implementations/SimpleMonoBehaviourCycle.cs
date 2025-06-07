using System;
using Modules.Common.Interfaces;
using UnityEngine;

namespace Modules.Common.Implementations
{
    /// <summary>
    /// Вариант реализации класса для взаимодействия с циклом жизни MonoBehaviour
    /// </summary>
    public class SimpleMonoBehaviourCycle : MonoBehaviour, IMonoBehaviourCycle
    {
        private Action<float> OnUpdate;
        private Action<bool> OnApplicationFocusChange;

        ///  <inheritdoc />
        public void SubscribeToUpdate(Action<float> action)
        {
            OnUpdate += action;
        }

        ///  <inheritdoc />
        public void UnsubscribeFromUpdate(Action<float> action)
        {
            OnUpdate -= action;
        }

        ///  <inheritdoc />
        public void SubscribeToApplicationFocus(Action<bool> action)
        {
            OnApplicationFocusChange += action;
        }

        ///  <inheritdoc />
        public void UnsubscribeFromApplicationFocus(Action<bool> action)
        {
            OnApplicationFocusChange -= action;
        }

        /// <summary>
        /// Каждый кадр
        /// </summary>
        private void Update()
        {
            OnUpdate?.Invoke(Time.deltaTime);
        }

        /// <summary>
        /// Вызывается при смене фокуса приложения
        /// </summary>
        /// <param name="focus">Значение фокуса</param>
        private void OnApplicationFocus(bool focus)
        {
            OnApplicationFocusChange?.Invoke(focus);
        }
    }
}
