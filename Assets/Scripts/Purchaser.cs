using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

// Placing the Purchaser class in the CompleteProject namespace allows it to interact with ScoreManager,
// one of the existing Survival Shooter scripts.
//namespace CompleteProject
//{
// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class Purchaser : MonoBehaviour, IStoreListener
{

	public static Purchaser Instance;
	private static IStoreController m_StoreController;
	// The Unity Purchasing system.
	private static IExtensionProvider m_StoreExtensionProvider;
	// The store-specific Purchasing subsystems.
	public static string PRODUCT_1 = "yourProductID";
	public static string PRODUCT_2 = "yourid1";
	public static string PRODUCT_3 = "yourid1";
	public static string PRODUCT_4 = "yourid1";
	public static string PRODUCT_5 = "yourid1";
	public static string PRODUCT_6 = "yourid1";

	void Start ()
	{		
		if (m_StoreController == null) {			
			InitializePurchasing ();
		}
		if (Instance == null) {
			Instance = this;
		}
	}

	public void InitializePurchasing ()
	{		
		if (IsInitialized ()) {			
			return;
		}
			
		var builder = ConfigurationBuilder.Instance (StandardPurchasingModule.Instance ());
		builder.AddProduct (PRODUCT_6, ProductType.Consumable);
		builder.AddProduct (PRODUCT_1, ProductType.Consumable);
		builder.AddProduct (PRODUCT_2, ProductType.Consumable);
		builder.AddProduct (PRODUCT_3, ProductType.Consumable);
		builder.AddProduct (PRODUCT_4, ProductType.Consumable);
		builder.AddProduct (PRODUCT_5, ProductType.Consumable);

		UnityPurchasing.Initialize (this, builder);
	}


	private bool IsInitialized ()
	{		
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}

	public void BUYPRODUCT1 ()
	{
		BuyProductID (PRODUCT_1);
	}

	public void BUYPRODUCT2 ()
	{		
		BuyProductID (PRODUCT_2);
	}

	public void BUYPRODUCT3 ()
	{		
		BuyProductID (PRODUCT_3);
	}

	public void BUYPRODUCT4 ()
	{		
		BuyProductID (PRODUCT_4);
	}

	public void BUYPRODUCT5 ()
	{		
		BuyProductID (PRODUCT_5);
	}

	public void BUYPRODUCT6 ()
	{		
		BuyProductID (PRODUCT_6);
	}
	void BuyProductID (string productId)
	{		
		if (IsInitialized ()) {						
			Product product = m_StoreController.products.WithID (productId);

			if (product != null && product.availableToPurchase) {
				Debug.Log (string.Format ("Purchasing product asychronously: '{0}'", product.definition.id));
				m_StoreController.InitiatePurchase (product);
			} else {				
				Debug.Log ("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		} else {
			Debug.Log ("BuyProductID FAIL. Not initialized.");
		}
	}
	public void RestorePurchases ()
	{		
		if (!IsInitialized ()) {			
			Debug.Log ("RestorePurchases FAIL. Not initialized.");
			return;
		}
		if (Application.platform == RuntimePlatform.IPhonePlayer
		    || Application.platform == RuntimePlatform.OSXPlayer) {			
			Debug.Log ("RestorePurchases started ...");

			var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions> ();

			apple.RestoreTransactions ((result) => {				
				Debug.Log ("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		} else {			
			Debug.Log ("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}
	//
	// --- IStoreListener
	//
	public void OnInitialized (IStoreController controller, IExtensionProvider extensions)
	{		
		Debug.Log ("OnInitialized: PASS");

		m_StoreController = controller;
		m_StoreExtensionProvider = extensions;
	}

	public void OnInitializeFailed (InitializationFailureReason error)
	{		
		Debug.Log ("OnInitializeFailed InitializationFailureReason:" + error);
	}

	public PurchaseProcessingResult ProcessPurchase (PurchaseEventArgs args)
	{
		// A consumable product has been purchased by this user.
		if (String.Equals (args.purchasedProduct.definition.id, PRODUCT_1, StringComparison.Ordinal)) {
			//		ShopCoinButton.Instance.UpdateCoin (100);
			InAppManager.Instance.AddCoin (0);
		}
		if (String.Equals (args.purchasedProduct.definition.id, PRODUCT_2, StringComparison.Ordinal)) {
			//		ShopCoinButton.Instance.UpdateCoin (500);
			InAppManager.Instance.AddCoin (1);
		}
		if (String.Equals (args.purchasedProduct.definition.id, PRODUCT_3, StringComparison.Ordinal)) {
			//		ShopCoinButton.Instance.UpdateCoin (1500);
			InAppManager.Instance.AddCoin (2);
		}
		if (String.Equals (args.purchasedProduct.definition.id, PRODUCT_4, StringComparison.Ordinal)) {
			//		ShopCoinButton.Instance.UpdateCoin (5000);
			InAppManager.Instance.AddCoin (3);
		}
		if (String.Equals (args.purchasedProduct.definition.id, PRODUCT_5, StringComparison.Ordinal)) {
			//		ShopCoinButton.Instance.UpdateCoin (15000);
			InAppManager.Instance.AddCoin (4);
		}
		// A non-consumable product has been purchased by this user.
		if (String.Equals (args.purchasedProduct.definition.id, PRODUCT_6, StringComparison.Ordinal)) {			
			//	PlayerPrefs.SetInt ("Noads", 1);
			//	AdsManager.Instance.HideBanner ();
			InAppManager.Instance.AddCoin (5);
		}
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed (Product product, PurchaseFailureReason failureReason)
	{
		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
		// this reason with the user to guide their troubleshooting actions.
		Debug.Log (string.Format ("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}
}
//}
