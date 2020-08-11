namespace WhereYouAt
{
    using System;

    using WhereYouAt.Logic;

    internal static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="Args">The arguments.</param>
        private static void Main(string[] Args)
        {
            // 
            // Execute the tests.
            // 

            ExecuteTests(Args);

            // 
            // Wait until a key has been pressed to exit.
            // 

            Console.ReadKey(true);
        }

        /// <summary>
        /// Executes the tests.
        /// </summary>
        /// <param name="Args">The launch arguments.</param>
        private static void ExecuteTests(string[] Args)
        {
            // 
            // Locate my nigga Bush.
            // 

            var CIA = new CIA();
            // CIA.Locate("George W. Bush");
            CIA.Locate(Environment.UserName);
        }
    }
}
