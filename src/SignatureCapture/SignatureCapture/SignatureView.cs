using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;

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
				this.BackgroundColor = _backgroundColor;
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
			path = new UIBezierPath();
			path.LineWidth = this.StrokeWidth;
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

