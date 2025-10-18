namespace EventsManagement.Dtos.ProgressDtos
{
    public class StudentApiResponse
    {
        public int Id { get; set; }
        public string individuNomLatin { get; set; } = string.Empty;
        public string individuPrenomLatin { get; set; } = string.Empty;
        public string individuDateNaissance { get; set; } = string.Empty;
        public string niveauLibelleLongLt { get; set; } = string.Empty;
        public string anneeAcademiqueCode { get; set; } = string.Empty;
        public string uuid { get; set; } = string.Empty;
    }
}
