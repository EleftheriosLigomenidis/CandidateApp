using System.ComponentModel.DataAnnotations;

namespace CandidateApp.Business.Options
{
    public class AzureBlobStorageSettings
    {
        public const string Key = "AzureBlobStorage";
        [Required]
        public string ConnectionString { get; set; } = null!;
        [Required]
        public string ContainerName { get; set; } = null!;
    }
}
