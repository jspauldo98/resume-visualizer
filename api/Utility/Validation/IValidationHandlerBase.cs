namespace api.Utility.Validation
{
    public interface IValidationHandlerBase
    {
        public void Validate(ValidationResult validationResult, object obj);
    }
}