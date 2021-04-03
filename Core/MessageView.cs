using System;
using HtmlTags;
using System.IO;
using TheArtOfDev.HtmlRenderer.Eto;
using System.Text.RegularExpressions;
using Eto.Forms;

namespace IRCCl.Core
{
    public class MessageView
    {
        public HtmlDocument Html;
        public HtmlPanel MessagesWebView = new HtmlPanel();
        private readonly Regex domainRegex = new Regex(@"(((?<scheme>http(s)?):\/\/)?([\w-]+?\.\w+)+([a-zA-Z0-9\~\!\@\#\$\%\^\&amp;\*\(_\-\=\+\\\/\?\.\:\;\,]*)?)", RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        /// Creates new MessageView instance.
        /// </summary>
        public MessageView()
        {
            Html = new HtmlDocument();
            Html.AddStyle(File.ReadAllText("Assets/style.css"));
            MessagesWebView.Text = Html.ToString();
        }

        public string Linkify(string text, string target = "_self")
        {
            return domainRegex.Replace(
                text,
                match => {
                    var link = match.ToString();
                    var scheme = match.Groups["scheme"].Value == "https" ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;

                    var url = new UriBuilder(link) { Scheme = scheme }.Uri.ToString();

                    return string.Format(@"<a href=""{0}"" target=""{1}"">{2}</a>", url, target, link);
                }
            );
        }

        public void UpdateHtml()
        {
            Application.Instance.Invoke(() => MessagesWebView.Text = Html.ToString());
        }

        public void AddSystemMessage(string message)
        {
            Html.Add(new HtmlTag("p").AppendHtml($"{Linkify(message)}"));
        }
    }
}
