﻿using FN.Testing.Application.Contract.Models;
using FN.Testing.Business.Contract.Abstractions;
using FN.Testing.Common.Core;
using System;

namespace FluentValidation.Application.Validators
{
    public class UploadModelValidator : AbstractValidator<UploadModel>
    {
        public UploadModelValidator(IUploadDataService uploadDataService)
        {
            if (uploadDataService == null)
                throw new ArgumentNullException(nameof(uploadDataService));

            //RuleFor(x => x.Id).NotNullConfigured();
            //RuleFor(x => x.FileName).MinimumLengthConfigured(50);
            //RuleFor(x => x.UploadDate).NotNullConfigured();
        }
    }
}
