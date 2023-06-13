This is my first REST API app with blazor front end.

### Required Resources:
1. [SDK .net 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
2. [SQL Server Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### To build and run this app:
1. Create the DB using the Microsoft SQL Server Management Studio.
    1. Open the Microsoft SQL Server Management Studio.
    2. Drag and drop the file `DB.sql` into the editor.
    3. Execute the entire file.
2. Clone the repository `git clone https://github.com/KwnGou/WorkOutAppRepo.git`
3. Run the following in a CMD line:
    1. `cd WorkOutAPI`
    2. `dotnet run .\WorkOutAPI.csproj` (Keep this open)
4. In a separate CMD line, run the following:
    1. `cd WorkOutBlazor`
    2. `dotnet run .\WorkOutBlazor.csproj`
