using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Projekt
{
    class Note
    {
        public TextRange range { get; set; }
        public FileStream fileStream { get; set; }

        public Note()
        {

        }

        public void SaveFile(string fileName, RichTextBox richTextBox)
        {
            
            range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            fileStream = new FileStream(fileName, FileMode.Create);
            range.Save(fileStream, DataFormats.Rtf);
            fileStream.Close();
        }

        public void LoadFile(string fileName, RichTextBox richTextBox)
        {
            
            if (File.Exists(fileName))
            {
                range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                fileStream = new FileStream(fileName, FileMode.OpenOrCreate);
                range.Load(fileStream, DataFormats.Rtf);
                fileStream.Close();
            }
        }
    }
}
