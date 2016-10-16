using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Chapter_35_Smugmug_Fetcher
{
    public class ImageItem
    {
        BitmapImage m_bitmapImage;
        public ImageSource Image { get; set; }
        public event EventHandler<EventArgs> ImageReady;

        private void CreateBitmapFromBuffer(byte[] buffer)
        {
            MemoryStream stream = new MemoryStream(buffer);
            stream.Seek(0, SeekOrigin.Begin);

            m_bitmapImage = new BitmapImage();
            m_bitmapImage.BeginInit();
            m_bitmapImage.StreamSource = stream;
            m_bitmapImage.EndInit();
        }

        void SetImageDpi()
        {
            double dpi = 72;
            int width = m_bitmapImage.PixelWidth;
            int height = m_bitmapImage.PixelHeight;

            int stride = width * 4;

            byte[] pixelData = new byte[stride * height];
            m_bitmapImage.CopyPixels(pixelData, stride, 0);

            BitmapSource bitmapSource = BitmapSource.Create(width, height, dpi, dpi, PixelFormats.Bgra32, null, pixelData, stride);
            bitmapSource.Freeze();

            Image = bitmapSource;
        }

        #region synchronous

        public void LoadImageSynchronously(string url)
        {
            WebClient webClient = new WebClient();
            byte[] buffer = webClient.DownloadData(url);
            CreateBitmapFromBuffer(buffer);
            SetImageDpi();
        }

        #endregion

        #region OldAsynchronous

        int m_offset;
        int m_contentLength;
        byte[] m_buffer;
        Stream m_stream;

public void LoadImageStart(string url)
{
    WebRequest request = WebRequest.CreateHttp(url);

    request.BeginGetResponse(new AsyncCallback(LoadImageResponseCallback), request);
}

public void LoadImageResponseCallback(IAsyncResult result)
{
    WebRequest request = (WebRequest)result.AsyncState;

    WebResponse response = request.EndGetResponse(result);

    m_stream = response.GetResponseStream();
    m_offset = 0;
    m_contentLength = (int) response.ContentLength;
    m_buffer = new byte[m_contentLength];

    StartRead();
}

        private void StartRead()
        {
            m_stream.BeginRead(m_buffer, m_offset, m_contentLength - m_offset, new AsyncCallback(ReadResponseCallback), null);
        }

        public void ReadResponseCallback(IAsyncResult result)
        {
            int bytesRead = m_stream.EndRead(result);

            if (bytesRead + m_offset == m_contentLength)
            {
                DownloadCompleted();
            }
            else
            {
                m_offset += bytesRead;
                StartRead();
            }
        }

        void DownloadCompleted()
        {
            CreateBitmapFromBuffer(m_buffer);
            SetImageDpi();
            if (ImageReady != null)
            {
                ImageReady(this, null);
            }
        }
        #endregion


        #region synchronous

        public async Task<ImageItem> LoadImageAsync(string url)
        {
            byte[] buffer = await new WebClient().DownloadDataTaskAsync(new Uri(url));
            CreateBitmapFromBuffer(buffer);
            SetImageDpi();
            return this;
        }

        static async void WebMethodDownload(string url)
        {
            string contents = await new WebClient().DownloadStringTaskAsync("http://www.bing.com");
            Console.WriteLine(contents);
        }

        #endregion
    }
}
