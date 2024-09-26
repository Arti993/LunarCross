using UnityEngine;

public class VideoAdService : IVideoAdService
{
    private IAssetsProvider _provider;

    public VideoAdService(IAssetsProvider provider)
    {
        _provider = provider;
    }
    
    public void ShowInterstitialAd()
    {
        GameObject InterstitialAdObject = _provider.Instantiate(PrefabsPaths.InterstitialAd);

        InterstitialVideoAd interstitialVideoAd = InterstitialAdObject.GetComponent<InterstitialVideoAd>();

        interstitialVideoAd.Show();
    }
}
