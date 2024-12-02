var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
                 .WithLifetime(ContainerLifetime.Persistent);

var db = sql.AddDatabase("Database");

builder.AddProject<Projects.Savvy_ZooKeeper>("savvy-zookeeper")
       .WithReference(db);

builder.Build().Run();
