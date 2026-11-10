namespace Lab2;

// Interface changing: move from one form to another.
static class Nav
{
    private static bool switching;

    public static void Go(Form from, Form to)
    {
        switching = true;
        to.Show();
        from.Close();
        switching = false;
    }

    // Called when any form closes. If the user closed it (X button), exit the app.
    public static void OnClosed()
    {
        if (!switching) Application.Exit();
    }
}
