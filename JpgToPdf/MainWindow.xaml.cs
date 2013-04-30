using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

using JpgToPdf.ViewModels;

namespace JpgToPdf
{
   
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.ViewModel = new MainWindowViewModel();
        
            this.DataContext = this.ViewModel;
        }

        void Window_Drop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (var file in files)
            {
                var ext = new FileInfo(file).Extension;

                if (ext == ".jpg" || ext == ".png")
                {
                    this.ViewModel.Images.Add(new ImageModel { FileName = file });
                }
                else if (ext == ".pdf")
                {
                   this.ViewModel.PDFs.Add(new ImageModel{FileName = file});
                }
            }
        }

        void convertToPdfButton_Click(object sender, RoutedEventArgs e)
        {
            Document doc = new Document();
            using (var stream = new FileStream(@"d:\test.pdf", FileMode.Create, FileAccess.ReadWrite))
            {
                PdfWriter.GetInstance(doc, stream);
                doc.Open();

                foreach(var file in this.ViewModel.Images)
                {
                    using (var imageStream = new FileStream(file.FileName, FileMode.Open, FileAccess.Read))
                    {
                        var image = iTextSharp.text.Image.GetInstance(imageStream);
                        image.ScaleAbsolute(500, 500);
                        doc.Add(image);
                        imageStream.Close();
                    }
                }
                doc.Close();
                stream.Close();
            }
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Images.Clear();
        }
    }
}
