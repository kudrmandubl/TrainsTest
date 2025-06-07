using System;

namespace Modules.Common.Interfaces
{
    /// <summary>
    /// Класса для взаимодействия с циклом жизни MonoBehaviour
    /// </summary>
    public interface IMonoBehaviourCycle
    {
        /// <summary>
        /// Подписаться на апдейт
        /// </summary>
        /// <param name="action">Вызываемый экшен</param>
        void SubscribeToUpdate(Action<float> action);

        /// <summary>
        /// Отписаться от апдейта
        /// </summary>
        /// <param name="action">Вызываемый экшен</param>
        void UnsubscribeFromUpdate(Action<float> action);

        /// <summary>
        /// Подписаться на изменение фокуса
        /// </summary>
        /// <param name="action">Вызываемый экшен</param>
        void SubscribeToApplicationFocus(Action<bool> action);

        /// <summary>
        /// Отписаться от изменения фокуса
        /// </summary>
        /// <param name="action">Вызываемый экшен</param>
        void UnsubscribeFromApplicationFocus(Action<bool> action);
    }
}