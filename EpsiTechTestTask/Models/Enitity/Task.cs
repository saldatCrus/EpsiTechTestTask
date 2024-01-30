namespace EpsiTechTestTask.Models.Enitity
{
    public class Task
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

    }
}
