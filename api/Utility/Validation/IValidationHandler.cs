namespace api.Utility.Validation
{
    public interface IValidationHandler
    {
        public ValidationResult HandleValidationError(object o);
    }
}