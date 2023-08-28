WorkOutApp is a Single Page Application using Radzen component library for the UI and  Microsoft SQL Database for Data storage. 
It was created using Microsoft Visual Studio and Microsoft SQL Server Management Studio. 
The application function is to oganize you exercises and create your workout schedule. 

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

### Screenshots:

Schedules grid with inline edit and delete functions.

![image](https://github.com/KwnGou/WorkOutApp/assets/110529457/4e831b3c-c25a-43bb-8d5c-d4570d900b0f)

Schedule creation dialogue.

![image](https://github.com/KwnGou/WorkOutApp/assets/110529457/f4bcefae-d8be-4309-8092-698bad70da44)
![image](https://github.com/KwnGou/WorkOutApp/assets/110529457/f0bd6e5d-2327-45c6-b27f-197f5ef7e6a6)

 Exercises grid with inline edit , delete and create functions.

![image](https://github.com/KwnGou/WorkOutApp/assets/110529457/7ce31d5c-922d-44bd-be9f-76881b27fb18)
