using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace CokeCanGen_WPF
{
    /// <summary>
    /// Interaction logic for LabelAutoFontSize.xaml
    /// </summary>
    public partial class LabelAutoFontSize : UserControl
    {
        public LabelAutoFontSize()
        {
            InitializeComponent();
        }

        public string CokeText { get; set; }

    }
    [ValueConversion(typeof(object), typeof(double))]
    public class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
         object parameter, CultureInfo culture)
        {
            double dblValue = (double)value;
            double scale = Double.Parse(((string)parameter), System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            return dblValue * scale;
        }

        public object ConvertBack(object value, Type targetType,
         object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
