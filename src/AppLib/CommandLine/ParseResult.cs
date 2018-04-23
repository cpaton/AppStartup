namespace AppLib.CommandLine
{
    public enum ParseResult
    {
        /// <summary>
        /// Command line arguments were understood and converted into the appropriate type
        /// </summary>
        Successful,
        /// <summary>
        /// Arguments were successfully parsed and understood and requested action was taken.
        /// The application should exit gracefully
        /// Example is the command line arguments requested help or version information
        /// </summary>
        SuccessfulAndExit,
        /// <summary>
        /// Command line arguments could not be parsed/understood
        /// </summary>
        Failed
    }
}