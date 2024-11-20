using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Manage.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "FullName must not be blank")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email must not be blank")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone must not be blank")]
        [Phone]
        public string Phone { get; set; }

        [StringLength(50)]
        public string? ReferralCode { get; set; }

        public int? ReferredBy { get; set; }

        // Thời gian đăng ký
        public DateTime CreatedAt { get; set; }

        // Thời gian cập nhật
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("ReferredBy")]
        public virtual Users? Referrer { get; set; }

        
    }
}
