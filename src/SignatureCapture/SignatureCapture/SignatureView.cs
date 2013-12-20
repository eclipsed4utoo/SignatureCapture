using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.CoreAnimation;

namespace SignatureCapture
{
	public class SignatureView : UIView
	{
		private UIBezierPath path;
		private UIImage signatureImage;
		private PointF[] points = new PointF[5];
		private uint controlPoint;
		private int _strokeWidth = 2;
		private UIColor _strokeColor = UIColor.Black;
		private UIColor _backgroundColor = UIColor.White;
		private UIColor _textColor = UIColor.Black;
		private bool _showShadow = true;
		private UIColor _shadowColor = UIColor.Black;

		public UIColor ShadowColor
		{
			get { return _shadowColor; }
			set
			{
				_shadowColor = value;
				SetShadow ();
			}
		}

		public bool ShowShadow 
		{
			get { return _showShadow; }
			set 
			{ 
				_showShadow = value; 
				SetShadow ();
			}
		}

		public int StrokeWidth
		{
			get { return _strokeWidth; }
			set 
			{ 
				_strokeWidth = value; 

				if (path != null)
					path.LineWidth = _strokeWidth;
			}
		}

		public UIColor StrokeColor
		{
			get { return _strokeColor; }
			set { _strokeColor = value; }
		}

		/// <summary>
		/// Gets or sets the color of the view background.
		/// </summary>
		/// <value>The color of the view background. Default is White</value>
		public UIColor ViewBackgroundColor
		{
			get { return _backgroundColor; }
			set 
			{ 
				_backgroundColor = value; 
				InvokeOnMainThread (() => this.BackgroundColor = _backgroundColor);
			}
		}

		public UIColor TextColor
		{
			get { return _textColor; }
			set 
			{ 
				_textColor = value;
				SetUIElementsColor ();
			}
		}

		public event EventHandler SignatureChanged;
		private void OnSignatureChanged()
		{
			if (SignatureChanged != null)
				SignatureChanged (this, EventArgs.Empty);
		}

		public SignatureView (RectangleF rect): base(rect)
		{
			this.MultipleTouchEnabled = false;
			this.BackgroundColor = UIColor.White;
			this.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleTopMargin | UIViewAutoresizing.FlexibleBottomMargin;
			path = new UIBezierPath();
			path.LineWidth = this.StrokeWidth;
			SetSize ();
			SetShadow ();
			CreateUIElements ();
			SubscribeToNotifications ();
		}

		public void SetShadow ()
		{
			CALayer layer = this.Layer;

			if (!this.ShowShadow)
			{
				layer.MasksToBounds = true;
				layer.ShadowPath = UIBezierPath.FromRect(RectangleF.Empty).CGPath;
				return;
			}

			layer.MasksToBounds = false;
			layer.ShadowColor = this.ShadowColor.CGColor;
			layer.ShadowOpacity = 1.0f;
			layer.ShadowOffset = new SizeF(3.0f, 3.0f);
			layer.ShadowPath = UIBezierPath.FromRect(this.Bounds).CGPath;
		}

		private void SetSize()
		{
			var frame = this.Frame;

			if (frame.X == 0)
			{
				frame.X = 6;

				if (frame.Width == UIScreen.MainScreen.ApplicationFrame.Width)
					frame.Width -= 12;
				else
					frame.Width -= 9;
			}	

			this.Frame = frame;
		}

		private void SubscribeToNotifications()
		{
			lineLabel.AddObserver (this, new NSString ("frame"), NSKeyValueObservingOptions.New, IntPtr.Zero);
		}

