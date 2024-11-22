using Data;
using Infrastructure.Services.AssetsProvider;
using UnityEngine;

namespace Infrastructure.Services.InterstitialAd
{
    public class InterstitialAdService : IInterstitionalAdService
    {
        private IAssetsProvider _provider;

        public InterstitialAdService(IAssetsProvider provider)
        {
            _provider = provider;
        }

        public void ShowAd()
        {
            GameObject InterstitialAdObject = _provider.Instantiate(PrefabsPaths.InterstitialAd);

            InterstitialAd interstitialAd = InterstitialAdObject.GetComponent<InterstitialAd>();

            interstitialAd.Show();
        }
    }
}
