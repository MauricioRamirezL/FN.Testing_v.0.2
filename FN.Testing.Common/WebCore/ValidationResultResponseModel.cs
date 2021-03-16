namespace FN.Testing.Common.WebCore
{
    public class ValidationResultResponseModel<TResult> : ValidationResponseModel
    {
        public ValidationResultResponseModel(TResult model)
        {
            Result = model;
        }
        public TResult Result { get; set; }
    }
}
