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
		MonoTouch.UIKit.UIButton SetBackgroundColorButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton SetShadowColorButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton SetStrokeColorButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton SetStrokeWidthButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton SetTextColorButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton ShowShadowButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView SignatureView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ClearButton != null) {
				ClearButton.Dispose ();
				ClearButton = null;
			}

			if (SetBackgroundColorButton != null) {
				SetBackgroundColorButton.Dispose ();
				SetBackgroundColorButton = null;
			}

			if (SetShadowColorButton != null) {
				SetShadowColorButton.Dispose ();
				SetShadowColorButton = null;
			}

			if (SetStrokeColorButton != null) {
				SetStrokeColorButton.Dispose ();
				SetStrokeColorButton = null;
			}

			if (SetStrokeWidthButton != null) {
				SetStrokeWidthButton.Dispose ();
				SetStrokeWidthButton = null;
			}

			if (SetTextColorButton != null) {
				SetTextColorButton.Dispose ();
				SetTextColorButton = null;
			}

			if (ShowShadowButton != null) {
				ShowShadowButton.Dispose ();
				ShowShadowButton = null;
			}

			if (SignatureView != null) {
				SignatureView.Dispose ();
				SignatureView = null;
			}
		}
	}
}
