using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ImageSorter
{
    public class ImageViewViewModel : INotifyPropertyChanged
    {
        private List<string> _files;

        public BitmapImage Image { get; set; }
        private int index = 0;

        public ImageViewViewModel()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void setup()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            _files = new List<string>();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var ext = new List<string> { ".jpg", ".jpeg", ".tif", ".gif", ".png", ".jfif" };
                var dir = dialog.SelectedPath;
                _files = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories).OrderBy(i => System.IO.Path.GetExtension(i)).Where(s => ext.Contains(System.IO.Path.GetExtension(s))).ToList();
            }
            if (_files.Count > 0)
                setImage(_files[index]);
            else
            {
                var res = MessageBox.Show("Es wurden keine Bilder gefunden", "Fehler", MessageBoxButtons.OK);
                if (res == DialogResult.OK)
                    setup();
            }
        }

        private void setImage(string file, bool rotate = false)
        {
            var stream = new MemoryStream(File.ReadAllBytes(file));
            stream.Position = 0;


            BitmapImage bmpImage = new BitmapImage();

            bmpImage.BeginInit();
            bmpImage.StreamSource = stream;
            bmpImage.CacheOption = BitmapCacheOption.OnLoad;
            if (rotate)
                bmpImage.Rotation = Rotation.Rotate0;
            bmpImage.EndInit();
            bmpImage.Freeze();
            Image = bmpImage;
        }

        private void moveImage(string file, string path)
        {
            File.Move(file, System.IO.Path.Combine(path, System.IO.Path.GetFileName(file)));
        }

        public void changeImage(System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                if (index < _files.Count)
                    setImage(_files[index], true);
            }
            else
            {
                string path = "";
                bool move = false;
                switch (e.Key)
                {
                    case Key.NumPad0:
                        {
                            path = @"D:\Archiv\Bilder\Trash";
                            move = true;
                            break;
                        }
                    case Key.NumPad1:
                        {
                            path = @"D:\Archiv\Bilder\Bratanzlatan";
                            move = true;
                            break;
                        }
                    case Key.NumPad2:
                        {
                            path = @"D:\Archiv\Bilder\Bruhs - Parkour - Squad";
                            move = true;
                            break;
                        }
                    case Key.NumPad3:
                        {
                            path = @"D:\Archiv\Bilder\Fam";
                            move = true;
                            break;
                        }
                    case Key.NumPad4:
                        {
                            path = @"D:\Archiv\Bilder\hotstuff";
                            move = true;
                            break;
                        }
                    case Key.NumPad5:
                        {
                            path = @"C:\Users\Denis\Desktop\Fam";
                            move = true;
                            break;
                        }
                    case Key.NumPad6:
                        {
                            path = @"D:\Archiv\Bilder\Unsortiert";
                            move = true;
                            break;
                        }
                }
                if (move)
                {
                    moveImage(_files[index], path);
                    index++;
                    if (index < _files.Count)
                        setImage(_files[index]);
                    else
                    {
                        MessageBox.Show("Keine Bilder mehr");
                    }
                }
            }
        }
    }
}
