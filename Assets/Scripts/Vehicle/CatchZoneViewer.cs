using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicle
{
    public class CatchZoneViewer : MonoBehaviour
    {
        private const float DelayToDisable = 9;

        [SerializeField] private GameObject[] _catchZonesViews;
        [SerializeField] private GameObject[] _dangerZonesViews;

        private bool _isShowing;

        public void ShowCatchZones()
        {
            Show(_catchZonesViews);
        }

        public void ShowDangerZones()
        {
            Show(_dangerZonesViews);
        }

        public void StopShowCatchZones()
        {
            _ = StartCoroutine(StopShowAfterDelay(_catchZonesViews));
        }

        public void StopShowDangerZones()
        {
            _ = StartCoroutine(StopShowAfterDelay(_dangerZonesViews));
        }

        private void Show(IEnumerable<GameObject> zones)
        {
            _isShowing = true;

            foreach (var zone in zones)
            {
                zone.SetActive(true);
            }
        }

        private IEnumerator StopShowAfterDelay(IEnumerable<GameObject> zones)
        {
            if (_isShowing == false)
                throw new InvalidOperationException();

            yield return new WaitForSeconds(DelayToDisable);

            foreach (var zone in zones)
            {
                zone.SetActive(false);
            }

            _isShowing = false;
        }
    }
}
