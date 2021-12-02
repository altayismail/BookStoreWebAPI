using FluentValidation;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidation : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidation()
        {
            RuleFor(query => query.BookId).GreaterThan(0);
        }
    }
}