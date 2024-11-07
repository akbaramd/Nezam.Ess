using AutoMapper;
using Microsoft.Extensions.Localization;
using Nezam.Modular.ESS.Secretariat.Application.Documents;
using Nezam.Modular.ESS.Secretariat.Application.Resources;
using Nezam.Modular.ESS.Secretariat.Domain.Documents;

namespace Nezam.Modular.ESS.IdEntity.Application.Employers
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            // Map DocumentStatus with localization using the BonEnumerationTranslationResolver
            CreateMap<DocumentAggregateRoot, DocumentDto>();

            // Map Description with DocumentDescriptionConverter
            CreateMap<DocumentActivityLogEntity, DocumentActivityLogDto>()
                .ForMember(d => d.Description, 
                    opt => opt.ConvertUsing<DocumentDescriptionConverter, string>(src => src.Key));
        }
    }

    // Custom converter to apply localization on string properties
    public class DocumentDescriptionConverter : IValueConverter<string, string>
    {
        private readonly IStringLocalizer<DocumentTranslates> _localizer;

        // Inject IStringLocalizer through constructor
        public DocumentDescriptionConverter(IStringLocalizer<DocumentTranslates> localizer)
        {
            _localizer = localizer;
        }

        public string Convert(string source, ResolutionContext context)
        {
            // Use the localizer to translate the source string, defaulting to source if no translation is found
            return string.IsNullOrEmpty(source) ? source : _localizer[source];
        }
    }

    // Custom resolver to translate BonEnumeration-based properties using IStringLocalizer
}
