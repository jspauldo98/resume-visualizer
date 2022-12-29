using AutoMapper;

using api.Utility.Validation;

namespace api.Utility
{
    public class XfrHandler : IXfrHandler
    {
        private readonly IMapper _mapper;
        private readonly IValidationFactory _validationFactory;
        
        public XfrHandler(
            IMapper mapper,
            IValidationFactory validationFactory
        )
        {
            _mapper = mapper;
            _validationFactory = validationFactory;
        }

        public async Task<ValidationResult> Validate<T>(object xfr)
        {
            var validationResult = new ValidationResult();

            // In case validaiton fails, only want client to be able to see client side architecture
            if (xfr.GetType().Namespace?.Contains("Dto") ?? false)
                validationResult.AddObject(xfr);

            var validationHandler = await _validationFactory.GetHandler(xfr);
            validationHandler.Validate(validationResult, xfr);

            // TODO: Could save validation result to DB here if not valid

            return validationResult;
        }

        public async Task<ValidationResult> Xfr<T1, T2>(object xfrStart)
        {
            var ftValidation = await Validate<T1>(xfrStart);
            if (!ftValidation.IsValid)
                return ftValidation;
            
            var xfrDest = _mapper.Map<T2>((T1)xfrStart);

            if (xfrDest == null)
                return new ValidationResult($"Xfr destination is null upon validation");

            var bkValidation = await Validate<T2>(xfrDest);
            if (!bkValidation.IsValid)
                return bkValidation;

            return new ValidationResult(xfrDest);
        }

        public async Task<ValidationResult> Xfr<T1, T2, T3>(object xfrStart)
        {
            var ftValidation = await Xfr<T1, T2>(xfrStart);
            if (!ftValidation.IsValid)
                return ftValidation;

            var xfrMid = ftValidation.ObjectList.FirstOrDefault();

            if (xfrMid == null)
                return new ValidationResult($"Xfr destination is null upon validation");

            return await Xfr<T2, T3>(xfrMid);
        }
    }
}