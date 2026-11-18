# Ikimina Manager - C# Desktop Application (Windows Forms)

Savings group (ikimina) contribution and loan calculators, built in the same style as Lab 2.

Flow: Login -> Choose -> Contribution / Loan (Back = Choose, Logout = Login)

Rules used (example values, edit the constants at the top of each form):
- Contribution: 5,000 RWF per weekly cycle. Paid = on time, Late = 1-6 days late (+500 RWF fee), Missed = 7+ days late.
- Loan: interest = amount x 5% x months; rejected if larger than the cash in the pot.

## Run
- Visual Studio 2022: open the folder (or `IkiminaManager.csproj`) and press F5
- Terminal (Windows): `dotnet run`
