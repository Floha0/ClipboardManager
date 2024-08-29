using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ClipboardManager
{
    internal class Program
    {
        private static Queue<string> copiedTexts = new Queue<string>();
        private static readonly int maxCopiedTexts = 10;
        
        //private static List<string> copiedTexts = new List<string>();
        private static bool newCopy = false;
        private static bool containsAtStart = false;
        [STAThread]
        public static void Main(string[] args)
        {
            
            Console.WriteLine("Clipboard Manager Started!");
            
            
            // Clipboard.Clear();

            // Check the clipboard on startup
            if (Clipboard.ContainsText())
            {
                containsAtStart = true;
                copiedTexts.Enqueue(Clipboard.GetText());
            }
            else
            {
                string tempText = "Empty Clipboard!";
                
                copiedTexts.Enqueue(tempText);
            }
            
            Console.Clear();
            for (int i = 0; i < copiedTexts.Count; i++)
            {
                Console.WriteLine((i+1) + ": " + copiedTexts.ElementAt(i));
            }
            
            
            while (true)
            {
                if (Clipboard.ContainsText())
                {
                    if (!containsAtStart)
                    {
                        newCopy = true;
                    }
                    else
                    {
                        if (Clipboard.GetText() != copiedTexts.Last())
                        {
                            newCopy = true;
                        }
                    }
                }

                if (newCopy)
                {
                    if (!containsAtStart)
                    {
                        copiedTexts.Clear();
                        containsAtStart = true;
                    }

                    if (copiedTexts.Count >= maxCopiedTexts)
                    {
                        copiedTexts.Dequeue(); // Dequeue the oldest copied text
                    }
                    
                    copiedTexts.Enqueue(Clipboard.GetText());

                    Console.Clear();
                    for (int i = 0; i < copiedTexts.Count; i++)
                    {
                        Console.WriteLine((i+1) + ": " + copiedTexts.ElementAt(i));
                    }

                    newCopy = false;
                }

                Thread.Sleep(100);
            }
        }
    }
}