﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class ValidationError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }

        public ValidationError(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }
    }
}
