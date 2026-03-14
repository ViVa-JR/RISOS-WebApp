namespace RISOS.Services
{
    public class ImportService(LocalStorageService localStorageService)
    {
        public async Task ApplyAppStateAsync(AppState state)
        {
            await localStorageService.SaveProgramAbbreviation(state.ProgramAbbreviation);
        }
    }
}
