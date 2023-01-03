# Todo List Application
- Built with .NET Core 3.1
- Author : Sangjin Kim

# Back-end

## Swagger 확인
- ~/swagger

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