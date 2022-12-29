namespace api.Utility.Validation
{
    public interface IValidationFactory
    {
        public Task<IValidationHandlerBase> GetHandler(object o);
    }
}