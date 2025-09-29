using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.ValidationServices
{
    public interface IPersonelValidationService
    {
        Task<ValidationResult> ValidatePersonelUpdateAsync(PersonelUpdateDto personelUpdateDto);
        Task<ValidationResult> ValidateForeignKeysAsync(PersonelUpdateDto personelUpdateDto);
    }
}
