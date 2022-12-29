namespace api.Utility.Validation
{
    public class ValidationFactory : IValidationFactory
    {
        private readonly IServiceProvider _provider;

        public ValidationFactory(
            IServiceProvider provider
        )
        {
            _provider = provider;
        }

        public Task<IValidationHandlerBase> GetHandler(object o)
        {
            switch (o)
            {
                default:
                    throw new NotSupportedException($"Handler not supported with object type: {o.GetType()}");
            }
        }
    }
}