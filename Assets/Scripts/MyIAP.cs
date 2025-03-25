using System;
using Unity.Services.Core;
using UnityEngine;
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

        UnityPurchasing.Initialize(this, builder);
    }
    //�����õģ���ʽ�����ɾ��
    public void BuyDimaond1()
    {
        BuyProduct("buycoin1");
    }
    //�����õģ���ʽ�����ɾ��
    public void BuyDimaond2()
    {
        BuyProduct("buycoin2");
    }
    //����ʱ���õĽӿڣ��ⲿֻ�������һ���ӿڼ���
    public void BuyProduct(string pruductid)
    {
        //��ʼ����
        m_StoreController.InitiatePurchase(m_StoreController.products.WithID(pruductid));
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        //��ʼ���ɹ�
        Debug.Log("In-App Purchasing successfully initialized");
        m_StoreController = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        //��ʼ��ʧ��
        OnInitializeFailed(error, null);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        //��ʼ��ʧ��
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
        //����ɹ���֪ͨ����������
        //�˴���Ҫ��������߼���֪ͨ�Լ��ķ���������������߾�ʡ���ˡ�
        /*
         ***
         ***
         ***
         */
        Debug.Log($"Purchase Complete - Product: {product.definition.id}");

        //We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        //����ʧ��
        Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        //����ʧ��
        Debug.Log($"Purchase failed - Product: '{product.definition.id}'," +
            $" Purchase failure reason: {failureDescription.reason}," +
            $" Purchase failure details: {failureDescription.message}");
    }

}