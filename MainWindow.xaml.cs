using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CokeCanGen_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        // CanName.Content = NameBox.Text;
        //CanName.Font = new Font(CanName.FontFamily, 32 - 0.5f, CanName.FontStyle);
        //while (CanName.Width < System.Windows.Forms.TextRenderer.MeasureText(CanName.Content,
        //new Font(CanName.FontFamily, (float)CanName.FontSize, CanName.FontStyle)).Width)
        //{
        //     CanName.Font = new Font(CanName.FontFamily, (float)CanName.FontSize - 0.5f, CanName.FontStyle);
        //}
        //       }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Save the file.
                string filename = "Share a Coke with " + NameBox.Text + ".png";
                SaveControlImage(ImageGrid, filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // Save a control's image.
        private static void SaveControlImage(FrameworkElement control,
            string filename)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = filename, // Default file name
                DefaultExt = ".png", // Default file extension
                Filter = "Portable Network Graphics (.png)|*.png" // Filter files by extension
            };

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string fn = dlg.FileName;
                // Get the size of the Visual and its descendants.
                Rect rect = VisualTreeHelper.GetDescendantBounds(control);

                // Make a DrawingVisual to make a screen
                // representation of the control.
                DrawingVisual dv = new DrawingVisual();

                // Fill a rectangle the same size as the control
                // with a brush containing images of the control.
                using (DrawingContext ctx = dv.RenderOpen())
                {
                    VisualBrush brush = new VisualBrush(control);
                    ctx.DrawRectangle(brush, null, new Rect(rect.Size));
                }

                // Make a bitmap and draw on it.
                int width = (int)control.ActualWidth;
                int height = (int)control.ActualHeight;
                RenderTargetBitmap rtb = new RenderTargetBitmap(
                    width, height, 96, 96, PixelFormats.Pbgra32);
                rtb.Render(dv);

                // Make a PNG encoder.
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));

                // Save the file.
                using FileStream fs = new FileStream(fn,
                    FileMode.Create, FileAccess.Write, FileShare.None);
                encoder.Save(fs);
            }

        }
    }
}
