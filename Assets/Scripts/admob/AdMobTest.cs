// using UnityEngine;
// using GoogleMobileAds.Api;
//
// public class AdMobTest : MonoBehaviour
// {
//     // 测试广告ID（来自Google官方测试单元）
//     private string _appId = "ca-app-pub-3940256099942544~3347511713"; // 应用ID
//     private string _bannerAdId = "ca-app-pub-3940256099942544/6300978111"; // 横幅广告
//     private string _interstitialAdId = "ca-app-pub-3940256099942544/1033173712"; // 插页广告
//     private string _rewardedAdId = "ca-app-pub-3940256099942544/5224354917"; // 激励视频
//
//     private BannerView _bannerView;
//     private InterstitialAd _interstitialAd;
//     private RewardedAd _rewardedAd;
//
//     void Start()
//     {
//         // 初始化SDK
//         MobileAds.Initialize(initStatus => 
//         {
//             Debug.Log("AdMob SDK初始化完成");
//             RequestBanner();
//             RequestInterstitial();
//             RequestRewardedAd();
//         });
//     }
//
//     // 横幅广告
//     private void RequestBanner()
//     {
//         _bannerView = new BannerView(_bannerAdId, AdSize.Banner, AdPosition.Bottom);
//         AdRequest request = new AdRequest();
//         _bannerView.LoadAd(request);
//         _bannerView.Show();
//     }
//
//     // 插页广告
//     private void RequestInterstitial()
//     {
//         _interstitialAd = new InterstitialAd(_interstitialAdId);
//         AdRequest request = new AdRequest();
//         _interstitialAd.LoadAd(request);
//         _interstitialAd.OnAdLoaded += (sender, args) => 
//         {
//             Debug.Log("插页广告加载完成");
//             _interstitialAd.Show();
//         };
//     }
//
//     // 激励视频广告
//     private void RequestRewardedAd()
//     {
//         _rewardedAd = new RewardedAd().;
//         AdRequest request = new AdRequest();
//         _rewardedAd.LoadAd(request);
//         _rewardedAd.OnUserEarnedReward += (sender, reward) => 
//         {
//             Debug.Log($"用户获得奖励：{reward.Amount} {reward.Type}");
//         };
//     }
//
//     // 手动触发激励视频
//     public void ShowRewardedAd()
//     {
//         if (_rewardedAd.IsLoaded()) 
//         {
//             _rewardedAd.Show();
//         }
//     }
// }