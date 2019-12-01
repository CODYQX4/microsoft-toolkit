namespace Common
{
    /// <summary>
    /// Object that contains the result of running a command
    /// </summary>
    public class Result
    {
        /// <summary>
        /// The string containing the error of the command
        /// </summary>
        private string _error;

        /// <summary>
        /// The Exit code of the command
        /// </summary>
        private int _exitCode;

        /// <summary>
        /// Creates a new CMDOutput object with the default values:
        /// - ExitCode: 0
        /// - Output: string.Empty
        /// </summary>
        public Result()
        {
            _exitCode = 0;
            Output = string.Empty;
            _error = string.Empty;
        }

        /// <summary>
        /// Gets or sets the ExitCode of the command
        /// </summary>
        public int ExitCode
        {
            set
            {
                _exitCode = value;
            }
            get
            {
                return _exitCode;
            }
        }

        /// <summary>
        /// Gets or sets the returned string of the command
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// Gets or sets the string containing the of the command.
        /// If the ExitCode is 0 it is set to -1 to indicate an error.
        /// </summary>
        public string Error
        {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
                // If the ExitCode is 0 it is set to -1 to indicate an error.
                if (_exitCode == 0)
                {
                    _exitCode = -1;
                }
            }
        }

        /// <summary>
        /// Indicates if the CMDOutput objet has an ExitCode other than zero.
        /// - ExitCode == 0 --> There was no error
        /// - ExitCode != 0 --> There is an error
        /// </summary>
        public bool HasError
        {
            get
            {
                return _exitCode != 0;
            }
        }
    }
}