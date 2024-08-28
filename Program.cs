using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace ClipboardManager
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            bool newCopy = false;
            List<string> copiedTexts = new List<string>();
            
            Clipboard.Clear();
            Console.WriteLine("Clipboard Manager Started!");
            
            while (true)
            {
                // Ctrl + C is pressed
                if ((Control.ModifierKeys & Keys.C) != 0 && (Control.ModifierKeys & Keys.Control) != 0)
                {
                    if (!newCopy)
                    {
                        newCopy = true;
                    }
                }
                else
                {
                    // Ctrl + C is released
                    if (newCopy)
                    {
                        if (Clipboard.ContainsText())
                        {
                            copiedTexts.Add(Clipboard.GetText());
                        }
                        else if (Clipboard.ContainsImage())
                        {
                            copiedTexts.Add("Image Copied!");
                        }
                        else if (Clipboard.ContainsAudio())
                        {
                            copiedTexts.Add("Audio Copied!");
                        }

                        Console.Clear();
                        for (int i = 0; i < copiedTexts.Count; i++)
                        {
                            Console.WriteLine(i + ": " + copiedTexts[i]);
                        }

                        newCopy = false;
                    }
                }

                Thread.Sleep(25);
            }
        }
    }
}