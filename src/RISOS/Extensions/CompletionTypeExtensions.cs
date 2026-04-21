using RISOS.Enums;
using RISOS.Localization;

namespace RISOS.Extensions;

public static class CompletionTypeExtensions
{
    extension(CompletionType typ)
    {
        public string GetDescription() => typ switch
        {
            CompletionType.None => LanguageHelper.CompletionTypeNone,
            CompletionType.Cr => LanguageHelper.CompletionTypeCr,
            CompletionType.GCr => LanguageHelper.CompletionTypeGCr,
            CompletionType.CrEx => LanguageHelper.CompletionTypeCrEx,
            CompletionType.Ex => LanguageHelper.CompletionTypeEx,
            CompletionType.Col => LanguageHelper.CompletionTypeCol,
            CompletionType.Kp => LanguageHelper.CompletionTypeKp,
            CompletionType.Rec => LanguageHelper.CompletionTypeRec,
            CompletionType.Szz => LanguageHelper.CompletionTypeSzz,
            CompletionType.RCr => LanguageHelper.CompletionTypeRCr,
            CompletionType.RgCr => LanguageHelper.CompletionTypeRgCr,
            CompletionType.RCrEx => LanguageHelper.CompletionTypeRCrEx,
            CompletionType.REx => LanguageHelper.CompletionTypeREx,
            CompletionType.Hs => LanguageHelper.CompletionTypeHs,
            CompletionType.Hds => LanguageHelper.CompletionTypeHds,
            CompletionType.UpZa => LanguageHelper.CompletionTypeUpZa,
            CompletionType.RVol => LanguageHelper.CompletionTypeRVol,
            CompletionType.CrFsp => LanguageHelper.CompletionTypeCrFsp,
            CompletionType.DrEx => LanguageHelper.CompletionTypeDrEx,
            CompletionType.RecCr => LanguageHelper.CompletionTypeRecCr,
            CompletionType.ExDd => LanguageHelper.CompletionTypeExDd,
            CompletionType.SpEx => LanguageHelper.CompletionTypeSpEx,
            CompletionType.SpCrEx => LanguageHelper.CompletionTypeSpCrEx,
            CompletionType.RCol => LanguageHelper.CompletionTypeRCol,
            _ => LanguageHelper.CompletionTypeUnknown
        };

        public string ToLabel() => typ switch
        {
            CompletionType.None => LanguageHelper.CompletionTypeShortNone,
            CompletionType.Cr => LanguageHelper.CompletionTypeShortCr,
            CompletionType.GCr => LanguageHelper.CompletionTypeShortGCr,
            CompletionType.CrEx => LanguageHelper.CompletionTypeShortCrEx,
            CompletionType.Ex => LanguageHelper.CompletionTypeShortEx,
            CompletionType.Col => LanguageHelper.CompletionTypeShortCol,
            CompletionType.Kp => LanguageHelper.CompletionTypeShortKp,
            CompletionType.Rec => LanguageHelper.CompletionTypeShortRec,
            CompletionType.Szz => LanguageHelper.CompletionTypeShortSzz,
            CompletionType.RCr => LanguageHelper.CompletionTypeShortRCr,
            CompletionType.RgCr => LanguageHelper.CompletionTypeShortRgCr,
            CompletionType.RCrEx => LanguageHelper.CompletionTypeShortRCrEx,
            CompletionType.REx => LanguageHelper.CompletionTypeShortREx,
            CompletionType.Hs => LanguageHelper.CompletionTypeShortHs,
            CompletionType.Hds => LanguageHelper.CompletionTypeShortHds,
            CompletionType.UpZa => LanguageHelper.CompletionTypeShortUpZa,
            CompletionType.RVol => LanguageHelper.CompletionTypeShortRVol,
            CompletionType.CrFsp => LanguageHelper.CompletionTypeShortCrFsp,
            CompletionType.DrEx => LanguageHelper.CompletionTypeShortDrEx,
            CompletionType.RecCr => LanguageHelper.CompletionTypeShortRecCr,
            CompletionType.ExDd => LanguageHelper.CompletionTypeShortExDd,
            CompletionType.SpEx => LanguageHelper.CompletionTypeShortSpEx,
            CompletionType.SpCrEx => LanguageHelper.CompletionTypeShortSpCrEx,
            CompletionType.RCol => LanguageHelper.CompletionTypeShortRCol,
            _ => LanguageHelper.CompletionTypeShortUnknown
        };
    }
}
