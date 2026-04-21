namespace RISOS.Services;

public class ImportService(LocalStorageService localStorageService)
{
    public async Task ApplyAppStateAsync(AppState state)
    {
        await localStorageService.SaveProgramAbbreviation(state.ProgramAbbreviation);
        await localStorageService.SaveProgramSpecialization(state.ProgramSpecialization);
        await localStorageService.SaveSubjects(state.Subjects);
        await localStorageService.SaveCustomSubjects(state.CustomSubjects);
        await localStorageService.SaveCreditOverride(state.CreditOverride);
        await localStorageService.SaveStudyYears(state.StudyYears);
        await localStorageService.SaveRecognizedYear(state.RecognizedYear);
    }
}