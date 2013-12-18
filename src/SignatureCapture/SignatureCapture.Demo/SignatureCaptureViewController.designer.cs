// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace SignatureCapture.Demo
{
	[Register ("SignatureCaptureViewController")]
	partial class SignatureCaptureViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton ClearButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView SignatureView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ClearButton != null) {
				ClearButton.Dispose ();
				ClearButton = null;
			}

			if (SignatureView != null) {
				SignatureView.Dispose ();
				SignatureView = null;
			}
		}
	}
}
