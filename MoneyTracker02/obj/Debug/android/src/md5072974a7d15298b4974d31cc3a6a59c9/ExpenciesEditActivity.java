package md5072974a7d15298b4974d31cc3a6a59c9;


public class ExpenciesEditActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("MoneyTracker02.ExpenciesEditActivity, MoneyTracker02, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ExpenciesEditActivity.class, __md_methods);
	}


	public ExpenciesEditActivity ()
	{
		super ();
		if (getClass () == ExpenciesEditActivity.class)
			mono.android.TypeManager.Activate ("MoneyTracker02.ExpenciesEditActivity, MoneyTracker02, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
