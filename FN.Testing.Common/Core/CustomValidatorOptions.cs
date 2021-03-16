using FluentValidation;
using FN.Testing.Common.Contract;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FN.Testing.Common.Core
{
    public static class CustomValidatorOptions
    {
        public static IRuleBuilderOptions<T, TProperty> WithMessage<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule,
            string errorMessage,
            string[] args
            )
        {
            var merged = new string[]
            {
                errorMessage,
                string.Join(",", args)
            };

            return rule.WithMessage(string.Join("|", merged));
        }
        public static IRuleBuilderOptions<T, string> MinimumLengthConfigured<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            int minimumLength,
            string validationKey = ValidationKey.MinLength
            ) => ruleBuilder
                .MinimumLength(minimumLength)
                .WithSeverity(Severity.Error)
                .WithMessage(
                    validationKey,
                    new string[] { minimumLength.ToString(CultureInfo.InvariantCulture) });

        public static IRuleBuilderOptions<T, string> MaximumLengthConfigured<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            int maximumLength,
            string validationKey = ValidationKey.MaxLength
            ) => ruleBuilder
                .MaximumLength(maximumLength)
                .WithSeverity(Severity.Error)
                .WithMessage(
                    validationKey,
                    new string[] { maximumLength.ToString(CultureInfo.InvariantCulture) });

        public static IRuleBuilderOptions<T, TProperty> GreaterThanConfigured<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            TProperty valueToCompare,
            string validationKey = ValidationKey.Min
            ) where TProperty : IComparable<TProperty>, IComparable
                => ruleBuilder
                .GreaterThan(valueToCompare)
                .WithSeverity(Severity.Error)
                .WithMessage(
                    validationKey,
                    new string[] { valueToCompare.ToString() });

        public static IRuleBuilderOptions<T, TProperty> MustConfigured<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            Func<TProperty, bool> predicate,
            string validationKey = ValidationKey.Exists)
            => ruleBuilder
                .Must(predicate)
                .WithSeverity(Severity.Error)
                .WithMessage(validationKey);

        public static IRuleBuilderOptions<T, TProperty> MustConfigured<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            Func<T, TProperty, bool> predicate,
            string validationKey = ValidationKey.Exists)
            => ruleBuilder
                .Must(predicate)
                .WithSeverity(Severity.Error)
                .WithMessage(validationKey);

        public static IRuleBuilderOptions<T, TProperty> NotNullConfigured<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            string validationKey = ValidationKey.Required)
            => ruleBuilder
                .NotNull()
                .WithSeverity(Severity.Error)
                .WithMessage(validationKey);

        public static IRuleBuilderOptions<T, string> CreditCardConfigured<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            string validationKey = ValidationKey.Invalid)
            => ruleBuilder
                .CreditCard()
                .WithSeverity(Severity.Error)
                .WithMessage(validationKey);

        public static IRuleBuilderOptions<T, string> MatchesConfigured<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            Regex regex,
            string validationKey = ValidationKey.Invalid)
            => ruleBuilder
                .Matches(regex)
                .WithSeverity(Severity.Error)
                .WithMessage(validationKey);
    }
}
