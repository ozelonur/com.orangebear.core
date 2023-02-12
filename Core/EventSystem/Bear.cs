using UnityEngine;

namespace OrangeBear.EventSystem
{
    public abstract class Bear : MonoBehaviour
    {
        #region Properties

        public BearManager Manager => BearManager.Instance;

        #endregion

        #region MonoBehaviour Methods

        protected virtual void OnEnable()
        {
            CheckRoarings(true);
        }

        protected virtual void OnDisable()
        {
            CheckRoarings(false);
        }

        #endregion

        #region Protected Methods

        protected virtual void CheckRoarings(bool status)
        {
        }

        protected virtual void Roar(string roarName, params object[] arguments)
        {
            Manager.Roar(roarName, arguments);
        }

        protected virtual void Register(string roarName, Roaring roaring)
        {
            Manager.Register(roarName, roaring);
        }

        protected virtual void Unregister(string roarName, Roaring roaring)
        {
            Manager.Unregister(roarName, roaring);
        }

        #endregion
    }
}