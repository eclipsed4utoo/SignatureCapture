Signature Capture
================

Signature Capture Library for Xamarin.iOS (MonoTouch)

### Simple Usage


    public override void ViewDidLoad()
    {
       var signature = new UISignatureView(new RectangeF(0, 40, UIScreen.MainScreen.ApplicationFrame.Width, 200));
       this.View.AddSubview(signature);
    }


### Additional Properties

`StrokeWidth` - Allows the setting of the width of the signature line. Defaults to 2  
`StrokeColor` - Allows the setting of the color of the signature line. Defaults to Black  
`TextColor` - Allows the setting of the text color of the UI elements. Defaults to Black  
`ViewBackgroundColor` - Allows the setting of the background color of the signature capture pad. Same as setting `BackgroundColor` on the view. Defaults to White  
`ShowShadow` - Determines whether to show a shadow around the signature view. Defaults to true  
`ShadowColor` - Allows the setting of the shadow color. Defaults to Black  


### License
> The MIT License (MIT)
> 
> Copyright (c) 2013 Ryan Alford
> 
> Permission is hereby granted, free of charge, to any person obtaining a copy
> of this software and associated documentation files (the "Software"), to deal
> in the Software without restriction, including without limitation the rights
> to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
> copies of the Software, and to permit persons to whom the Software is
> furnished to do so, subject to the following conditions:
> 
> The above copyright notice and this permission notice shall be included in all
> copies or substantial portions of the Software.
> 
> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
> IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
> FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
> AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
> LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
> OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
> SOFTWARE.
