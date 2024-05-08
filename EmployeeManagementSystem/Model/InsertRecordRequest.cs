using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Model
{
    public class InsertRecordRequest
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }

        [Required]
        [BsonElement("Name")]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string PhonenNumber { get; set; }

        [Required]
        public string Role { get; set; }

        public int Salary { get; set; }

    }
}
