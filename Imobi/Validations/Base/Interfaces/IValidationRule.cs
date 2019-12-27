namespace Imobi.Validations.Base.Interfaces
{
    public interface IValidationRule<T>
    {
        #region Public Properties

        string ValidationMessage { get; set; }

        #endregion Public Properties



        #region Public Methods

        bool Check(T value);

        #endregion Public Methods
    }
}