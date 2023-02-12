using System.Collections;
using OrangeBear.EventSystem;

namespace OrangeBear.Core
{
    public class LevelBear : Bear
    {
        #region Public Methods

        public virtual void InitLevel()
        {
            StartCoroutine(Delay());
        }

        #endregion

        #region Private Methods

        private IEnumerator Delay()
        {
            yield return null;
            Roar(GameEvents.InitLevel);
        }

        #endregion
    }
}