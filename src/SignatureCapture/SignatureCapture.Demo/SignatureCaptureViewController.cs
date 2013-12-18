using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace SignatureCapture.Demo
{
	public partial class SignatureCaptureViewController : UIViewController
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public SignatureCaptureViewController ()
			: base (UserInterfaceIdiomIsPhone ? "SignatureCaptureViewController_iPhone" : "SignatureCaptureViewController_iPad", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			var signature = new SignatureView (this.SignatureView.Frame);
			signature.StrokeWidth = 2;
			signature.StrokeColor = UIColor.Red;
			signature.BackgroundColor = UIColor.Gray;
			this.View.AddSubview (signature);

			this.ClearButton.TouchUpInside += (object sender, EventArgs e) => 
			{
				signature.ClearSignature();
			};
		}
	}
}

