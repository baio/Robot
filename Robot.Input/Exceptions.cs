namespace Robot.Input
{
    public class InputException : Exception
    {
        public InputException(string Message) : base(Message) { }
    }

    public class InvalidContentException : InputException
    {
        public InvalidContentException(string input) : base($"Content input is not recognized {input}") { }
    }

    public class InvalidStateException : InputException
    {
        public InvalidStateException(string input) : base($"State input [{input}] is not reognized") { }
    }

    public class InvalidPositionException : InputException
    {
        public InvalidPositionException(string input) : base($"Position input [{input}] is not reognized") { }
    }

    public class InvalidDirectionException : InputException
    {
        public InvalidDirectionException(string input) : base($"Direction input [{input}] is not reognized") { }
    }

    public class InvalidCommandException : InputException
    {
        public InvalidCommandException(string input) : base($"Command input [{input}] is not reognized") { }

        public InvalidCommandException(string s, string input) : base($"Command input [{s}] is not reognized in {input}") { }
    }

    public class InvalidResultException : InputException
    {
        public InvalidResultException(string input) : base($"Result input [{input}] is not reognized") { }
    }


}
