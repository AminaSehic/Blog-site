namespace BlogApiRubicon.Exceptions
{
    class ClientErrorResponse
    {
        public string Message { get; set; }

        public ClientErrorResponse(string message)
        {
            Message = message;
        }
    }
}