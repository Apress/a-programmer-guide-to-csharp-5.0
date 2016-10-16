using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chapter_35_Smugmug_Fetcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SmugMugFeed m_smugMugFeed;
        ObservableCollection<ImageItem> m_AllImages = new ObservableCollection<ImageItem>();

        public MainWindow()
        {
            m_smugMugFeed = new SmugMugFeed("popular");

            InitializeComponent();
            DataContext = this;
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, EventArgs e)
        {
            //LoadImagesSynchronously();
            //LoadImagesThreadpool();
            LoadImagesAsynchronously();
            //LoadImagesAsyncNewInOrder();
            //LoadImagesAsyncNewInParallel();

            //WebMethodDownload("http://www.random.org/integers/?num=1&min=1&max=100&col=1&base=10&format=plain&rnd=new");
        }

        void LoadImagesSynchronously()
        {
            List<string> urls = m_smugMugFeed.Fetch();

            int i = 0;
            foreach (string url in urls)
            {
                ImageItem imageItem = new ImageItem();
                imageItem.LoadImageSynchronously(url);

                m_AllImages.Add(imageItem);
                i++;
                if (i == 50)
                {
                    break;
                }
            }
        }

        void LoadImagesThreadpool()
        {
            List<string> urls = m_smugMugFeed.Fetch();

            ThreadPool.SetMinThreads(15, 15);
            int i = 0;
            foreach (string url in urls)
            {
                ThreadPool.QueueUserWorkItem(LoadImageWorker, url);
                i++;
                if (i == 50)
                {
                    break;
                }
            }
        }

        void LoadImagesAsynchronously()
        {
            List<string> urls = m_smugMugFeed.Fetch();

            int i = 0;
            foreach (string url in urls)
            {
                ImageItem imageItem = new ImageItem();
                imageItem.ImageReady += imageItem_ImageReady;
                imageItem.LoadImageStart(url);
                i++;
                if (i == 100)
                {
                    break;
                }
            }
        }

        void imageItem_ImageReady(object sender, EventArgs e)
        {
            ImageItem imageItem = (ImageItem)sender;
            Dispatcher.BeginInvoke(new Action<ImageItem>(AddImageToUI), imageItem);        
        }

        void AddImageToUI(ImageItem imageItem)
        {
            m_AllImages.Add(imageItem);
            ImageCount = "Count: " + m_AllImages.Count.ToString();
        }

        void LoadImageWorker(object urlObject)
        {
            string url = (string)urlObject;

            ImageItem imageItem = new ImageItem();
            imageItem.LoadImageSynchronously(url);

            Dispatcher.BeginInvoke(new Action<ImageItem>(AddImageToUI), imageItem);
        }

async void LoadImagesAsyncNewInOrder()
{
    List<string> urls = m_smugMugFeed.Fetch();

    int i = 0;
    foreach (string url in urls)
    {
        ImageItem imageItem = new ImageItem();
        await imageItem.LoadImageAsync(url);
        AddImageToUI(imageItem);
        
        i++;
        if (i == 100)
        {
            break;
        }
    }
}

void LoadImage(string url)
{
    ImageItem imageItem = new ImageItem();
    imageItem.LoadImageSynchronously(url);
    Dispatcher.BeginInvoke(new Action<ImageItem>(AddImageToUI), imageItem);
}

async void LoadImageAsync(string url)
{
    ImageItem imageItem = new ImageItem();
    await imageItem.LoadImageAsync(url);
    await Dispatcher.BeginInvoke(new Action<ImageItem>(AddImageToUI), imageItem);
}

void LoadImagesAsyncNewInParallel()
{
    List<string> urls = m_smugMugFeed.Fetch();

    ThreadPool.SetMinThreads(25, 25);
    foreach (string url in urls)
    {
        Task.Run(() => LoadImageAsync(url));
        //Task.Run(() => LoadImage(url));
    }
}

        public string ImageCount
        { 
            get { return (string)GetValue(ImageCountProperty); }
            set { SetValue(ImageCountProperty, value); } 
        }

        public static readonly DependencyProperty ImageCountProperty = DependencyProperty.Register("ImageCountProperty", typeof(string), typeof(Window), new UIPropertyMetadata(string.Empty)); 

        public ObservableCollection<ImageItem> AllImages
        {
            get { return m_AllImages; }
        }

async void WebMethodDownload(string url)
{
    string contents = await new WebClient().DownloadStringTaskAsync(url);
    c_label.Content = contents;
}
    }
}
