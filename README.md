# todolist
Todo List Application

## Init

### Init (Server)
- ```bash
  dotnet new webapi -f netcoreapp3.1
  ```

## Update

### Update (DB)
- ```bash
  # Visual Studio 의 Package Management Console 에서 수행
  
  # Migration Code 생성
  Add-Migration InitTodoListDb

  # DB에 반영
  Update-Database
  ```