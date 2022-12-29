namespace api.Utility.Validation
{
    public class ValidationHandler : IValidationHandler
    {
        public ValidationResult HandleValidationError(object o)
        {
            switch (o)
            {
                default:
                    return HandleUnknownValidationError();
            }
        }

        private ValidationResult HandleUnknownValidationError()
        {
            var validationResultModel = new ValidationResult();
            validationResultModel.AddMessage($"An unexpected error occurred when trying to save an Unknown object");
            return validationResultModel;
        }
    }
}