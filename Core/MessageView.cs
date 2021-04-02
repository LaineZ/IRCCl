using System;
using System.Collections.Generic;
using System.Text;
using HtmlTags;
using System.IO;
using Eto.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using TheArtOfDev.HtmlRenderer.Eto;

namespace IRCCl.Core
{
    public class MessageView
    {
        public HtmlDocument Html;
        public HtmlPanel MessagesWebView = new HtmlPanel();

        /// <summary>
        /// Creates new MessageView instance.
        /// </summary>
        public MessageView()
        {
            Html = new HtmlDocument();
            Html.AddStyle(File.ReadAllText("Assets/style.css"));

        }

        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        private void MessagesWebView_DocumentLoading(object sender, WebViewLoadingEventArgs e)
        {
            if (e.Uri.ToString() != "about:blank")
            {
                e.Cancel = true;
                OpenUrl(e.Uri.ToString());
            }
        }

        public void UpdateHtml()
        {
            MessagesWebView.Text = Html.ToString();
        }

        public void AddSystemMessage(string message)
        {
            Html.Add(new HtmlTag("p").Text($"[{DateTime.Now}] {message}"));
            UpdateHtml();
        }

        public void AddImage(string src)
        {
            Html.Add(new HtmlTag("a").Attr("href", src).Text(src));
            Html.Add(new HtmlTag("img").Attr("src", src));
            UpdateHtml();
        }

    }
}
