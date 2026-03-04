namespace RISOS.Services
{
    public class ImportService(ThemeStateService themeStateService)
    {
        public async Task SaveFullStateAsync(AppState state)
        {
            await themeStateService.SetDarkMode(state.IsDarkMode);
        }
    }
}
