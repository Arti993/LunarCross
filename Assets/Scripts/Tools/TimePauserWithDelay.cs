using System.Collections;
using UnityEngine;

namespace Tools
{
    public class TimePauserWithDelay
    {
        private const float StopTimeDelay = 0.15f;

        public IEnumerator Pause()
        {
            yield return new WaitForSeconds(StopTimeDelay);

            Time.timeScale = 0f;
        }
    }
}
