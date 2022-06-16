# ExperimentingNCache
Trying out NCache extension methods


## How to setup project

1. Open [UserContext.cs](UsersApi/Models/UserContext.cs) 
2. Update the `connectionSting` and `cacheId` in `OnConfiguringMethod` as shown below:

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    var cacheId = "your-ncache-cache-id";
    var connectionString = "your-mysql-connection-string";

    NCacheConfiguration.Configure(cacheId, DependencyType.Other);
    NCacheConfiguration.ConfigureLogger();
    
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
    optionsBuilder.UseMySql(connectionString, serverVersion);
}
```

3. Apply the database migrations.


## Seeding the database

1. Run the project. (Opens swagger UI)
2. Call the API: POST `/api/Users` several times and post some random values several times to add users to database.


## Testing NCache and Reproducing ISSUES

### ISSUE 1

1. Call the API: GET `/api/Users/LoadIntoCache`  
It should load the data into cache.

2. Call the API: GET `/api/FromCacheOnly`  
It returns empty list. (Even though `LoadIntoCache` was called earlier).



### ISSUE 2

1. Call the API: GET `/api/users/FromCacheAsSeperateEntities`  
If its called the first time, it should load all data into cache as seperate entities.

2. Call the same API again: GET `/api/users/FromCacheAsSeperateEntities`  
It makes a Database call, and updates the cache again instead of retrieving data from cache directly.
