using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Imaging;

namespace NBA.Models
{
    public class GamePhotos
    {
        public BitmapImage Photo1 { get; set; }
        public BitmapImage Photo2 { get; set; }
        public BitmapImage Photo3 { get; set; }
        public BitmapImage Photo4 { get; set; }

        public GamePhotos(BitmapImage photo1, BitmapImage photo2, BitmapImage photo3, BitmapImage photo4)
        {
            Photo1 = photo1;
            Photo2 = photo2;
            Photo3 = photo3;
            Photo4 = photo4;
        }
    }
}
