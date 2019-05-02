using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ZiliCountBeer.Controls
{
    public class MyBoxView : BoxView
    {
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(MyBoxView), 0.0);
        public double CornerRadius
        {
            get
            {
                return (double)GetValue(CornerRadiusProperty);
            }
            set
            {
                SetValue(CornerRadiusProperty, value);
            }
        }
    }
}
