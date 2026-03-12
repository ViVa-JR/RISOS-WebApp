namespace RISOS.Services
{
    public class ImportService(LocalStorageService localStorageService)
    {
        public async Task SaveFullStateAsync(AppState state)
        {
            await localStorageService.SaveProgramAbbreviation(state.ProgramAbbreviation);
        }
    }
}