		public override void ObserveValue (NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
		{
			AddBottomBorder (lineLabel);
			SetShadow ();
		}

		private UILabel xlabel;
		private UILabel lineLabel;
		private UILabel signHereLabel;

		private void CreateUIElements()
		{
			xlabel = new UILabel (new RectangleF (10, this.Frame.Height - 32, 10, 15));
			xlabel.BackgroundColor = UIColor.Clear;
			xlabel.Font = UIFont.SystemFontOfSize (12);
			xlabel.Text = "X";
			this.AddSubview (xlabel);

			lineLabel = new UILabel (new RectangleF (10, this.Frame.Height - 22, this.Frame.Width - 20, 5));
			lineLabel.BackgroundColor = UIColor.Clear;
			lineLabel.AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleTopMargin | UIViewAutoresizing.FlexibleBottomMargin;
			AddBottomBorder (lineLabel);
			this.AddSubview (lineLabel);

			float controlWidth = 75;
			signHereLabel = new UILabel (new RectangleF (GetMiddleOfFrame(controlWidth), this.Frame.Height - 15, controlWidth, 15));
			signHereLabel.BackgroundColor = UIColor.Clear;
			signHereLabel.Text = "Sign here";
			signHereLabel.Font = UIFont.SystemFontOfSize (12);
			signHereLabel.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleRightMargin;
			this.AddSubview (signHereLabel);
		}

		private void AddBottomBorder(UILabel label)
		{
			var layer = label.Layer;
			var bottomBorder = new CALayer ();
			bottomBorder.BorderColor = this.TextColor.CGColor;
			bottomBorder.BorderWidth = 1;
			bottomBorder.Frame = new RectangleF (-1, layer.Frame.Size.Height - 1, layer.Frame.Size.Width, 1);
			layer.AddSublayer (bottomBorder);
		}

		private float GetMiddleOfFrame(float widthOfControl)
		{
			var middleOfFrame = this.Frame.Width / 2;
			var middleOfControl = widthOfControl / 2;
			return middleOfFrame - middleOfControl;
		}

		private void SetUIElementsColor()
		{
			xlabel.TextColor = this.TextColor;
			signHereLabel.TextColor = this.TextColor;
			AddBottomBorder (lineLabel);
		}

		public bool IsSignatureEmpty()
		{
			return signatureImage == null && controlPoint == 0;
		}

		public void ClearSignature()
		{
			if (signatureImage != null)
			{
				signatureImage.Dispose ();
				signatureImage = null;
			}

			path.RemoveAllPoints ();
			SetNeedsDisplay ();
		}

		public override void Draw (RectangleF rect)
		{
			if (signatureImage != null)
				signatureImage.Draw(rect);

			this.StrokeColor.SetStroke ();
			path.Stroke();
		}

		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			controlPoint = 0;
			UITouch touch = touches.AnyObject as UITouch;
			points[0] = touch.LocationInView(this);
		}

		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			if(SignatureChanged != null)
				OnSignatureChanged ();

			UITouch touch = touches.AnyObject as UITouch;
			PointF point = touch.LocationInView(this);
			controlPoint++;
			points[controlPoint] = point;

			if (controlPoint == 3)
			{
				points[2] = new PointF((points[1].X + points[3].X)/2.0f, (points[1].Y + points[3].Y)/2.0f);
				path.MoveTo(points[0]);
				path.AddQuadCurveToPoint (points [2], points [1]);

				this.SetNeedsDisplay ();
				points[0] = points[2];
				points[1] = points[3];
				controlPoint = 1;
			}
		}

		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			if (controlPoint == 0) // only one point acquired = user tapped on the screen
			{
				path.AddArc (points [0], path.LineWidth / 2, 0, (float)(Math.PI * 2), true);
			}
			else if (controlPoint == 1)
			{
				path.MoveTo (points [0]);
				path.AddLineTo (points [1]);
			}
			else if (controlPoint == 2)
			{
				path.MoveTo (points [0]);
				path.AddQuadCurveToPoint (points [2], points [1]);
			}

			this.GetSignatureImage();
			this.SetNeedsDisplay();

			path.RemoveAllPoints();

			controlPoint = 0;
		}

		public override void TouchesCancelled (NSSet touches, UIEvent evt)
		{
			this.TouchesEnded(touches, evt);
		}

		public UIImage GetSignatureImage ()
		{
			UIGraphics.BeginImageContextWithOptions(this.Bounds.Size, false, 0);

			if (signatureImage == null)
			{
				signatureImage = new UIImage ();
				UIBezierPath rectPath = UIBezierPath.FromRect(this.Bounds);
				UIColor.Clear.SetFill();
				rectPath.Fill();
			}

			signatureImage.Draw(new PointF(0,0));

			this.StrokeColor.SetStroke();

			path.Stroke();

			signatureImage = UIGraphics.GetImageFromCurrentImageContext();

			UIGraphics.EndImageContext();
			return signatureImage;
		}
	}
}

