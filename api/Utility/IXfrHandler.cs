using api.Utility.Validation;

namespace api.Utility
{
    public interface IXfrHandler
    {
        public Task<ValidationResult> Validate<T>(object xfr);
        public Task<ValidationResult>Xfr<T1, T2>(object xfrStart);
        public Task<ValidationResult>Xfr<T1, T2, T3>(object xfrStart);
    }
}