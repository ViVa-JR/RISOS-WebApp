using RISOS.Enums;

namespace RISOS.Pages.Home.Dialogs.SettingsDialog;

public class SettingsDialogModel
{
    public AppLanguage Language { get; set; }
    public int? Credits { get; set; }
    public ThemeType SelectedTheme { get; set; }
}
