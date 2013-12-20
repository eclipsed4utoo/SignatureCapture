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
			signature.BackgroundColor = UIColor.Gray;

			this.View.AddSubview (signature);

			// clears the signature
			this.ClearButton.TouchUpInside += (object sender, EventArgs e) => 
			{
				signature.ClearSignature();
			};

			// shows/hides shadow
			this.ShowShadowButton.TouchUpInside += (object sender, EventArgs e) => 
			{
				if (this.ShowShadowButton.CurrentTitle.Contains("Show"))
				{
					signature.ShowShadow = false;
					SetButtonText(this.ShowShadowButton, "Hide Shadow");
				}
				else
				{
					signature.ShowShadow = true;
					SetButtonText(this.ShowShadowButton, "Show Shadow");
				}
			};

			// sets shadow color
			this.SetShadowColorButton.TouchUpInside += (object sender, EventArgs e) => 
			{
				if (this.SetShadowColorButton.CurrentTitle.Contains("Green"))
				{
					signature.ShadowColor = UIColor.Green;
					SetButtonText(this.SetShadowColorButton, "Set Shadow Color To Black");
				}
				else
				{
					signature.ShadowColor = UIColor.Black;
					SetButtonText(this.SetShadowColorButton, "Set Shadow Color To Green");
				}
			};

			// sets stroke color of the signature
			this.SetStrokeColorButton.TouchUpInside += (object sender, EventArgs e) => 
			{
				if (this.SetStrokeColorButton.CurrentTitle.Contains("Red"))
				{
					signature.StrokeColor = UIColor.Red;
					SetButtonText(this.SetStrokeColorButton, "Set Stroke Color To Black");
				}
				else
				{
					signature.StrokeColor = UIColor.Black;
					SetButtonText(this.SetStrokeColorButton, "Set Stroke Color To Red");
				}
			};

			// sets stroke width of the signature lines
			this.SetStrokeWidthButton.TouchUpInside += (object sender, EventArgs e) => 
			{
				if (this.SetStrokeWidthButton.CurrentTitle.Contains("10"))
				{
					signature.StrokeWidth = 10;
					SetButtonText(this.SetStrokeWidthButton, "Set Stroke Width to 2");
				}
				else
				{
					signature.StrokeWidth = 2;
					SetButtonText(this.SetStrokeWidthButton, "Set Stroke Width to 10");
				}
			};

			// sets background color of the signature view
			this.SetBackgroundColorButton.TouchUpInside += (object sender, EventArgs e) => 
			{
				if (this.SetBackgroundColorButton.CurrentTitle.Contains("Black"))
				{
					signature.ViewBackgroundColor = UIColor.Black;
					SetButtonText(this.SetBackgroundColorButton, "Set View Background To Grey");
				}
				else
				{
					signature.ViewBackgroundColor = UIColor.Gray;
					SetButtonText(this.SetBackgroundColorButton, "Set View Background To Black");
				}
			};

			// sets the color of the text of the UI elements on the signature view
			this.SetTextColorButton.TouchUpInside += (object sender, EventArgs e) => 
			{
				if (this.SetTextColorButton.CurrentTitle.Contains("Blue"))
				{
					signature.TextColor = UIColor.Blue;
					SetButtonText(this.SetTextColorButton, "Set Text Color To Black");
				}
				else
				{
					signature.TextColor = UIColor.Black;
					SetButtonText(this.SetTextColorButton, "Set Text Color To Blue");
				}
			};
		}

		private void SetButtonText(UIButton button, string text)
		{
			button.SetTitle (text, UIControlState.Normal);
		}
	}
}

