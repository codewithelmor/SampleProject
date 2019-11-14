namespace SampleProject.DataTransferObject.BindingModels
{
    public class BaseBindingModel
    {
        private string _userName;

        public void SetUserName(string userName)
        {
            _userName = userName;
        }

        public string GetUserName()
        {
            return _userName;
        }
    }
}