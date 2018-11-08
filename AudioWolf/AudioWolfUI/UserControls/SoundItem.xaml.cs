using AudioWolfStandard.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WaveFormRendererLib;

namespace AudioWolfUI.UserControls
{
    /// <summary>
    /// Interaction logic for SoundItem.xaml
    /// </summary>
    public partial class SoundItem : UserControl
    {
        public SoundItemData SoundItemData = new SoundItemData();

        private readonly WaveFormRendererSettings _standardSettings;

        private readonly WaveFormRenderer _waveFormRenderer = new WaveFormRenderer();

        public SoundItem()
        {
            InitializeComponent();

            RenderWaveform();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public string Title
        {
            get
            {
                return SoundItemData.Name;
            }
        }

        private IPeakProvider GetPeakProvider()
        {
            var option = 0;
            switch (option)
            {
                case 0:
                    return new MaxPeakProvider();

                case 1:
                    return new RmsPeakProvider(16);

                case 2:
                    return new SamplingPeakProvider(16);

                case 3:
                    return new AveragePeakProvider(4);

                default:
                    throw new InvalidOperationException("Unknown calculation strategy");
            }
        }

        private WaveFormRendererSettings GetRendererSettings()
        {
            var settings = new WaveFormRendererSettings()
            {
                TopHeight = 20,
                BottomHeight = 20,
                Width = 10,
                DecibelScale = true
            };

            return settings;
        }

        private void RenderThreadFunc(IPeakProvider peakProvider, WaveFormRendererSettings settings)
        {
            System.Drawing.Image image = null;
            try
            {
                image = _waveFormRenderer.Render(SoundItemData.FullPath, peakProvider, settings);

                Dispatcher.Invoke(() =>
                {
                    MemoryStream stream = new MemoryStream();
                    image.Save(stream, ImageFormat.Png);

                    BitmapImage tempBitmap = new BitmapImage();
                    tempBitmap.BeginInit();
                    tempBitmap.StreamSource = stream;
                    tempBitmap.EndInit();
                    SoundWave.Stretch = Stretch.Fill;
                    SoundWave.Source = tempBitmap;
                });
            }
            catch (Exception e)
            {
                int i = 0;
                i++;
            }
        }

        private void RenderWaveform()
        {
            var settings = GetRendererSettings();
            // settings.BackgroundBrush = new System.Drawing.Brush(System.Drawing.Color.Black);
            settings.BottomPeakPen = new System.Drawing.Pen(System.Drawing.Color.DarkSeaGreen);
            settings.BottomSpacerPen = new System.Drawing.Pen(System.Drawing.Color.DarkSlateGray);
            settings.TopPeakPen = new System.Drawing.Pen(System.Drawing.Color.Yellow);

            if (SoundItemData.FullPath != null)
            {
                settings.BackgroundImage = new Bitmap(SoundItemData.FullPath);
            }

            settings.Width = 400;
            settings.BottomHeight = 50;
            settings.TopHeight = 100;
            SoundWave.Source = null;

            var peakProvider = GetPeakProvider();
            Task.Factory.StartNew(() => RenderThreadFunc(peakProvider, settings));
        }
    }
}
