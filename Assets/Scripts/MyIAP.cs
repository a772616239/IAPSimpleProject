using System;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;

public class MyIAP : MonoBehaviour, IDetailedStoreListener
{
    IStoreController m_StoreController; // The Unity Purchasing system.
    void Start()
    {
        InitializePurchasing();
    }

    void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Add products that will be purchasable and indicate its type.
        //��ʼ����Ʒ�б�����Ҫ��IOS��Google��̨�Ĳ�Ʒ�б�һ��
        builder.AddProduct("buycoin1", ProductType.Consumable);
        builder.AddProduct("buycoin2", ProductType.Consumable);
        builder.AddProduct("buycoin3", ProductType.Consumable);
        builder.AddProduct("buycoin4", ProductType.Consumable);
        builder.AddProduct("buycoin5", ProductType.Consumable);
        builder.AddProduct("buycoin6", ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }


    public void BuyProduct(string pruductid)
    {
        var product= m_StoreController.products.WithID(pruductid);

        m_StoreController.InitiatePurchase(product);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("In-App Purchasing successfully initialized");
        m_StoreController = controller;

        // 在初始化完成后获取商品信息
        for (int i = 0; i < transform.childCount; i++)
        {
            var trans= transform.GetChild(i);
            
            SetProduct(trans.GetComponent<Button>(), "buycoin"+(i+1));
        }
    }

    public void SetProduct(Button btn,string product1)
    {
        Product product = m_StoreController.products.WithID(product1);

        // Product product = m_StoreController.products.WithID("buycoin1");

        if (product != null && product.metadata != null)
        {
            // 获取本地化价格字符串（如 "$4.99" 或 "€4.99"）
            string localizedPrice = product.metadata.localizedPriceString;
    
            // 获取货币代码（如 "USD"、"EUR"、"JPY"）
            string currencyCode = product.metadata.isoCurrencyCode;
            var txt= btn.transform.Find("txt");
            if (txt)
            {
                txt.GetComponent<Text>().text = localizedPrice;
            }
            btn.onClick.AddListener((() =>
            {
                BuyProduct(product1);
            }));
            // 显示到UI
            // priceText.text = localizedPrice; 
        }
        else
        {
            Debug.LogError("no p:"+product1);
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("In-App Purchasing OnInitializeFailed"+error);

        OnInitializeFailed(error, null);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";

        if (message != null)
        {
            errorMessage += $" More details: {message}";
        }

        Debug.Log(errorMessage);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        //Retrieve the purchased product
        var product = args.purchasedProduct;

        //Add the purchased product to the players inventory
        Debug.Log($"Purchase Complete - Product: {product.definition.id}");

        //We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}'," +
            $" Purchase failure reason: {failureDescription.reason}," +
            $" Purchase failure details: {failureDescription.message}");
    }

}