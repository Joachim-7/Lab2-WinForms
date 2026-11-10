namespace Lab2;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        new LoginForm().Show();
        Application.Run(); // ends when Nav calls Application.Exit()
    }
}
