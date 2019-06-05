using System;
using System.ComponentModel.DataAnnotations;
using Framework.Shared.Messaging;

namespace CustomerServiceApp.Domain.Commands
{
    public class CreateCustomerCommand : ICommand
    {
        public CreateCustomerCommand()
        {
            CommandId = Guid.NewGuid();
        }

        public Guid CommandId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Range(1, int.MaxValue)]
        public int BrandId { get; set; }
    }
}
