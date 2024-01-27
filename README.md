# todo-web-app

### resume
[bahriddin-abdusalomov-resume.pdf](https://github.com/bahriddin-abdusalomov/todo-web-app/files/14072266/bahriddin-abdusalomov-resume.pdf)

### git clone 
```
https://github.com/bahriddin-abdusalomov/todo-web-app.git
```
![image](https://github.com/bahriddin-abdusalomov/todo-web-app/assets/123171397/898d71c4-1247-4234-8c29-fa3d498cb6a2)
Projectni ishlatish uchun `Server` va `Database` qismi o'zingizning databasengizga mos ravishda o'zgartirilishi kerak boladi

### Blazor layerda yaratilgan api dan foydalanish

`CORS` package yukab olinadi
```
<PackageReference Include="Microsoft.AspNetCore.Cors" Version="2.2.0" />
```

`Program.cs` filega `CORS` ni qo'shish kerak
![image](https://github.com/bahriddin-abdusalomov/todo-web-app/assets/123171397/73b8df02-8f4b-428c-a056-3489772dbb55)

`Blazor` layerdagi `Program.cs` filega qo'shiladi
![image](https://github.com/bahriddin-abdusalomov/todo-web-app/assets/123171397/698b1a78-d6d5-493c-9125-eb1aa31e2975)

`API` layerdan malumotlarni olish quyidacha boladi
```
 private readonly HttpClient httpClient;

 public TaskService(HttpClient httpClient)
 {
     this.httpClient = httpClient;
 }

 public async Task<IList<TaskModel>?> GetTasks()
 {
     var tasks = await httpClient.GetFromJsonAsync<IList<TaskModel>>("api/Tasks/GetTasks");
     return tasks;
 }
```
