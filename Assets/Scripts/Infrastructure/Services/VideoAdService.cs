using UnityEngine;

public class VideoAdService : IVideoAdService
{
    private const string InterstitialAdPrefabPath = "Prefabs/InterstitialAd";
    
    private IAssetsProvider _provider;

    public VideoAdService(IAssetsProvider provider)
    {
        _provider = provider;
    }
    
    public void ShowInterstitialAd()
    {
        GameObject InterstitialAdObject = _provider.Instantiate(InterstitialAdPrefabPath);

        InterstitialVideoAd interstitialVideoAd = InterstitialAdObject.GetComponent<InterstitialVideoAd>();

        interstitialVideoAd.Show();
    }
}
