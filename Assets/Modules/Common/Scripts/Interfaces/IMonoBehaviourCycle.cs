using System;

namespace Modules.Common.Interfaces
{
    /// <summary>
    /// ������ ��� �������������� � ������ ����� MonoBehaviour
    /// </summary>
    public interface IMonoBehaviourCycle
    {
        /// <summary>
        /// ����������� �� ������
        /// </summary>
        /// <param name="action">���������� �����</param>
        void SubscribeToUpdate(Action<float> action);

        /// <summary>
        /// ���������� �� �������
        /// </summary>
        /// <param name="action">���������� �����</param>
        void UnsubscribeFromUpdate(Action<float> action);

        /// <summary>
        /// ����������� �� ��������� ������
        /// </summary>
        /// <param name="action">���������� �����</param>
        void SubscribeToApplicationFocus(Action<bool> action);

        /// <summary>
        /// ���������� �� ��������� ������
        /// </summary>
        /// <param name="action">���������� �����</param>
        void UnsubscribeFromApplicationFocus(Action<bool> action);
    }
}