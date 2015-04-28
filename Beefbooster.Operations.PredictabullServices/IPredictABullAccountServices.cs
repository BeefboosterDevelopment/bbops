namespace Beefbooster.Operations.PredictabullServices
{
    public interface IPredictABullAccountServices
    {
        AccountsWithPreferencesView AccountsWithPreferences(int year, string strain);
        PreferencesView PreferencesForUser(int userId, int year, string strain);
    }
}