namespace SampleProject.DataTransferObject.ViewModels
{
    public class PersonViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SelectViewModel Gender { get; set; }
        public bool IsActive { get; set; }
    }
}
