using System;
using System.Collections.Generic;
using System.Text;
using HtmlTags;
using System.IO;

namespace IRCCl.Core
{
    public class MessageView
    {
        public HtmlDocument Html;

        /// <summary>
        /// Creates new MessageView instance.
        /// </summary>
        public MessageView()
        {
            Html = new HtmlDocument();
            Html.AddStyle(File.ReadAllText("style.css"));
        }

        public void AddSystemMessage(string message)
        {
            Html.Add(new HtmlTag("p").Text($"[{DateTime.Now}] SYSTEM: {message}"));
        }

    }
}
