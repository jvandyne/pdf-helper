using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JpgToPdf.ViewModels
{
   using System.Collections;

   public class MainWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ImageModel> Images { get; set; }

      public ObservableCollection<ImageModel> PDFs { get; set; }

      public MainWindowViewModel()
        {
            this.Images = new ObservableCollection<ImageModel>();
         this.PDFs = new ObservableCollection<ImageModel>();
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
